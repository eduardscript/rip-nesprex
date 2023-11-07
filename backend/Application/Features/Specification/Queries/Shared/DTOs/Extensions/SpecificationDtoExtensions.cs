namespace Application.Features.Specification.Queries.Shared.DTOs.Extensions;

public static class SpecificationDtoExtensions
{
    public static SpecificationDto ToDto(this Entities.Specification specification)
    {
        return new(specification.Id, specification.Size, specification.Milliliters);
    }
}