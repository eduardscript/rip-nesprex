using Application.Features.Specification.Queries.Shared.DTOs;
using Application.Features.Specification.Queries.Shared.DTOs.Extensions;
using Application.Repositories;
using MediatR;

namespace Application.Features.Specification.Queries.GetAllSpecifications;

public record GetAllSpecificationsQuery : IRequest<IEnumerable<SpecificationDto>>;

public class GetAllSpecificationsQueryHandler : IRequestHandler<GetAllSpecificationsQuery, IEnumerable<SpecificationDto>>
{
    private readonly ISpecificationRepository _specificationRepository;

    public GetAllSpecificationsQueryHandler(ISpecificationRepository specificationRepository)
    {
        _specificationRepository = specificationRepository;
    }

    public async Task<IEnumerable<SpecificationDto>> Handle(GetAllSpecificationsQuery request, CancellationToken cancellationToken)
    {
        var specifications = await _specificationRepository.GetAllSpecifications();

        return specifications.Select(x => x.ToDto());
    }
}
