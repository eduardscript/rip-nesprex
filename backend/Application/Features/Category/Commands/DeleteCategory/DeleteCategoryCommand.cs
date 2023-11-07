using Application.Repositories;
using MediatR;

namespace Application.Features.Category.Commands.DeleteCategory;

public record DeleteCategoryCommand(int CategoryId) : IRequest;

public class DeleteCategoryCommandCommandHandler : IRequestHandler<DeleteCategoryCommand>
{
    private readonly ICategoryRepository _categoryRepository;

    public DeleteCategoryCommandCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        await _categoryRepository.DeleteCategoryById(request.CategoryId);
    }
}
