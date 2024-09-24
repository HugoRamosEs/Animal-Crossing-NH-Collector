using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Options;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<UserItem> UserItems { get; set; }
    public DbSet<Item> Items { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserItem>()
            .HasKey(ui => new { ui.UserId, ui.ItemId });

        modelBuilder.Entity<UserItem>()
            .HasOne(ui => ui.User)
            .WithMany()
            .HasForeignKey(ui => ui.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UserItem>()
            .HasOne(ui => ui.Item)
            .WithMany()
            .HasForeignKey(ui => ui.ItemId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
