using Flashcards.Application.Flashcards;

namespace Flashcards.Application.Common.Abstractions;

public interface IFlashcardsRepository
{
    public Task<IEnumerable<FlashcardDto>> GetAllFlashcards();
    public Task<FlashcardDto?> GetFlashcardById(int id);
    public Task<FlashcardDto?> CreateFlashcard(FlashcardRequestDto flashcard);
    public Task<FlashcardDto?> UpdateFlashcard(FlashcardDto flashcard);
    public Task<int?> DeleteFlashcard(int id);
}