using api.Models;

namespace api.Abstractions;

public interface IPasswordManagementService
{
    Task<Result<object>> RequestPasswordResetAsync(string email);

    Task<Result<object>> ResetPasswordAsync(string email, string token, string newPassword);
}
