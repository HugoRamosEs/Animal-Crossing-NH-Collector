using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using api.Abstractions;
using api.Models;
using api.Options;

using static api.Options.Constants;

namespace api.Internals;

public class UserItemsService : IUserItemService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ILogger<UserItemsService> _logger;
    private readonly ApplicationDbContext _dbContext;

    public UserItemsService(UserManager<IdentityUser> userManager, ILogger<UserItemsService> logger, ApplicationDbContext dbContext)
    {
        _userManager = userManager;
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task<Result<object>> AddUserItemAsync(string email, string itemName)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return Result<object>.Failure(RequestCodes.UserNotFound, RequestMessages.FailureUserNotFound);
        }

        var itemId = await GetItemIdByNameAsync(itemName);
        if (itemId == null)
        {
            return Result<object>.Failure(RequestCodes.ItemNotFound, RequestMessages.ItemNotFound);
        }

        var userItem = new UserItem
        {
            UserId = user.Id,
            ItemId = itemId.Value
        };

        try
        {
            _dbContext.UserItems.Add(userItem);
            await _dbContext.SaveChangesAsync();
            return Result<object>.Success(userItem, RequestMessages.SuccessAddUserItem);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding item for user: {Email}.", email);
            return Result<object>.Failure(RequestCodes.FailureAddUserItem, RequestMessages.FailureAddUserItem);
        }
    }

    public async Task<Result<IEnumerable<Item>>> GetUserItemsAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return Result<IEnumerable<Item>>.Failure(RequestCodes.UserNotFound, RequestMessages.FailureUserNotFound);
        }

        try
        {
            var items = await _dbContext.UserItems
                .Where(ui => ui.UserId == user.Id)
                .Select(ui => ui.Item)
                .ToListAsync();

            if (items == null || !items.Any())
            {
                return Result<IEnumerable<Item>>.Success(Enumerable.Empty<Item>(), RequestMessages.NoItemsFound);
            }

            return Result<IEnumerable<Item>>.Success(items, RequestMessages.SuccessGetUserItems);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting items for user: {Email}.", email);
            return Result<IEnumerable<Item>>.Failure(RequestCodes.FailureGetUserItems, RequestMessages.FailureGetUserItems);
        }
    }


    public async Task<Result<object>> RemoveUserItemAsync(string email, string itemName)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return Result<object>.Failure(RequestCodes.UserNotFound, RequestMessages.FailureUserNotFound);
        }

        var itemId = await GetItemIdByNameAsync(itemName);
        if (itemId == null)
        {
            return Result<object>.Failure(RequestCodes.ItemNotFound, RequestMessages.ItemNotFound);
        }

        var userItem = await _dbContext.UserItems
            .FirstOrDefaultAsync(ui => ui.UserId == user.Id && ui.ItemId == itemId.Value);

        if (userItem == null)
        {
            return Result<object>.Failure(RequestCodes.UserItemNotFound, RequestMessages.UserItemNotFound);
        }

        try
        {
            _dbContext.UserItems.Remove(userItem);
            await _dbContext.SaveChangesAsync();
            return Result<object>.Success(null, RequestMessages.SuccessGetUserItems);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting item for user: {Email}.", email);
            return Result<object>.Failure(RequestCodes.FailureRemoveUserItem, RequestMessages.FailureRemoveUserItem);
        }
    }

    private async Task<int?> GetItemIdByNameAsync(string itemName)
    {
        return await _dbContext.Items
            .Where(i => i.Name == itemName)
            .Select(i => (int?)i.Id)
            .FirstOrDefaultAsync();
    }
}
