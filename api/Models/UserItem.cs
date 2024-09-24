using Microsoft.AspNetCore.Identity;

namespace api.Models;

public class UserItem
{
    public string UserId { get; set; }
    public int ItemId { get; set; }

    public virtual IdentityUser User { get; set; }
    public virtual Item Item { get; set; }
}