using Flashcards.Application.Common.Abstractions;
using MediatR;

namespace Flashcards.Application.Flashcards.Commands;

public record DeleteFlashcardCommand(int Id) : IRequest<int?>;
public class DeleteFlashcardCommandHandler(IFlashcardsRepository repository) : IRequestHandler<DeleteFlashcardCommand, int?>
{
    public async Task<int?> Handle(DeleteFlashcardCommand command, CancellationToken cancellationToken)
    {
        var res = await repository.DeleteFlashcard(command.Id);
        return res;
    }
}