using api.Models;

namespace api.Abstractions;

public interface IAuthService
{
    Task<Result<object>> AuthSignUpAsync(string username, string email, string password, string repeatedPassword);

    Task<Result<object>> AuthLoginAsync(string email, string password);

    Task<Result<object>> AuthLogoutAsync();

    Task<bool> AuthCheckCookieAsync();
}
