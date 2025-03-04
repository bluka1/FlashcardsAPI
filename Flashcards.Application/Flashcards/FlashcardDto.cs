namespace Flashcards.Application.Flashcards;

public record FlashcardDto(int Id, DateTime CreatedAt, DateTime? UpdatedAt, int DeckId, string Question, string Answer);