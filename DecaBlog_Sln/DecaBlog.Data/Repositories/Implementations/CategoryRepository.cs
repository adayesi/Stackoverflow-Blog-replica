using DecaBlog.Data.Repositories.Interfaces;
using DecaBlog.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DecaBlog.Data.Repositories.Implementations
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DecaBlogDbContext _context;
        public CategoryRepository(DecaBlogDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddCategory(Category model)
        {
            await _context.Categories.AddAsync(model);

            return await SaveChanges();
        }

        public Category GetCategoryByCategoryName(string Name)
        {
            return _context.Categories.Where(x => x.Name == Name.Trim().ToLower()).FirstOrDefault();
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Category> GetCategoryById(string categoryId)
        {
            return await _context.Categories.Where(x => x.Id == categoryId).FirstOrDefaultAsync();
        }

        public IQueryable<Category> GetAllCategories()
        {
            return  _context.Categories.OrderBy(x => x.Name).AsQueryable();
        }

        public async Task<bool> DeleteCategory(Category category)
        {
            _context.Categories.Remove(category);
            return await SaveChanges();
        }

        public async Task<bool> UpdateCategory(Category newCategory)
        {
            _context.Categories.Update(newCategory);
            return await SaveChanges();
        }
    }
}
