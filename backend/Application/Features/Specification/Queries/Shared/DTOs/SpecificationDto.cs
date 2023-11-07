namespace Application.Features.Specification.Queries.Shared.DTOs;

public struct SpecificationDto
{
    public SpecificationDto(int id, Entities.Size size, double capacityInMl)
    {
        Id = id;
        Name = size.ToString();
        CapacityInMl = capacityInMl;
    }

    public int Id { get; init; }

    public string Name { get; init; }

    public double CapacityInMl { get; init; }
}

