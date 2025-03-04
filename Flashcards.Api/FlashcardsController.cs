using Flashcards.Application.Flashcards;
using Flashcards.Application.Flashcards.Commands;
using Microsoft.AspNetCore.Mvc;
using Flashcards.Contracts;
using Flashcards.Application.Flashcards.Queries;
using MediatR;

namespace Flashcards.Api;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class FlashcardsController(ISender mediator) : ControllerBase
{
  private readonly ISender _mediator = mediator;
  
  [HttpGet]
  public async Task<IActionResult> GetAllFlashcards()
  {
    var flashcards = await _mediator.Send(new GetFlashcardsQuery());
    if (!flashcards.Any()) return NotFound();
    return Ok(flashcards);
  }

  [HttpGet("{id:int}")]
  public async Task<ActionResult<FlashcardDto>> GetFlashcardById(int id)
  {
    var flashcard = await _mediator.Send(new GetFlashcardByIdQuery(id));
    if (flashcard == null) return NotFound();
    return Ok(flashcard);
  }

  [HttpPost]
  public async Task<ActionResult<FlashcardDto>> CreateFlashcard([FromBody] FlashcardsRequest flashcardsRequest)
  {
    try
    {
      var flashcard = await _mediator.Send(new CreateFlashcardCommand(new FlashcardRequestDto(flashcardsRequest.DeckId, flashcardsRequest
          .Question, flashcardsRequest.Answer)));
      if (flashcard == null) return BadRequest();
      return Ok(flashcard);
    }
    catch (Exception e)
    {
      Console.WriteLine(e);
      return BadRequest();
    }
  }

  [HttpPut("{id:int}")]
  public async Task<ActionResult<FlashcardDto>> UpdateFlashcard([FromRoute] int id, [FromBody] FlashcardDto flashcardDto)
  {
    var flashcard = await _mediator.Send(new UpdateFlashcardCommand(flashcardDto));
    if (flashcard == null) return NotFound();
    return Ok(flashcard);
  }

  [HttpDelete("{id:int}")]
  public async Task<IActionResult> DeleteFlashcard(int id)
  {
    var result = await _mediator.Send(new DeleteFlashcardCommand(id));
    if (result == null) return NotFound();
    return Ok();
  }
}