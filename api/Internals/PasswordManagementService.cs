using api.Abstractions;
using api.Models;

namespace api.Internals;

public class PasswordManagementService : IPasswordManagementService
{
    public Task<Result<object>> RequestPasswordResetAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task<Result<object>> ResetPasswordAsync(string email, string token, string newPassword)
    {
        throw new NotImplementedException();
    }
}
