namespace Domain.Entities;

public record Capsule(
    string Name,
    string Country,
    string? Description) : BaseEntity
{
    public int CategoryId { get; init; }
    public Category Category { get; init; } = default!;

    public int SpecificationId { get; init; }
    public Specification Specification { get; init; } = default!;

    public IEnumerable<AuditLog>? AuditLogs { get; init; } = default!;
}