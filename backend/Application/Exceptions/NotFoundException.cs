namespace Application.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string entity, object entityId) : base($"{entity} with ID:{entityId} was not found")
    {
    }
}