namespace Domain.Entities.Common;

public abstract record BaseEntity()
{
    public int Id { get; init; }

    public DateTime CreatedAt { get; init; } = DateTime.Now;
    
    public DateTime? UpdatedAt { get; init; }
}