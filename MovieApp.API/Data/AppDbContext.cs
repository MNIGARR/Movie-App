using Microsoft.EntityFrameworkCore;
using MovieApp.API.Models;

namespace MovieApp.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Movie> Movies => Set<Movie>();
    public DbSet<Watchlist> Watchlists => Set<Watchlist>();
    public DbSet<SearchHistory> SearchHistories => Set<SearchHistory>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(u => u.Email).IsUnique();
            entity.HasIndex(u => u.Username).IsUnique();
        });

        modelBuilder.Entity<Watchlist>(entity =>
        {
            entity.HasIndex(w => new { w.UserId, w.MovieId }).IsUnique();

            entity.HasOne(w => w.User)
                  .WithMany(u => u.Watchlists)
                  .HasForeignKey(w => w.UserId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(w => w.Movie)
                  .WithMany(m => m.Watchlists)
                  .HasForeignKey(w => w.MovieId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<SearchHistory>(entity =>
        {
            entity.HasOne(s => s.User)
                  .WithMany(u => u.SearchHistories)
                  .HasForeignKey(s => s.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
