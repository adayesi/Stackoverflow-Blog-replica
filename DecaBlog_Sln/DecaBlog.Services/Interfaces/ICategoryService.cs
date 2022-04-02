using DecaBlog.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DecaBlog.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<(bool, string, CategoryToReturnDto)> AddCategory(CategoryToAddDto model);
        Task<List<CategoryToReturnDto>> GetCategories(int pageNumber, int perPage);
        Task<CategoryToReturnDto> GetCategoryByIdAsync(string categoryId);
        Task<bool> DeleteCategory(string id);
        Task<(bool, string, CategoryToReturnDto)> UpdateCategoryByIdAsync(string categoryId, UpdateCategoryDto newCategory);
    }
}
