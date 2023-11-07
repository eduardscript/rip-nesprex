namespace Domain.Entities;

public enum Size
{
    Ristretto,
    Espresso,
    DoubleEspresso,
    GranLungo,
    Mug,
    Carafe,
}

public record Specification(
    Size Size,
    double Milliliters) : BaseEntity
{
    public int CapsuleId { get; init; }
    public Capsule Capsule { get; init; } = default!;
}