using Microsoft.EntityFrameworkCore;

namespace GamesStoreWebApi.Models;
public class ApplicationContext : DbContext
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
            modelBuilder.Entity<Game>().HasOne(g => g.Developer).WithMany(d => d.DeveloperGames).HasForeignKey("DeveloperId");
            modelBuilder.Entity<Game>().HasOne(g => g.Publisher).WithMany(d => d.PublisherGames).HasForeignKey("PublisherId");

            modelBuilder.Entity<Game>().HasData(new Game{Id = Guid.NewGuid(), Title = "Stellaris", ReleaseDate = new DateTime(2016, 5, 9)});
            modelBuilder.Entity<Company>().HasData(new Company{Id = Guid.NewGuid(), Name = "Blizzard Entertainment"});
      }
}