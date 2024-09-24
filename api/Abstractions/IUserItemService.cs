using api.Models;

namespace api.Abstractions;

public interface IUserItemService
{
    public Task<Result<object>> AddUserItemAsync(string email, string itemName);

    public Task<Result<IEnumerable<Item>>> GetUserItemsAsync(string email);

    public Task<Result<object>> RemoveUserItemAsync(string email, string itemName);
}
