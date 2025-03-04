using Flashcards.Application.Common.Abstractions;
using Flashcards.Application.Flashcards;
using Flashcards.Domain;
using Microsoft.EntityFrameworkCore;

namespace Flashcards.Infrastructure;

public class FlashcardsRepository(FlashcardsDbContext context) : IFlashcardsRepository
{
    public async Task<IEnumerable<FlashcardDto>> GetAllFlashcards()
    {
        return await context.Flashcards.Select(x => new FlashcardDto(x.Id, x.CreatedAt, x.UpdatedAt, x.DeckId, x.Question, x.Answer)).AsNoTracking().ToListAsync();
    }

    public async Task<FlashcardDto?> GetFlashcardById(int id)
    {
        return await context.Flashcards.AsNoTracking().Where(x => x.Id == id).Select(x => new FlashcardDto(x.Id, x.CreatedAt, x.UpdatedAt, x.DeckId, x.Question, x.Answer)).FirstOrDefaultAsync();
    }
    public async Task<FlashcardDto?> CreateFlashcard(FlashcardRequestDto flashcardRequestDto)
    {
        var flashcardToAdd = new Flashcard(flashcardRequestDto.DeckId, flashcardRequestDto.Question, flashcardRequestDto.Answer);
        
        var result = await context.Flashcards.AddAsync(flashcardToAdd);
        await context.SaveChangesAsync();
        
        if (result.Entity.Id == 0) return null;
        
        return new FlashcardDto(result.Entity.Id, result.Entity.CreatedAt, result.Entity.UpdatedAt, result.Entity.DeckId, result.Entity.Question, result.Entity.Answer);
    }
    
    public async Task<FlashcardDto?> UpdateFlashcard(FlashcardDto flashcard)
    {
        var flashcardToUpdate = await context.Flashcards.FirstOrDefaultAsync(f => f.Id == flashcard.Id);
        
        if (flashcardToUpdate == null) return null;
        
        flashcardToUpdate.DeckId = flashcard.DeckId;
        flashcardToUpdate.Answer = flashcard.Answer;
        flashcardToUpdate.Question = flashcard.Question;
        
        context.Update(flashcardToUpdate);
        
        await context.SaveChangesAsync();
        
        return new FlashcardDto(flashcardToUpdate.Id, flashcardToUpdate.CreatedAt, flashcardToUpdate.UpdatedAt, flashcardToUpdate.DeckId, flashcardToUpdate.Question, flashcardToUpdate.Answer);
    }
    
    public async Task<int?> DeleteFlashcard(int id)
    {
        var flashcard = await context.Flashcards.FirstOrDefaultAsync(x => x.Id == id);
        if (flashcard == null) return null;
        context.Flashcards.Remove(flashcard);
        return await context.SaveChangesAsync();
    }
}