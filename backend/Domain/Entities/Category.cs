namespace Domain.Entities;

public record Category(
    string Name) : BaseEntity
{
    public IEnumerable<Capsule> Capsules { get; set; } = default!;
}