using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Repositories;

namespace Infra.Database.Repositories;

public class CapsuleRepository : ICapsuleRepository
{
    private readonly AppDbContext _dbContext;

    public CapsuleRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> CreateCapsule(Capsule capsule)
    {
        await _dbContext.Capsules.AddAsync(capsule);
        await _dbContext.SaveChangesAsync();

        return capsule.Id;
    }

    public async Task UpdateCapsule(Capsule capsule)
    {
        _dbContext.Capsules.Update(capsule);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Capsule?> GetCapsuleById(int capsuleId)
    {
        var existingCapsule = await _dbContext.Capsules
            .Include(x => x.Specification)
            .Include(x => x.Category)
            .FirstOrDefaultAsync(x => x.Id == capsuleId);

        return existingCapsule;
    }

    public async Task DeleteCapsuleById(int capsuleId)
    {
        var existingCapsule = await _dbContext.Capsules.FindAsync(capsuleId);

        _dbContext.Capsules.Remove(existingCapsule!);
        await _dbContext.SaveChangesAsync();
    }
}
