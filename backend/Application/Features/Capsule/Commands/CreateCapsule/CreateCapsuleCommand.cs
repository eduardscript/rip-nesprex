using Application.Exceptions;
using Application.Repositories;
using MediatR;

namespace Application.Features.Capsule.Commands.CreateCapsule;

public record CreateCapsuleCommand(
    string CapsuleName,
    string Country,
    int CategoryId,
    Entities.Size Size,
    string? Description = null) : IRequest<int>;

public class CreateCapsuleCommandHandler : IRequestHandler<CreateCapsuleCommand, int>
{
    private readonly ICapsuleRepository _capsuleRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ISpecificationRepository _specificationRepository;

    public CreateCapsuleCommandHandler(
        ICapsuleRepository capsuleRepository,
        ICategoryRepository categoryRepository,
        ISpecificationRepository specificationRepository)
    {
        _capsuleRepository = capsuleRepository;
        _categoryRepository = categoryRepository;
        _specificationRepository = specificationRepository;
    }

    public async Task<int> Handle(CreateCapsuleCommand request, CancellationToken cancellationToken)
    {
        var existingCategory = await _categoryRepository.GetCategoryById(request.CategoryId);
        if (existingCategory is null)
        {
            throw new NotFoundException(nameof(Entities.Category), request.CategoryId);
        }

        var existingSpecification = await _specificationRepository.GetSpecificationBySize(request.Size);
        if (existingSpecification is null)
        {
            throw new NotFoundException(nameof(Entities.Size), request.Size);
        }

        var capsule = new Entities.Capsule(request.CapsuleName, request.Country, request.Description)
        {
            Category = existingCategory,
            Specification = existingSpecification,
        };

        await _capsuleRepository.CreateCapsule(capsule);

        return capsule.Id;
    }
}

