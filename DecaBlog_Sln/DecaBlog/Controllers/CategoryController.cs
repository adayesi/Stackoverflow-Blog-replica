using AutoMapper;
using DecaBlog.Commons.Helpers;
using DecaBlog.Models.DTO;
using DecaBlog.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DecaBlog.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService CategoryService, IMapper mapper)
        {
            _categoryService = CategoryService;
            _mapper = mapper;
        }

        [HttpGet("get-category-by-id")]
        [Authorize(Roles = "Admin, Editor, Decadev")]
        public async Task<IActionResult> GetCategoryById([FromQuery] string categoryId)
        {
            if (string.IsNullOrWhiteSpace(categoryId))
                return BadRequest(ResponseHelper.BuildResponse<object>(false, "Invalid request parametre", ModelState, null));
            var category = await _categoryService.GetCategoryByIdAsync(categoryId);
            if (category == null)
            {
                ModelState.AddModelError("Not found", "");
                return BadRequest(ResponseHelper.BuildResponse<object>(false, "Category does not exist", ModelState, null));
            }
            return Ok(ResponseHelper.BuildResponse<object>(true, "Category was successful retrieved", ResponseHelper.NoErrors, category));
        }

        [HttpGet("get-categories")]
        [Authorize(Roles = "Admin, Editor, Decadev")]
        public async Task<IActionResult> GetAllCategory([FromQuery] int page, int perPage)
        {
            var Response = await _categoryService.GetCategories(page, perPage);
            return Ok(ResponseHelper.BuildResponse<object>(true, "All categories successfully fetched", ResponseHelper.NoErrors, Response));
        }

        [HttpPost("add-category")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddCategory([FromBody]CategoryToAddDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ResponseHelper.BuildResponse<CategoryToReturnDto>(false, "Request is invalid, input valid request", ModelState, null));

            var result = await _categoryService.AddCategory(model);
            if (!result.Item1)
            {
                ModelState.AddModelError("Add Category", "Category already exist");
                return NotFound(ResponseHelper.BuildResponse<CategoryToReturnDto>(false, "Bad Request", ModelState, null));
            }
            return Ok(ResponseHelper.BuildResponse(true, "Category successfully added", ResponseHelper.NoErrors, result));
        }

        [HttpPut("update-category/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCategory(string id, UpdateCategoryDto model)
        {
            var category = await _categoryService.UpdateCategoryByIdAsync(id, model);
            if (!category.Item1)
            {
                ModelState.AddModelError(category.Item2, "");
                return Ok(ResponseHelper.BuildResponse<CategoryToReturnDto>(category.Item1, category.Item2, ModelState, category.Item3));
            }
            return Ok(ResponseHelper.BuildResponse<CategoryToReturnDto>(category.Item1, category.Item2, ResponseHelper.NoErrors, category.Item3));
        }

        [HttpDelete("delete-category")]
        [Authorize(Roles = "Admin, Editor, Decadev")]
        public async Task<IActionResult> DeleteCategory([FromQuery] string CategoryId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ResponseHelper.BuildResponse<bool>(false, "Invalid Request", ModelState, false));
            }
            var isDeleted = await _categoryService.DeleteCategory(CategoryId);
            if (!isDeleted)
            {
                ModelState.AddModelError("Delete Category", "Category not Deleted");
                return BadRequest(ResponseHelper.BuildResponse<bool>(false, "Category not Deleted", ModelState, false));
            }
            return Ok(ResponseHelper.BuildResponse<bool>(isDeleted, "Category Successfully Deleted", ResponseHelper.NoErrors, isDeleted));
        }
    }
}
