using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Database.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _dbContext;

    public CategoryRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> CreateCategory(Category category)
    {
        await _dbContext.Categories.AddAsync(category);
        await _dbContext.SaveChangesAsync();

        return category.Id;
    }

    public async Task UpdateCategory(Category category)
    {
        _dbContext.Categories.Update(category);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<Category>> GetAllCategories()
    {
        return await _dbContext.Categories
            .Include(x => x.Capsules)
            .ToListAsync();
    }

    public async Task<Category?> GetCategoryById(int categoryId)
    {
        var existingCategory = await _dbContext.Categories
            .Include(x => x.Capsules)
            .FirstOrDefaultAsync(x => x.Id == categoryId);

        if (!existingCategory!.Capsules.Any())
        {
            existingCategory.Capsules = null!;
        }

        return existingCategory;
    }

    public async Task DeleteCategoryById(int categoryId)
    {
        var existingCategory = await _dbContext.Categories.FindAsync(categoryId);

        _dbContext.Categories.Remove(existingCategory!);
        await _dbContext.SaveChangesAsync();
    }
}