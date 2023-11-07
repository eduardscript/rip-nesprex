using Application.Exceptions;
using Application.Features.Specification.Queries.Shared.DTOs;
using Application.Features.Specification.Queries.Shared.DTOs.Extensions;
using Application.Repositories;
using MediatR;

namespace Application.Features.Specification.Queries;

public record GetSpecificationBySizeQuery(string Size) : IRequest<SpecificationDto>;

public class GetSpecificationByIdQueryHandler : IRequestHandler<GetSpecificationBySizeQuery, SpecificationDto>
{
    private readonly ISpecificationRepository _specificationRepository;

    public GetSpecificationByIdQueryHandler(ISpecificationRepository specificationRepository)
    {
        _specificationRepository = specificationRepository;
    }

    public async Task<SpecificationDto> Handle(GetSpecificationBySizeQuery request, CancellationToken cancellationToken)
    {
        if (!Enum.TryParse(request.Size, out Entities.Size size))
        {
            throw new FormatException($"Invalid size: {request.Size}");
        }

        var specification = await _specificationRepository.GetSpecificationBySize(size);
        if (specification is null)
        {
            throw new NotFoundException(nameof(Entities.Category), request.Size);
        }

        return specification.ToDto();
    }
}