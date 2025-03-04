using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flashcards.Domain;

public class Flashcard : BaseEntity
{
    public Flashcard(int deckId, string question, string answer)
    {
        DeckId = deckId;
        Question = question;
        Answer = answer;
    }
    
    [ForeignKey("DeckId")]
    public int DeckId { get; set; }

    [Required] public string Question { get; set; } = null!;

    [Required] public string Answer { get; set; } = null!;
}