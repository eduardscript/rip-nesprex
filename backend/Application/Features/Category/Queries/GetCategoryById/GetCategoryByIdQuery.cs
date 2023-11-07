using Application.Exceptions;
using Application.Repositories;
using MediatR;

namespace Application.Features.Category.Queries.GetCategoryById;

public record GetCategoryByIdQuery(int CategoryId) : IRequest<Entities.Category>;

public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Entities.Category>
{
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Entities.Category> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetCategoryById(request.CategoryId);
        if (category is null)
        {
            throw new NotFoundException(nameof(Entities.Category), request.CategoryId);
        }

        return category;
    }
}
