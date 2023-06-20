using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using GamesStoreWebApi.Models.Entities;

namespace GamesStoreWebApi.Models;
public class ApplicationContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public DbSet<Game> Games { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Purchase> Purchases { get; set; }
    public DbSet<Key> Keys { get; set; }
    public DbSet<Price> Prices { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<Collection> Collections { get; set; }
    public ApplicationContext()
    {

    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory());
        builder.AddJsonFile("appsettings.Development.json");
        var config = builder.Build();
        optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        optionsBuilder.LogTo(message => Console.WriteLine(message));
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //настройка Identity
        base.OnModelCreating(modelBuilder);

        //настройка значений по умолчанию
        modelBuilder.Entity<Price>().Property(p => p.StartDate).HasDefaultValue(DateTime.UnixEpoch);

        //настройка составных первичных ключей
        modelBuilder.Entity<Price>().HasKey(p => new {p.PricedGameId, p.StartDate});
        modelBuilder.Entity<Discount>().HasKey(d => new{d.DiscountedGameId, d.StartDate});
        modelBuilder.Entity<Collection>().HasKey(c => new {c.CollectionedGameId, c.CollectionerId, c.TypeId});

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
    }
}