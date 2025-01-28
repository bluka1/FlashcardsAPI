using Microsoft.EntityFrameworkCore;

namespace flashcards_api;

public class FlashcardsDbContext : DbContext
{
    public FlashcardsDbContext(DbContextOptions<FlashcardsDbContext> options)
        : base(options)
    {
    }

    public DbSet<Flashcard> Flashcards { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Flashcard>()
            .HasKey(f => f.Id);
    }
}