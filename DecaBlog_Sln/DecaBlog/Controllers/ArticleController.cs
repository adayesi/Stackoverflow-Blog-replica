using DecaBlog.Commons.Helpers;
using DecaBlog.Models;
using System.Threading.Tasks;
using DecaBlog.Commons.Parameters;
using DecaBlog.Helpers;
using DecaBlog.Models.DTO;
using DecaBlog.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using DecaBlog.Data.Repositories.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace DecaBlog.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;
        private readonly IArticleTopicService _articleTopicService;
        private readonly UserManager<User> _userManager;

        public ArticleController(IArticleService articleService, IArticleTopicService articleTopicService, UserManager<User> userManager)
        {
            _articleService = articleService;
            _articleTopicService = articleTopicService;
            _userManager = userManager;
        }

        [HttpGet("get-articles-by-categoryid")]
        public async Task<IActionResult> GetAllArticleByCategoryId([FromQuery] ArticleRequestParameter parameters, string categoryId)
        {
            var articlesResponse = await _articleService.GetAllArticlesByCategoryIdAsync(categoryId, parameters);

            if (articlesResponse != null)
                return Ok(ResponseHelper.BuildResponse<object>(true, "All articles successfully fetched for this category", ResponseHelper.NoErrors, articlesResponse));
            ModelState.AddModelError("", "Articles not found");
            return NotFound(ResponseHelper.BuildResponse<object>(false, "No records found", ModelState, null));
        }

        [HttpGet("get-article-topics-with-published-articles")]
        public async Task<IActionResult> GetAllArticleTopicsWithPublishedArticles([FromQuery] ArticleRequestParameter parameters)
        {
            var articlesResponse = await _articleService.GetAllArticleTopicsWithPublishedArticles(parameters);
            if (articlesResponse != null)
                return Ok(ResponseHelper.BuildResponse<object>(true, "All articles successfully fetched", ResponseHelper.NoErrors, articlesResponse));
            ModelState.AddModelError("", "Article not found");
            return NotFound(ResponseHelper.BuildResponse<object>(false, "No records found", ModelState, null));
        }

        [HttpGet("get-article/{keyword}")]
        public async Task<IActionResult> GetArticleByKeyword(string keyword, [FromQuery] int pageNumber, [FromQuery] int perPage)
        {

            if (string.IsNullOrWhiteSpace(keyword))
            {
                ModelState.AddModelError("", "Bad articleId format");
                return BadRequest(ResponseHelper.BuildResponse<object>(false, "Empty keyword", ModelState, null));
            }
            keyword = keyword.ToLower();
            var result = await _articleService.GetArticleByKeyword(keyword);

            if (result != null)
                return Ok(ResponseHelper.BuildResponse<object>(true, "Articles Was Successfully retrieved", ResponseHelper.NoErrors, result));

            return NotFound(ResponseHelper.BuildResponse<object>(false, "Article was not found", ResponseHelper.NoErrors, null));
        }

        [HttpGet("article/get-published-articles")]
        [Authorize(Roles = "Admin, Editor, Decadev")]
        public async Task<IActionResult> GetPublishedArticles([FromQuery] int pageNumber, [FromQuery] int perPage)
        {
            var publishedArticles = await _articleService.GetPublishedArticlesAsync(pageNumber, perPage);
            if (publishedArticles == null)
            {
                ModelState.AddModelError("Not found", "");
                return NotFound(ResponseHelper.BuildResponse<object>(false, "No any published articles found", ModelState, null));
            }
            return Ok(ResponseHelper.BuildResponse<object>(true, "Successfully fetched all published aricles", ResponseHelper.NoErrors, publishedArticles));
        }

        [HttpGet("get-articles-by-author")]
        public async Task<IActionResult> GetArticlesByAuthorId([FromQuery] string authorId, [FromQuery] ArticleRequestParameter parameters)
        {
            var articlesResponse = await _articleService.GetArticleListByAuthorAsync(authorId, parameters);
            if (articlesResponse != null)
                return Ok(ResponseHelper.BuildResponse(true, "All articles successfully fetched", ResponseHelper.NoErrors, articlesResponse));
            ModelState.AddModelError("", "Article not found");
            return NotFound(ResponseHelper.BuildResponse<object>(false, "No records found", ModelState, null));
        }

        [HttpGet("get-popular-articles")]
        public async Task<IActionResult> GetPopularArticles([FromQuery] int pageNumber, int perPage)
        {
            var popularArticles = await _articleService.GetPopularArticleTopics(pageNumber, perPage);
            if (popularArticles == null)
            {
                ModelState.AddModelError("Not found", "");
                return NotFound(ResponseHelper.BuildResponse<object>(false, "No popular articles", ModelState, null));
            }
            return Ok(ResponseHelper.BuildResponse<object>(true, "Successfully fetched all popular aricles", ResponseHelper.NoErrors, popularArticles));
        }

        [HttpGet("article/articles-published-by-month")]
        [Authorize(Roles = "Admin, Editor")]
        public async Task<IActionResult> ArticlesPublishedByMonth([FromQuery] int year)
        {
            var numberOfArticlesPublished = await _articleService.ArticlesPublishedByMonth(year);
            if (numberOfArticlesPublished == null)
            {
                ModelState.AddModelError("Not found", "");
                return NotFound(ResponseHelper.BuildResponse<object>(false, "No articles published yet", ModelState, null));
            }
            return Ok(ResponseHelper.BuildResponse<object>(true, $"Total articles created for year {year}", ResponseHelper.NoErrors, numberOfArticlesPublished));
        }

        [HttpGet("get-pending-articles")]
        [Authorize(Roles = "Admin, Editor")]
        public async Task<IActionResult> GetPendingArticles([FromQuery] int pageNumber, [FromQuery] int perPage)
        {
            var pendingArticles = await _articleService.GetPendingArticlesAsync(pageNumber, perPage);
            if (pendingArticles == null)
            {
                ModelState.AddModelError("Not found", "");
                return NotFound(ResponseHelper.BuildResponse<object>(false, "No any pending articles found", ModelState, null));
            }
            return Ok(ResponseHelper.BuildResponse<object>(true, "Successfully fetched all pending aricles", ResponseHelper.NoErrors, pendingArticles));
        }

        [HttpGet("get-articles")]
        public async Task<IActionResult> GetAllArticles([FromQuery] ArticleRequestParameter parameters)
        {
            var articlesResponse = await _articleService.GetAllArticlesAsync(parameters);
            if (articlesResponse != null)
                return Ok(ResponseHelper.BuildResponse<object>(true, "All articles successfully fetched", ResponseHelper.NoErrors, articlesResponse));
            ModelState.AddModelError("", "Article not found");
            return NotFound(ResponseHelper.BuildResponse<object>(false, "No records found", ModelState, null));
        }

        //[Authorize(Roles = "Admin, Editor, Decadev")]
        [HttpGet("get-article-by-id/{articleId}")]
        public async Task<IActionResult> GetArticleById(string articleId)
        {
            if (string.IsNullOrEmpty(articleId))
                return BadRequest(NotFound(ResponseHelper.BuildResponse<object>(false, "Article was not found", ModelState, null)));
            var article = await _articleTopicService.GetArticleById(articleId);
            if (article == null)
                return NotFound(ResponseHelper.BuildResponse<object>(false, "Article was not found", ModelState, null));
            return Ok(ResponseHelper.BuildResponse<object>(true, "Article found successfully", null, article));
        }

        [Authorize(Roles = "Admin, Editor, Decadev")]
        [HttpGet("get-articles-by-contributorid/{contributorId}")]
        public async Task<IActionResult> GetArticleByContributorId(string contributorId, int pageNumber, int perPage)
        {
            if (contributorId.Length == 0)
                return BadRequest(ResponseHelper.BuildResponse<object>(false, "Bad input", ModelState, null));
            var response = await _articleService.GetArticleByContributorId(contributorId, pageNumber, perPage);
            if (response == null)
                return Ok(ResponseHelper.BuildResponse<object>(true, "No article matching this user", ModelState, null));
            return Ok(ResponseHelper.BuildResponse<object>(
               true, "Successfully fetched all contributions by contributionId", ModelState,
               await _articleService.GetArticleByContributorId(contributorId, pageNumber, perPage)));
        }

        [Authorize(Roles = "Admin, Editor, Decadev")]
        [HttpGet("get-articles-by-publisherid/{publisherId}")]
        public async Task<IActionResult> GetArticleByPublisherId(string publisherId, int pageNumber, int perPage)
        {
            if (publisherId.Length == 0)
                return BadRequest(ResponseHelper.BuildResponse<object>(false, "Bad input", ModelState, null));
            var response = await _articleService.GetArticleByPublisherId(publisherId, pageNumber, perPage);
            if (response == null)
                return Ok(ResponseHelper.BuildResponse<object>(true, "No article matching this user", ModelState, null));
            return Ok(ResponseHelper.BuildResponse<object>(
                true, "Successfully fetched all contributions by publisherId", ModelState,
                await _articleService.GetArticleByPublisherId(publisherId, pageNumber, perPage)));
        }

        [HttpGet("get-related-articles/{articleId}")]
        public async Task<IActionResult> GetReletaedArticles(string articleId)
        {
            if (string.IsNullOrEmpty(articleId))
                return BadRequest(ResponseHelper.BuildResponse<object>(false, "Bad input", ModelState, null));
            var response = await _articleService.GetRelatedArticle(articleId);
            if (response == null)
                return Ok(ResponseHelper.BuildResponse<object>(true, "No related articles for this article ", ModelState, null));
            return Ok(ResponseHelper.BuildResponse<object>(
                true, "Successfully fetched all related articles", ModelState,
                response));
        }

        [Authorize(Roles = "Admin, Editor, Decadev")]
        [HttpPost("add-article")]
        public async Task<IActionResult> CreateArticle([FromForm] AddArticleDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            if (_articleTopicService.CheckArtictleTopic(model.Topic))
            {
                ModelState.AddModelError("Already exist", "An article with the same topic already exist");
                return BadRequest(ResponseHelper.BuildResponse<string>(false, "An article with the same topic already exist", ModelState, null));
            }

            var photo = model.Photo;
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByIdAsync(userId);

            var response = await _articleTopicService.CreateNewArticle(model, photo, user);
            if (response == null)
            {
                ModelState.AddModelError("Failed To Add Article", "Article Addition Failed, Please Try again");
                return BadRequest(ResponseHelper.BuildResponse<string>(false, "Article failed to Add.", ModelState, null));
            }
            return Ok(ResponseHelper.BuildResponse<object>(true, "Article Added successful", ResponseHelper.NoErrors, response));
        }

        [Authorize(Roles = "Admin, Editor, Decadev")]
        [HttpPost("add-contribution/{topicId}")]
        public async Task<IActionResult> AddContribution([FromBody] AddContributionDTO model, [FromRoute]string topicId)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByIdAsync(userId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var response = await _articleService.CreateContribution(model, topicId, user);
            if (response == null)
            {
                ModelState.AddModelError("Failed To Add Article", "Article Addition Failed, Please Try again");
                return BadRequest(ResponseHelper.BuildResponse<string>(false, "Article failed to Add.", ModelState, null));
            }
            return Ok(ResponseHelper.BuildResponse<object>(true, "Contribution Added successful", ResponseHelper.NoErrors, response));
        }

       // [Authorize(Roles = "Editor, Decadev")]
        [HttpPost("like-article-topic/{topicId}")]
        public async Task<IActionResult> AddLikeToArticleTopic([FromRoute]string topicId)
        {

            if (!ModelState.IsValid)
                return BadRequest(ResponseHelper.BuildResponse<string>(false, "Something went wrong", ModelState, null));

            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByIdAsync(userId);
            var response = await _articleService.AddLikeToArticleTopic(user, topicId);

            if (!response)
            {
                ModelState.AddModelError("Operation Failed", "Failed to like article, Please Try again");
                return BadRequest(ResponseHelper.BuildResponse<string>(false, "User can only like an article once", ResponseHelper.NoErrors, null));
            }

            var articleTopicLikeInfo = new ArticleTopicLikeDto()
            {
                TopicId = topicId,
                TotalLikes = await _articleService.GetTotalArticleLikes(topicId)
            };

            return Ok(ResponseHelper.BuildResponse<object>(true, "Successfully liked the article", ResponseHelper.NoErrors, articleTopicLikeInfo));
        }

        [Authorize(Roles = "Editor, Decadev")]
        [HttpPost("unlike-article-topic/{topicId}")]
        public async Task<IActionResult> RemoveLikeFromArticleTopic(string topicId)
        {

            if (string.IsNullOrWhiteSpace(topicId))
                 return BadRequest(ResponseHelper.BuildResponse<string>(false, "Something went wrong", ModelState, null));

            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByIdAsync(userId);
            var response = await _articleService.RemoveLikeFromArticleTopic(user, topicId);
            if (!response)
            {
                ModelState.AddModelError("Failed", "Failed to unlike article");
                return BadRequest(ResponseHelper.BuildResponse<string>(false, "User can only unlike an article once", ResponseHelper.NoErrors, null));
            }

            var articleTopicLikeInfo = new ArticleTopicLikeDto()
            {
                TopicId = topicId,
                TotalLikes = await _articleService.GetTotalArticleLikes(topicId)
            };

            return Ok(ResponseHelper.BuildResponse<object>(true, "Successfully unliked the article", ResponseHelper.NoErrors, articleTopicLikeInfo));
        }

        [Authorize(Roles = "Editor, Decadev")]
        [HttpPut("update-contribution/{contributionId}")]
        public async Task<IActionResult> UpdateContribution([FromBody] ContributionToUpdateDto model, [FromRoute] string contributionId)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var response = await _articleService.UpdateContribution(model, contributionId, userId);
            if (response == null)
            {
                ModelState.AddModelError("Failed To Add Article", "Article Addition Failed, Please Try again");
                return BadRequest(ResponseHelper.BuildResponse<bool>(false, "Article failed to Add.", ModelState, false));
            }
            return Ok(ResponseHelper.BuildResponse<object>(true, "Article Updated successful", ResponseHelper.NoErrors, response));
        }

        [HttpPatch("publish-article/{articleId}")]
        public async Task<IActionResult> PublishArticle([FromRoute] string articleId)
        {
            if (string.IsNullOrWhiteSpace(articleId))
            {
                ModelState.AddModelError("", "Bad articleId format");
                return BadRequest(ResponseHelper.BuildResponse<object>(false, "Empty articleId", ModelState, null));
            }
            var articleResponse = await _articleService.PublishArticleAsync(articleId);
            if (articleResponse != null)
                return Ok(ResponseHelper.BuildResponse<object>(true, "Article successfully published", ResponseHelper.NoErrors, articleResponse));
            ModelState.AddModelError("", "Article not found");
            return NotFound(ResponseHelper.BuildResponse<object>(false, "An error occured", ModelState, null));
        }

        [HttpPatch("unpublish-article/{articleId}")]
        public async Task<IActionResult> UnPublishArticle([FromRoute] string articleId)
        {
            if (string.IsNullOrWhiteSpace(articleId))
            {
                ModelState.AddModelError("", "Bad articleId format");
                return BadRequest(ResponseHelper.BuildResponse<object>(false, "Empty articleId", ModelState, null));
            }
            var articleResponse = await _articleService.UnPublishArticleAsync(articleId);
            if (articleResponse != null)
                return Ok(ResponseHelper.BuildResponse<object>(true, "Article successfully unpublished", ResponseHelper.NoErrors, articleResponse));
            ModelState.AddModelError("", "Article not found");
            return NotFound(ResponseHelper.BuildResponse<object>(false, "An error occured", ModelState, null));
        }

        [Authorize(Roles = "Admin,Editor, Decadev")]
        [HttpDelete("delete-contribution/{contributionId}")]
        public async Task<IActionResult> DeleteContribution(string contributionId)
        {
            if (string.IsNullOrEmpty(contributionId))
                ResponseHelper.BuildResponse<bool>(false, "No article Found", null, false);
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var response = await _articleService.DeleteContribution(contributionId, userId);
            if (!response)
            {
                ModelState.AddModelError("Failed to Delete", "Failed to delete");
                return BadRequest(ResponseHelper.BuildResponse<string>(false, "Failed", ModelState, ""));
            }
            return Ok(ResponseHelper.BuildResponse<Article>(true, "Contribution was Successfully Deleted", null, null));
        }

        [HttpDelete("delete-article/{articleId}")]
        [Authorize(Roles = "Editor")]
        public async Task<IActionResult> DeleteArticle(string articleId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ResponseHelper.BuildResponse<object>(true, "Article was successful Deleted", ModelState, null));
            var response = await _articleTopicService.DeleteArticle(articleId);
            if (response == false)
            {
                ModelState.AddModelError("Failed to delete article", "Failed, Please Try again");
                return BadRequest(ResponseHelper.BuildResponse<string>(false, "Article failed to delete.", ModelState, null));
            }
            return Ok(ResponseHelper.BuildResponse<object>(true, "Article was successful Deleted", ResponseHelper.NoErrors, null));
        }
    }
}