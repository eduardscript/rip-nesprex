using Application.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Features.Category.Commands.CreateCategory;

public record CreateCategoryCommand(string CategoryName) : IRequest<int>;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
{
    private readonly ICategoryRepository _categoryRepository;

    public CreateCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var newCategory = new Entities.Category(request.CategoryName);

        var insertedCategoryId = await _categoryRepository.CreateCategory(newCategory);

        return insertedCategoryId;
    }
}
