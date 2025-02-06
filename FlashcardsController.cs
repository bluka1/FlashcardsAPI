using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace flashcards_api;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class FlashcardsController(FlashcardsDbContext context, ILogger<FlashcardsController> logger) : ControllerBase
{
  private readonly FlashcardsDbContext _context = context;
  private readonly ILogger<FlashcardsController> _logger = logger;

  [HttpGet]
  public async Task<IActionResult> GetAllFlashcards()
  {
    _logger.LogInformation("Attempting to fetch flashcards");
    try 
    {
      var flashcards = await _context.Flashcards.ToListAsync();
      _logger.LogInformation($"Successfully retrieved {flashcards.Count} flashcards");
      return Ok(flashcards);
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Error fetching flashcards");
      return StatusCode(500, "Internal server error");
    }
    // var flashcards = await _context.Flashcards.ToListAsync();
    // return Ok(flashcards);
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

  // [HttpPut("{id:int}")]
  // public async Task<IActionResult> UpdateFlashcard([FromBody] Flashcard flashcard)
  // {
  //   var existingFlashcard = await _context.Flashcards.FindAsync(flashcard.Id);
  //   
  //   if (existingFlashcard == null)
  //   {
  //     return NotFound();
  //   }
  //   
  //   existingFlashcard.Answer = flashcard.Answer;
  //   existingFlashcard.Question = flashcard.Question;
  //   
  //   await _context.SaveChangesAsync();
  //   return Ok();
  // }

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