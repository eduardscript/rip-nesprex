using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Database.Repositories;

public class SpecificationRepository : ISpecificationRepository
{
    private readonly AppDbContext _dbContext;

    public SpecificationRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Specification?> GetSpecificationBySize(Size size)
    {
        return await _dbContext.Specifications
            .FirstOrDefaultAsync(x => x.Size == size);
    }

    public async Task<List<Specification>> GetAllSpecifications()
    {
        return await _dbContext.Specifications.ToListAsync();
    }
}