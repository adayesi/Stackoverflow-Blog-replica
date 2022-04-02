using DecaBlog.Commons.Parameters;
using DecaBlog.Models;
using DecaBlog.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DecaBlog.Services.Interfaces
{
    public interface IArticleService
    {
        Task<List<ArticleByKeywordDto>> GetArticleByKeyword(string keyword);
        Task<List<RelatedArticleToReturnDto>> GetRelatedArticle(string articleId);
        Task<PaginatedListDto<PublishedContributionToReturnDto>> GetPublishedArticlesAsync(int pageNumber, int perPage);
        Task<PaginatedListDto<PendingContributionToReturnDto>> GetPendingArticlesAsync(int pageNumber, int perPage);
        Task<CreatedContributionDTO> CreateContribution(AddContributionDTO model, string topicId, User user);
        Task<PaginatedListDto<ArticlesToReturnByPublisherIdDto>> GetArticleByPublisherId(string publisherId, int pageNumber, int perPage);
        Task<PaginatedListDto<ArticlesToReturnByContributorIdDto>> GetArticleByContributorId(string contributorId, int pageNumber, int perPage);
        Task<bool> DeleteContribution(string contributionId, string userId);
        Task<PublisUnPublishArticleResponseDto> UnPublishArticleAsync(string articleId);
        Task<PaginatedListDto<AllArticlesToReturnDto>> GetAllArticlesAsync(ArticleRequestParameter parameters);
        Task<PaginatedListDto<AllArticlesToReturnDto>> GetAllArticleTopicsWithPublishedArticles(ArticleRequestParameter parameters);
        Task<PublisUnPublishArticleResponseDto> PublishArticleAsync(string articleId);
        Task<PublisUnPublishArticleResponseDto> CheckArtictleTopic(string ArticleTopic);
        Task<UpdatedContributionDto> UpdateContribution(ContributionToUpdateDto model, string articleId, string userId);
        Task<PaginatedListDto<AllArticlesToReturnDto>> GetAllArticlesByCategoryIdAsync(string Id, ArticleRequestParameter parameters);
        Task<bool> AddLikeToArticleTopic(User user, string topicId);
        Task<int> GetTotalArticleLikes(string topicId);
        Task<bool> RemoveLikeFromArticleTopic(User user, string topicId);
        Task<PaginatedListDto<ArticlesToReturnDto>> GetPopularArticleTopics(int pageNumber, int perPage);
        Task<Dictionary<string,int>> ArticlesPublishedByMonth(int year);
        Task<PaginatedListDto<AllArticlesToReturnDto>> GetArticleListByAuthorAsync(string authorId, ArticleRequestParameter parameters);
    }
}