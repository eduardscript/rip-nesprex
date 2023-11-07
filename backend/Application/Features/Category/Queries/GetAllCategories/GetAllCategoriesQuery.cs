using Application.Repositories;
using MediatR;

namespace Application.Features.Category.Queries.GetAllCategories;

public record GetAllCategoriesQuery : IRequest<List<Entities.Category>>;

public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, List<Entities.Category>>
{
    private readonly ICategoryRepository _categoryRepository;

    public GetAllCategoriesQueryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<List<Entities.Category>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _categoryRepository.GetAllCategories();

        return categories;
    }
}
