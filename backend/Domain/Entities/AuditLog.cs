namespace Domain.Entities;

public enum EventType
{
    Added,
    Removed,
}

public record AuditLog(
    int Quantity,
    EventType EventType) : BaseEntity
{
    public int CapsuleId { get; set; }
    public Capsule Capsule { get; set; } = default!;
}