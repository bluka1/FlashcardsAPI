using Flashcards.Application.Common.Abstractions;
using MediatR;

namespace Flashcards.Application.Flashcards.Commands;

public record UpdateFlashcardCommand(FlashcardDto Flashcard) : IRequest<FlashcardDto?>;
public class UpdateFlashcardCommandHandler(IFlashcardsRepository repository) : IRequestHandler<UpdateFlashcardCommand, FlashcardDto?>
{
    public async Task<FlashcardDto?> Handle(UpdateFlashcardCommand command, CancellationToken cancellationToken)
    {
        var result = await repository.UpdateFlashcard(command.Flashcard);
        return result;
    }
}