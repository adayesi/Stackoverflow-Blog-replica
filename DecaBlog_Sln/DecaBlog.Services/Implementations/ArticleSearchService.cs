using AutoMapper;
using DecaBlog.Data.Repositories.Interfaces;
using DecaBlog.Helpers;
using DecaBlog.Models;
using DecaBlog.Models.DTO;
using DecaBlog.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DecaBlog.Services.Implementations
{
    public class ArticleSearchService : IArticleSearchService
    {
        private readonly IArticleSearchRepository _articleSearchRepository;
        private readonly IArticleRepository _articleRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public ArticleSearchService(IArticleSearchRepository articleSearchRepository, IArticleRepository articleRepository, IMapper mapper, UserManager<User> userManager)
        {
            _articleSearchRepository = articleSearchRepository;
            _articleRepository = articleRepository;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<PaginatedListDto<SearchArticleToReturnDto>> ArticleByTopicName(ArticleByTopicNameDto model)
        {
            var queryKeyWords = model.TopicName == null ? new string[] { } : model.TopicName.Split(" ", StringSplitOptions.TrimEntries).Select(x => x.ToLower());
            var articlesByTopicName = _articleSearchRepository.ArticleByTopicName(queryKeyWords);
            var paginatedArticlesByTopicName = PagedList<ArticleTopic>.Paginate(articlesByTopicName, model.PageNumber, model.PerPage);
            var data = new List<SearchArticleToReturnDto>();
            foreach (var article in paginatedArticlesByTopicName.Data)
            {
                var allArticlesReturns = _articleRepository.GetAllArticlesAsync();
                var tags = new List<string>();
                var articles = allArticlesReturns.Where(c => c.Id == article.Id).SelectMany(x => x.ArticleList).Where(x => x.Keywords != null);
                foreach (var art in articles)
                {
                    var sp = art.Keywords?.Split(',', StringSplitOptions.TrimEntries);
                    foreach (var sd in sp!)
                        tags.Add(sd);
                }
                var author = await _userManager.FindByIdAsync(article.UserId);
                data.Add(new SearchArticleToReturnDto
                {
                    TopicId = article.Id,
                    Topic = article.Topic,
                    Abstract = article.Abstract,
                    CoverPhotoUrl = article.PhotoUrl,
                    Author = _mapper.Map<AuthorDto>(author),
                    Tags = new HashSet<string>(tags)
                });
            }
            return new PaginatedListDto<SearchArticleToReturnDto>
            {
                MetaData = paginatedArticlesByTopicName.MetaData,
                Data = data
            };
        }

        public async Task<PaginatedListDto<SearchArticleToReturnDto>> ArticleBySearchKeyword(string searchKeyword, int pageNumber, int pageSize)
        {
            var keyWords =  searchKeyword == null ? new string[]{} : searchKeyword.ToLower().Split(" ", StringSplitOptions.TrimEntries);
            var articleList = _articleSearchRepository.ArticleSearchByKeyword(keyWords);
            var pagedlist = PagedList<SearchArticleToReturnDto>.Paginate(articleList, pageNumber, pageSize);
            foreach (var item in pagedlist.Data)
            {
                var queryableArticles = _articleRepository.GetAllArticlesAsync();
                var articles = queryableArticles
                    .Where(c => c.Id == item.TopicId)
                    .SelectMany(x => x.ArticleList)
                    .Where(x => x.Keywords != null);
                var tags = new List<string>();
                foreach (var art in articles)
                {
                    var sp = art.Keywords?.Split(',', StringSplitOptions.TrimEntries);
                    foreach (var sd in sp!)
                        tags.Add(sd);
                }

                item.Tags = new HashSet<string>(tags);
            }

            await Task.CompletedTask;
            return pagedlist;
        }
        public async Task<PaginatedListDto<SearchArticleToReturnDto>> SearchArticleTopicByAuthor(string author, int pageNumber, int perPage)
        {
            var searchParams = author == null ? new string[] { } : author.ToLower().Split(" ", StringSplitOptions.TrimEntries);
            var articlesTopicsByAuthor = _articleSearchRepository.SearchArticleTopicByAuthorName(searchParams);
            var ArticleTopicToReturn = new List<SearchArticleToReturnDto>();
            var pagedList = PagedList<ArticleTopic>.Paginate(articlesTopicsByAuthor, pageNumber, perPage);
            foreach (var articleTopic in pagedList.Data)
            {
                var allArticlesReturns = _articleRepository.GetAllArticlesAsync();
                var tags = new List<string>();
                var articles = allArticlesReturns.Where(c => c.Id == articleTopic.Id).SelectMany(x => x.ArticleList).Where(x => x.Keywords != null);
                foreach (var article in articles)
                {
                    var sp = article.Keywords?.Split(',', StringSplitOptions.TrimEntries);
                    foreach (var sd in sp!)
                        tags.Add(sd);
                }
                User user = await _userManager.FindByIdAsync(articleTopic.Author.Id);
                ArticleTopicToReturn.Add(new SearchArticleToReturnDto
                {
                    TopicId = articleTopic.Id,
                    Topic = articleTopic.Topic,
                    Abstract = articleTopic.Abstract,
                    CoverPhotoUrl = articleTopic.PhotoUrl,
                    Tags = new HashSet<string>(tags),
                    Author = _mapper.Map<AuthorDto>(user)
                });
            }
            var result = new PaginatedListDto<SearchArticleToReturnDto>
            {
                MetaData = pagedList.MetaData,
                Data = ArticleTopicToReturn
            };
            return result;
        }
    }
}
