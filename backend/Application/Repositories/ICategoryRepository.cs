using Domain.Entities;

namespace Application.Repositories;

public interface ICategoryRepository
{
    Task<int> CreateCategory(Category category);

    Task UpdateCategory(Category category);

    Task<List<Category>> GetAllCategories();

    Task<Category?> GetCategoryById(int categoryId);

    Task DeleteCategoryById(int categoryId);
}