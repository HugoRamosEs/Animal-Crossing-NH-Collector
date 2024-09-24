using Microsoft.AspNetCore.Mvc;

using api.Abstractions;
using api.Utilities;
using api.Models;
using static api.Options.Constants;

namespace api.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authenticationService;

    public AuthController(IAuthService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("signup")]
    public async Task<IActionResult> SignUpAsync([FromBody] SignUpRequest request)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            return BadRequest(new { Errors = errors });
        }

        var sanitizedRequest = SanitizeSignUpRequest(request);

        if (sanitizedRequest.Password != sanitizedRequest.RepeatedPassword)
        {
            return BadRequest(new { ErrorCode = RequestCodes.FailurePasswordDoesNotMatch, Message = RequestMessages.FailurePasswordDoesNotMatch });
        }

        var result = await _authenticationService.AuthSignUpAsync(
            sanitizedRequest.Username,
            sanitizedRequest.Email,
            sanitizedRequest.Password,
            sanitizedRequest.RepeatedPassword
        );

        return result.ToActionResult();
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            return BadRequest(new { Errors = errors });
        }

        var sanitizedRequest = SanitizeLoginRequest(request);

        var result = await _authenticationService.AuthLoginAsync(
            sanitizedRequest.Email, 
            sanitizedRequest.Password
        );

        return result.ToActionResult();
    }

    [HttpPost("logout")]
    public async Task<IActionResult> LogoutAsync()
    {
        var result = await _authenticationService.AuthLogoutAsync();

        return result.ToActionResult();
    }

    [HttpGet("check-cookie")]
    public async Task<IActionResult> CheckCookieAsync()
    {
        var hasCookie = await _authenticationService.AuthCheckCookieAsync();

        return Ok(hasCookie);
    }

    private static SignUpRequest SanitizeSignUpRequest(SignUpRequest request)
    {
        return new SignUpRequest
        {
            Username = StringUtilities.Sanitize(request.Username),
            Email = StringUtilities.Sanitize(request.Email),
            Password = StringUtilities.Sanitize(request.Password),
            RepeatedPassword = StringUtilities.Sanitize(request.RepeatedPassword)
        };
    }

    private static LoginRequest SanitizeLoginRequest(LoginRequest request)
    {
        return new LoginRequest
        {
            Email = StringUtilities.Sanitize(request.Email),
            Password = StringUtilities.Sanitize(request.Password)
        };
    }
}
