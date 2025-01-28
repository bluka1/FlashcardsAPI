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
  public async Task<IActionResult> GetAll()
  {
    var flashcards = await _context.Flashcards.ToListAsync();
    return Ok(flashcards);
  }

  [HttpPost]
  public async Task<IActionResult> CreateFlashcard([FromBody] Flashcard flashcard)
  {
    try
    {
      _context.Flashcards.Add(flashcard);_context.Flashcards.Add(flashcard);
      await _context.SaveChangesAsync();
      return Ok();
    }
    catch (Exception e)
    {
      Console.WriteLine(e);
      return BadRequest();
    }

    
  }
}