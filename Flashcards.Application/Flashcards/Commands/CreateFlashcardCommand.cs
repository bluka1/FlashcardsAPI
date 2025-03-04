using Flashcards.Application.Common.Abstractions;
using MediatR;

namespace Flashcards.Application.Flashcards.Commands;

public record CreateFlashcardCommand(FlashcardRequestDto Flashcard) : IRequest<FlashcardDto?>;
public class CreateFlashcardCommandHandler(IFlashcardsRepository repository) : IRequestHandler<CreateFlashcardCommand, FlashcardDto?>
{
    public async Task<FlashcardDto?> Handle(CreateFlashcardCommand command, CancellationToken cancellationToken)
    {
        var flashcard = await repository.CreateFlashcard(command.Flashcard);
        return flashcard;
    }
}