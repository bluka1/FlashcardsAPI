using System.ComponentModel.DataAnnotations;

namespace Flashcards.Contracts;

public class FlashcardsRequest
{
    public int DeckId { get; set; }

    [Required] public string Question { get; set; } = null!;

    [Required] public string Answer { get; set; } = null!;
}