namespace Flashcards.Application.Flashcards;

public record FlashcardRequestDto(int DeckId, string Question, string Answer);