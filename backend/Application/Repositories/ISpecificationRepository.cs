namespace Application.Repositories;

public interface ISpecificationRepository
{
    Task<Entities.Specification?> GetSpecificationBySize(Entities.Size size);

    Task<List<Entities.Specification>> GetAllSpecifications();
}