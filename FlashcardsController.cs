using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace flashcards_api;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class FlashcardsController(FlashcardsDbContext context) : ControllerBase
{
  private readonly FlashcardsDbContext _context = context;

  [HttpGet]
  public async Task<IActionResult> GetAllFlashcards()
  {
    var flashcards = await _context.Flashcards.ToListAsync();
    return Ok(flashcards);
  }

  [HttpGet("{id:int}")]
  public async Task<IActionResult> GetFlashcardById(int id)
  {
    return Ok(await _context.Flashcards.FindAsync(id));
  }

  [HttpPost]
  public async Task<IActionResult> CreateFlashcard([FromBody] Flashcard flashcard)
  {
    try
    {
      _context.Flashcards.Add(flashcard);
      await _context.SaveChangesAsync();
      return Ok();
    }
    catch (Exception e)
    {
      Console.WriteLine(e);
      return BadRequest();
    }
  }

  [HttpPut("{id:int}")]
  public async Task<IActionResult> UpdateFlashcard([FromRoute] int id, [FromBody] Flashcard flashcard)
  {
    var existingFlashcard = await _context.Flashcards.AsTracking().SingleOrDefaultAsync(f => f.Id == id);
    
    if (existingFlashcard == null)
    {
      return NotFound();
    }
    
    existingFlashcard.Answer = flashcard.Answer;
    existingFlashcard.Question = flashcard.Question;
    
    await _context.SaveChangesAsync();
    return Ok();
  }

  [HttpDelete("{id:int}")]
  public async Task<IActionResult> DeleteFlashcard(int id)
  {
    var existingFlashcard = await _context.Flashcards.FindAsync(id);
    
    if (existingFlashcard == null)
    {
      return NotFound();
    }
    
    _context.Flashcards.Remove(existingFlashcard);
    await _context.SaveChangesAsync();
    return Ok();
  }
}