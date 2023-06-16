using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace GamesStoreWebApi.Models;
public class ApplicationContext : IdentityDbContext<IdentityUser>
{
      public DbSet<Game> Games { get; set; }
      public DbSet<Company> Companies { get; set; }
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

            //настройка внешних ключей
            modelBuilder.Entity<Game>().HasOne(g => g.Developer).WithMany(d => d.DeveloperGames).HasForeignKey("DeveloperId");
            modelBuilder.Entity<Game>().HasOne(g => g.Publisher).WithMany(d => d.PublisherGames).HasForeignKey("PublisherId");
            modelBuilder.Entity<Price>().HasOne(p => p.PricedGame).WithMany(g => g.Prices).HasForeignKey(p => p.PricedGameId);
            modelBuilder.Entity<Discount>().HasOne(d => d.DiscountedGame).WithMany(g => g.Discounts).HasForeignKey(d => d.DiscountedGameId);   
      }
}