using AutoMapper;
using DecaBlog.Data.Repositories.Interfaces;
using DecaBlog.Helpers;
using DecaBlog.Models;
using DecaBlog.Models.DTO;
using DecaBlog.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DecaBlog.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<(bool, string, CategoryToReturnDto)> AddCategory(CategoryToAddDto model)
        {
            var foundCategory = _categoryRepository.GetCategoryByCategoryName(model.Name.ToLower());
            if (foundCategory != null)
                return (false, "Category already exists", null);
            var category = _mapper.Map<Category>(model);
            category.Name = category.Name.Trim().ToLower();
            var response = await _categoryRepository.AddCategory(category);
            if (!response)
                return (false, "Not Found", null);
            var data = _mapper.Map<CategoryToReturnDto>(category);
            return (true, "Category Succesfully Added", data);
        }

        public Task<List<CategoryToReturnDto>> GetCategories(int pageNumber, int perPage)
        {
            var categories =  _categoryRepository.GetAllCategories();
            var paginatedList = PagedList<Category>.Paginate(categories, pageNumber, perPage).Data;
            var returnDto = new List<CategoryToReturnDto>();
            var result = _mapper.Map<List<CategoryToReturnDto>>(paginatedList);
            return Task.Run(() => result);
        }

        public async Task<CategoryToReturnDto> GetCategoryByIdAsync(string categoryId)
        {
            var category = await _categoryRepository.GetCategoryById(categoryId);
            if (category == null) return null;
            var data = _mapper.Map<CategoryToReturnDto>(category);
            return data;
        }
        public async Task<bool> DeleteCategory(string id)
        {
            var category = await _categoryRepository.GetCategoryById(id);
            if (category == null)
                return false;
            var deleteCategory = await _categoryRepository.DeleteCategory(category);
            if (!deleteCategory)
                return false;
            return true;
        }

        public async Task<(bool, string, CategoryToReturnDto)> UpdateCategoryByIdAsync(string categoryId, UpdateCategoryDto newCategory)
        {
            Category catToUpdate = await _categoryRepository.GetCategoryById(categoryId);

            if (catToUpdate == null) return (false, "Category does not exist", null);

            catToUpdate.Name = newCategory.Name;
            var response = await _categoryRepository.UpdateCategory(catToUpdate);

            if (!response) return (false, "An error occurred while updating category", null);

            return (true, "Category Updated successfully", _mapper.Map<CategoryToReturnDto>(catToUpdate));
        }
    }
}