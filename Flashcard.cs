namespace FlashcardsApi;

public class Flashcard
{
    public int Id { get; set; }
    public int DeckId { get; set; }
    public string Question { get; set; }
    public string Answer { get; set; }
}