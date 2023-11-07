using Application.Exceptions;
using Application.Repositories;
using MediatR;

namespace Application.Features.Capsule.Queries.GetCapsuleById;

public record GetCapsuleByIdQuery(int CapsuleId) : IRequest<CapsuleDto>;

public class GetCapsuleByIdQueryHandler : IRequestHandler<GetCapsuleByIdQuery, CapsuleDto>
{
    private readonly ICapsuleRepository _capsuleRepository;

    public GetCapsuleByIdQueryHandler(ICapsuleRepository capsuleRepository)
    {
        _capsuleRepository = capsuleRepository;
    }

    public async Task<CapsuleDto> Handle(GetCapsuleByIdQuery request, CancellationToken cancellationToken)
    {
        var capsule = await _capsuleRepository.GetCapsuleById(request.CapsuleId);
        if (capsule is null)
        {
            throw new NotFoundException(nameof(Entities.Capsule), request.CapsuleId);
        }

        return new(
            capsule.Id,
            capsule.Name,
            capsule.Description,
            capsule.Category.Name,
            capsule.Specification.Size.ToString());
    }
}

public record CapsuleDto(
    int Id,
    string Name,
    string? Description,
    string CategoryName,
    string Size);