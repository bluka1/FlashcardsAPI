using FlashcardsApi;
using Microsoft.EntityFrameworkCore;

namespace flashcards_api;

public class FlashcardsDbContext(DbContextOptions<FlashcardsDbContext> options) : DbContext(options)
{
    public DbSet<Flashcard> Flashcards => Set<Flashcard>();
}