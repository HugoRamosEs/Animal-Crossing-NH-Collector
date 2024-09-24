using Microsoft.AspNetCore.Identity;

using api.Abstractions;
using api.Models;

using static api.Options.Constants;

namespace api.Internals;

public class AuthService : IAuthService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly ILogger<AuthService> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ILogger<AuthService> logger, IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Result<object>> AuthSignUpAsync(string username, string email, string password, string repeatedPassword)
    {
        var errors = new List<string>();

        var existingUser = await _userManager.FindByEmailAsync(email);
        if (existingUser != null) {
            errors.Add(RequestMessages.FailureEmailExists);
            return Result<object>.Failure(RequestCodes.FailureEmailExists, RequestMessages.FailureEmailExists, errors);
        }

        var user = new IdentityUser
        {
            UserName = username,
            Email = email,
        };

        var signUpResult = await _userManager.CreateAsync(user, password);

        if (signUpResult.Succeeded)
        {
            _logger.LogInformation("User {Username} created a new account with password hash: {PasswordHash}.", username, user.PasswordHash);
            return Result<object>.Success(null, RequestMessages.SuccessCreateAccount);
        }

        return Result<object>.Failure(RequestCodes.FailureCreateAccount, RequestMessages.FailureCreateAccount, errors);
    }

    public async Task<Result<object>> AuthLoginAsync(string email, string password)
    {
        var errors = new List<string>();
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
        {
            errors.Add(RequestMessages.FailureUserNotFound);
            return Result<object>.Failure(RequestCodes.UserNotFound, RequestMessages.FailureUserNotFound, errors);
        }

        var signInResult = await _signInManager.PasswordSignInAsync(user.UserName!, password, isPersistent: true, lockoutOnFailure: false);

        if (signInResult.Succeeded)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var isAuthenticated = httpContext?.User?.Identity?.IsAuthenticated ?? false;

            if (isAuthenticated)
            {
                var userInfo = new
                {
                    user.UserName,
                    user.Email
                };

                _logger.LogInformation("User {Username} logged in with email: {UserEmail}.", user.UserName, user.Email);
                return Result<object>.Success(userInfo, RequestMessages.SuccessLogin);
            }
            else
            {
                errors.Add(RequestMessages.FailureCookieNotSet);
                return Result<object>.Failure(RequestCodes.CookieNotSet, RequestMessages.FailureCookieNotSet, errors);
            }
        }

        if (signInResult.IsLockedOut)
        {
            errors.Add(RequestMessages.FailureLockout);
            return Result<object>.Failure(RequestCodes.LockedOut, RequestMessages.FailureLockout, errors);
        }
        else if (signInResult.IsNotAllowed)
        {
            errors.Add(RequestMessages.FailureLogin);
            return Result<object>.Failure(RequestCodes.NotAllowed, RequestMessages.FailureLogin, errors);
        }
        else
        {
            errors.Add(RequestMessages.FailurePasswordIncorrect);
            return Result<object>.Failure(RequestCodes.PasswordIncorrect, RequestMessages.FailurePasswordIncorrect, errors);
        }
    }

    public async Task<Result<object>> AuthLogoutAsync()
    {
        try
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out");
            return Result<object>.Success(null, RequestMessages.SuccessLogout);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred during logout.");
            return Result<object>.Failure(RequestCodes.FailureLogout, RequestMessages.FailureLogin);
        }
    }

    public async Task<bool> AuthCheckCookieAsync()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        var isAuthenticated = httpContext?.User?.Identity?.IsAuthenticated ?? false;
        var cookieExists = httpContext?.Request.Cookies.ContainsKey(".AspNetCore.Identity.Application") ?? false;

        return await Task.FromResult(isAuthenticated && cookieExists);
    }
}
