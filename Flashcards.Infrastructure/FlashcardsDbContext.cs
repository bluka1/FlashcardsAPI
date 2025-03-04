using Microsoft.EntityFrameworkCore;
using Flashcards.Domain;

namespace Flashcards.Infrastructure;

public class FlashcardsDbContext : DbContext
{
    public FlashcardsDbContext(DbContextOptions<FlashcardsDbContext> options)
        : base(options) {}

    public DbSet<Flashcard> Flashcards { get; set; }

    public override int SaveChanges()
    {
        var entries = ChangeTracker.Entries<BaseEntity>()
            .Where(e => e.State == EntityState.Modified);
        
        foreach (var entry in entries)
        {
            entry.Entity.UpdatedAt = DateTime.UtcNow;
        }
        return base.SaveChanges(); 
    }
    
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<BaseEntity>()
            .Where(e => e.State == EntityState.Modified);
        
        foreach (var entry in entries)
        {
            entry.Entity.UpdatedAt = DateTime.UtcNow;
        }
        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Flashcard>()
            .HasKey(f => f.Id);
    }
}