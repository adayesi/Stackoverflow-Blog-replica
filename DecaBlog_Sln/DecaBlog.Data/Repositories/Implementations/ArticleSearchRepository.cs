using DecaBlog.Data.Repositories.Interfaces;
using DecaBlog.Models;
using Microsoft.EntityFrameworkCore;
using NinjaNye.SearchExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DecaBlog.Models.DTO;

namespace DecaBlog.Data.Repositories.Implementations
{
    public class ArticleSearchRepository : IArticleSearchRepository
    {
        private readonly DecaBlogDbContext _context;

        public ArticleSearchRepository(DecaBlogDbContext context)
        {
            _context = context;
        }

        public IQueryable<ArticleTopic> ArticleByTopicName(IEnumerable<string> topicName)
        {
            return topicName.Count() < 1 ? _context.ArticleTopics.AsQueryable()
                : _context.ArticleTopics.Search(x => x.Topic.ToLower()).Containing(topicName.ToArray())
                .ToRanked().OrderByDescending(x => x.Hits).ThenByDescending(x => x.Item.DateCreated)
                .Select(x => x.Item);
        }

        public IQueryable<SearchArticleToReturnDto> ArticleSearchByKeyword(string[] searchKeywords)
        {
            var data = searchKeywords.Length < 1 ? _context.Articles.Include(article => article.ArticleTopic)
                .ThenInclude(articleTopic => articleTopic.Author).ThenInclude(author => author.UserSquads).ThenInclude(x => x.Squad)
                .Include(article => article.ArticleTopic)
                .ThenInclude(articleTopic => articleTopic.Author).ThenInclude(author => author.UserStacks).ThenInclude(x => x.Stack).OrderByDescending(x => x.DateCreated)
                .Select(x => new SearchArticleToReturnDto
                {
                    Topic = x.ArticleTopic.Topic,
                    TopicId = x.ArticleTopicId,
                    Abstract = x.ArticleTopic.Abstract,
                    CoverPhotoUrl = x.ArticleTopic.PhotoUrl,
                    Author = new AuthorDto()
                    {
                        AuthorId = x.ArticleTopic.UserId,
                        AuthorPhotoUrl = x.ArticleTopic.Author.PhotoUrl,
                        FullName = x.ArticleTopic.Author.FirstName+" "+x.ArticleTopic.Author.LastName,
                        Stack = x.ArticleTopic.Author.UserStacks.FirstOrDefault().Stack.Name,
                        Squad = x.ArticleTopic.Author.UserSquads.FirstOrDefault().Squad.Name,
                    }
                }).Distinct() :
                _context.Articles.Include(article => article.ArticleTopic)
                .ThenInclude(articleTopic => articleTopic.Author).ThenInclude(author => author.UserSquads).ThenInclude(x => x.Squad)
                .Include(article => article.ArticleTopic)
                .ThenInclude(articleTopic => articleTopic.Author).ThenInclude(author => author.UserStacks).ThenInclude(x => x.Stack)
                .Search(article => article.Keywords)
                .Containing(searchKeywords).ToRanked().OrderByDescending(x => x.Hits)
                .ThenByDescending(x => x.Item.DateCreated).Select(x => new SearchArticleToReturnDto
                {
                    Topic = x.Item.ArticleTopic.Topic,
                    TopicId = x.Item.ArticleTopicId,
                    Abstract = x.Item.ArticleTopic.Abstract,
                    CoverPhotoUrl = x.Item.ArticleTopic.PhotoUrl,
                    Author = new AuthorDto()
                    {
                        AuthorId = x.Item.ArticleTopic.UserId,
                        AuthorPhotoUrl = x.Item.ArticleTopic.Author.PhotoUrl,
                        FullName = x.Item.ArticleTopic.Author.FirstName +" " +x.Item.ArticleTopic.Author.LastName,
                        Stack = x.Item.ArticleTopic.Author.UserStacks.FirstOrDefault().Stack.Name,
                        Squad = x.Item.ArticleTopic.Author.UserSquads.FirstOrDefault().Squad.Name,
                    }
                }).Distinct();
            return data;
        }

        public List<RelatedArticleToReturnDto> RelatedArticlesByKeyWord(string articleId, string[] searchKeywords)
        {
            var relatedArticle = new List<RelatedArticleToReturnDto>();

            var articleTopics = _context.ArticleTopics
                .Include(x => x.ArticleList)
                .Include(u => u.Author)
                .ThenInclude(s => s.UserStacks)
                .ThenInclude(c => c.Stack)
                .Include(sq => sq.Author)
                .ThenInclude(s => s.UserSquads)
                .ThenInclude(x => x.Squad).ToList()
               .Where(x => x.Id != articleId).ToList();

            var index = 0;
            foreach (var art in articleTopics)
            {
                var articles = art.ArticleList.Where(x =>
                    x.Keywords.Split(",").Select(itm => itm.Trim())
                        .Any(value => searchKeywords.Contains(value))).ToList();

                if (articles.Count > 0 && index <= 6)
                {
                    relatedArticle.Add(new RelatedArticleToReturnDto()
                    {
                      Topic = art.Topic,
                      TopicId = art.Id,
                      Abstract = art.Abstract,
                      CoverPhotoUrl = art.Author.PhotoUrl,
                      CreatedDate = art.DateCreated,
                      Author = new AuthorForDto()
                      {
                          AuthorId = art.UserId,
                          AuthorPhotoUrl = art.Author.PhotoUrl,
                          FullName = $"{art.Author.FirstName} {art.Author.LastName}"
                      }
                    });
                    index++;
                }
                else
                {
                    break;
                }
            }
            return relatedArticle.ToList();
        }



        public IQueryable<ArticleTopic> SearchArticleTopicByAuthorName(string[] authorName)
        {
            return authorName.Length < 1 ? _context.ArticleTopics
                .Include(x => x.Author)
                .ThenInclude(x => x.UserSquads).ThenInclude(x => x.Squad)
                .Include(x => x.Author)
                .ThenInclude(x => x.UserStacks).ThenInclude(x => x.Stack)
                .OrderBy(x => x.Author.FirstName)
                : _context.ArticleTopics
                .Include(x => x.Author)
                .ThenInclude(x => x.UserSquads).ThenInclude(x => x.Squad)
                .Include(x => x.Author)
                .ThenInclude(x => x.UserStacks).ThenInclude(x => x.Stack)
                .Search(x => x.Author.FirstName.ToLower(), x => x.Author.LastName.ToLower())
                .Containing(authorName).ToRanked()
                .OrderByDescending(x => x.Hits)
                .Select(x => x.Item);
        }
    }
}