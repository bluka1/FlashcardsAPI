using Flashcards.Application.Common.Abstractions;
using MediatR;

namespace Flashcards.Application.Flashcards.Queries;

public record GetFlashcardsQuery() : IRequest<IEnumerable<FlashcardDto>>;

public class GetFlashcardsQueryHandler(IFlashcardsRepository repository) : IRequestHandler<GetFlashcardsQuery, IEnumerable<FlashcardDto>>
{

    public async Task<IEnumerable<FlashcardDto>> Handle(GetFlashcardsQuery request, CancellationToken cancellationToken)
    {
        return await repository.GetAllFlashcards();
    }
}
