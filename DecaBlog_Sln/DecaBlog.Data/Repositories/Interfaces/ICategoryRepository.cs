using DecaBlog.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DecaBlog.Data.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<bool> AddCategory(Category model);
        Category GetCategoryByCategoryName(string Name);
        Task<bool> SaveChanges();
        Task<Category> GetCategoryById(string categoryId);
        Task<bool> DeleteCategory(Category category);
        Task<bool> UpdateCategory(Category newCategory);
        IQueryable<Category> GetAllCategories();
    }
}
