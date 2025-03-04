using Flashcards.Application.Common.Abstractions;
using MediatR;

namespace Flashcards.Application.Flashcards.Queries;

public record GetFlashcardByIdQuery(int Id) : IRequest<FlashcardDto?>;

public class GetFlashcardByIdQueryHandler(IFlashcardsRepository repository) : IRequestHandler<GetFlashcardByIdQuery, FlashcardDto?>
{
    public async Task<FlashcardDto?> Handle(GetFlashcardByIdQuery request, CancellationToken cancellationToken)
    {
        var flashcard = await repository.GetFlashcardById(request.Id);
        return flashcard;
    }
}