using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace flashcards_api;

public class Flashcard : BaseEntity
{
    [ForeignKey("DeckId")]
    public int DeckId { get; set; }

    [Required] public string Question { get; set; } = null!;

    [Required] public string Answer { get; set; } = null!;
}