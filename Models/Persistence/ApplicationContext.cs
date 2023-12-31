using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using GamesStoreWebApi.Models.Entities;

namespace GamesStoreWebApi.Models.Persistence;
public class ApplicationContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public DbSet<Game> Games { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Purchase> Purchases { get; set; }
    public DbSet<Key> Keys { get; set; }
    public DbSet<Price> Prices { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<Collection> Collections { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(message => Console.WriteLine(message));
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //настройка Identity
        base.OnModelCreating(modelBuilder);
        //заполнение таблицы AspNetRoles начальными данными
        modelBuilder.Entity<IdentityRole<Guid>>().HasData(
            new IdentityRole<Guid>
            {
                Id = new Guid("cfdedf85-d8fc-4286-a9e8-d77e1a7dae1c"),
                Name = "User",
                NormalizedName = "User".Normalize().ToUpperInvariant(),
                ConcurrencyStamp = "235384a7-be0b-4094-b34c-0b0e9cb15fd3"
            },
            new IdentityRole<Guid>
            {
                Id = new Guid("7d85b102-0f69-450e-93e8-28192d10aafe"),
                Name = "Administrator",
                NormalizedName = "Administrator".Normalize().ToUpperInvariant(),
                ConcurrencyStamp = "1748c6d1-e1f6-4566-a4f9-269334ac65f3"
            },
            new IdentityRole<Guid>
            {
                Id = new Guid("de86b3db-8137-4e9e-b784-b45c3d5080b8"),
                Name = "Root",
                NormalizedName = "Root".Normalize().ToUpperInvariant(),
                ConcurrencyStamp = "180dd38f-8105-477e-bbb9-061f41d9c369"
            }
        );
        modelBuilder.Entity<CollectionType>().HasData(
            new CollectionType
            {
                Id = new Guid("3650af9b-3872-47fe-ad5d-111aaa194f6c"),
                Type = "Wish list"
            },
            new CollectionType
            {
                Id = new Guid("3c3b723c-f749-4162-a73f-03cbe163944d"),
                Type = "Shopping cart"
            }
        );

        //настройка значений по умолчанию
        modelBuilder.Entity<Price>().Property(p => p.StartDate).HasDefaultValue(DateTime.UnixEpoch);

        //настройка первичных ключей
        modelBuilder.Entity<Price>().HasKey(p => new { p.PricedGameId, p.StartDate });
        modelBuilder.Entity<Discount>().HasKey(d => new { d.DiscountedGameId, d.StartDate });
        modelBuilder.Entity<Collection>().HasKey(c => new { c.CollectionedGameId, c.CollectionerId, c.TypeId });
        modelBuilder.Entity<RefreshToken>().HasKey(rt => rt.Token);

        //настройка альтернативных ключей
        modelBuilder.Entity<CollectionType>().HasAlternateKey(ct => ct.Type);

        //настройка внешних ключей
        modelBuilder.Entity<Game>().HasOne(g => g.Developer).WithMany(d => d.DeveloperGames).HasForeignKey("DeveloperId");
        modelBuilder.Entity<Game>().HasOne(g => g.Publisher).WithMany(d => d.PublisherGames).HasForeignKey("PublisherId");
        modelBuilder.Entity<Price>().HasOne(p => p.PricedGame).WithMany(g => g.Prices).HasForeignKey(p => p.PricedGameId);
        modelBuilder.Entity<Discount>().HasOne(d => d.DiscountedGame).WithMany(g => g.Discounts).HasForeignKey(d => d.DiscountedGameId);
        modelBuilder.Entity<Collection>().HasOne(c => c.Collectioner).WithMany(u => u.Collections).HasForeignKey(c => c.CollectionerId);
        modelBuilder.Entity<Collection>().HasOne(c => c.CollectionedGame).WithMany(g => g.Collections).HasForeignKey(c => c.CollectionedGameId);
        modelBuilder.Entity<Collection>().HasOne(c => c.Type).WithMany(t => t.Collections).HasForeignKey(c => c.TypeId);
        modelBuilder.Entity<Purchase>().HasOne(p => p.Purchaser).WithMany(u => u.Purchases).HasForeignKey("PurchaserId");
        modelBuilder.Entity<Key>().HasOne(k => k.KeyPurchase).WithMany(p => p.Keys).HasForeignKey("PurchaseId");
        modelBuilder.Entity<Key>().HasOne(k => k.KeyGame).WithMany(g => g.Keys).HasForeignKey("GameId");
        modelBuilder.Entity<RefreshToken>().HasOne(rt => rt.User).WithMany(g => g.RefreshTokens).HasForeignKey("UserId");
    }
}