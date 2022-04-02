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
    public class ArticleSearchController : ControllerBase
    {
        private readonly IArticleSearchService _articleSearchService;

        public ArticleSearchController(IArticleSearchService articleSearchService)
        {
            _articleSearchService = articleSearchService;
        }

        [HttpGet("search-by-topic-name")]
        public async Task<IActionResult> ArticleByTopicName([FromQuery] ArticleByTopicNameDto model)
            => Ok(ResponseHelper.BuildResponse(true, "Articles By Topic Name", ResponseHelper.NoErrors, await _articleSearchService.ArticleByTopicName(model)));

        [HttpGet("search-by-keyword")]
        public async Task<IActionResult> ArticleByKeyword([FromQuery] SearchArticleByKeywordSearchParams model)
        {
            var result = await _articleSearchService.ArticleBySearchKeyword(model.KeyWords, model.PageNumber, model.PerPage);

            return Ok(ResponseHelper.BuildResponse<object>(true, "Related articles successfully retrieved", ResponseHelper.NoErrors, result));

        }
        [HttpGet("search-by-author")]
        public async Task<IActionResult> SearchArticleTopicByAuthorName([FromQuery]ArticleTopicNameSearchParams model)
            => Ok(ResponseHelper.BuildResponse(true, "success", ResponseHelper.NoErrors, await _articleSearchService.SearchArticleTopicByAuthor(model.AuthorName, model.PageNumber, model.PerPage)));
    }
}
