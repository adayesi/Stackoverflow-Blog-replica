using DecaBlog.Data.Repositories.Interfaces;
using DecaBlog.Models;
using DecaBlog.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using NinjaNye.SearchExtensions;
using System;

namespace DecaBlog.Data.Repositories.Implementations
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly DecaBlogDbContext _context;
        public ArticleRepository(DecaBlogDbContext context)
        {
            _context = context;
        }

        public async Task<List<Article>> GetArticlesByKeyword(string keyword)
        {
            return await _context.Articles.Include(x => x.Contributor).Include(x => x.ArticleTopic).Where(x => x.Keywords.Contains(keyword)).ToListAsync();
        }

        public IQueryable<ArticleTopic> GetAllArticlesAsync()
        {
            return _context.ArticleTopics
                .Include(x => x.ArticleList)
                .Include(u => u.Author)
                .ThenInclude(s => s.UserStacks)
                .ThenInclude(c => c.Stack)
                .Include(sq => sq.Author)
                .ThenInclude(s => s.UserSquads)
                .ThenInclude(x => x.Squad);
                //.Where(c => c.ArticleList.Count(x => x.IsPublished) >= 1);
        }

        public IQueryable<ArticleTopic> GetAllArticleTopicsWithPublishedArticles()
        {
            return _context.ArticleTopics
                .Include(x => x.ArticleList)
                .Include(u => u.Author)
                .ThenInclude(s => s.UserStacks)
                .ThenInclude(c => c.Stack)
                .Include(sq => sq.Author)
                .ThenInclude(s => s.UserSquads)
                .ThenInclude(x => x.Squad)
                .Where(c => c.ArticleList.Count(x => x.IsPublished) >= 1)
                .OrderByDescending(t => t.DateCreated).Reverse();
        }
        public async Task<bool> CreateContribution(Article model)
        {
            await _context.Articles.AddAsync(model);
            return await SaveChangesAsync();
        }

        public Task<List<Article>> GetArticlesAsync()
        {
            return _context.Articles.ToListAsync();
        }
        public async Task<Article> PublishArticleAsync(string articleId, string currentUserAsPublisherId)
        {
            var articleToBePublish = await _context.Articles.FirstOrDefaultAsync(x => x.Id == articleId);
            if (articleToBePublish == null) return null;
            articleToBePublish.PublisherId = currentUserAsPublisherId;
            articleToBePublish.IsPublished = true;
            articleToBePublish.DatePublished = DateTime.Now;
            return articleToBePublish;
        }

        public async Task<Article> UnPublishArticleAsync(string articleId, string currentUserAsPublisherId)
        {
            var articleToBeUnPublish = await _context.Articles.FirstOrDefaultAsync(x => x.Id == articleId);
            if (articleToBeUnPublish == null) return null;
            articleToBeUnPublish.PublisherId = currentUserAsPublisherId;
            articleToBeUnPublish.IsPublished = false;
            return articleToBeUnPublish;
        }
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<Article> GetContribution(string contributionId)
        {
            return await _context.Articles.Where(x => x.Id == contributionId).FirstOrDefaultAsync();
        }
        public Task<bool> RemoveContribution(Article contribution)
        {
            _context.Articles.Remove(contribution);
            return SaveChangesAsync();
        }
        public IQueryable<Article> GetArticleByContributorId(string contributorId)
        {
            return _context.Articles
                .Include(t => t.ArticleTopic)
                .Where(x => x.UserId == contributorId);
        }

        public IQueryable<Article> GetArticleByPublisherId(string publisherId)
        {
            return _context.Articles
                .Include(t => t.ArticleTopic)
                .Include(u => u.Contributor)
                .Where(x => x.PublisherId == publisherId);
        }
        public async Task<Article> GetContributionById(string articleId)
        {
            var article = await _context.Articles.Where(x => x.Id == articleId).FirstOrDefaultAsync();
            return article;
        }

        public async Task<bool> EditAsync(Article model)
        {
            _context.Articles.Update(model);
            return await SaveChangesAsync();
        }

        public IQueryable<Article> GetPublishedArticlesAsync()
        {
            return _context.Articles.Where(at => at.IsPublished == true)
                .Include(x => x.ArticleTopic);
        }

        public IQueryable<Article> GetPendingArticlesAsync()
        {
            return _context.Articles.Where(at => at.IsPublished == false)
                .Include(x => x.ArticleTopic);
        }

        public IQueryable<AllArticlesToReturnDto> GetAllArticlesByCategoryIdAsync(string categoryId)
        {
            return _context.ArticleTopics
              .Include(x => x.ArticleList)
              .Include(u => u.Author)
              .ThenInclude(s => s.UserStacks)
              .ThenInclude(c => c.Stack)
              .Include(sq => sq.Author)
              .ThenInclude(s => s.UserSquads)
              .ThenInclude(x => x.Squad)
              .Where(z => z.CategoryId == categoryId)
              .Select(x => new AllArticlesToReturnDto
              {
                  Abstract = x.Abstract,
                  CoverPhotoUrl = x.PhotoUrl,
                  Topic = x.Topic,
                  TopicId = x.Id,
                  Author = new AuthorDto
                  {
                      AuthorId = x.Author.Id,
                      AuthorPhotoUrl = x.Author.PhotoUrl,
                      FullName = $"{x.Author.FirstName} {x.Author.LastName}",
                      Stack = x.Author.UserStacks.FirstOrDefault().Stack.Name,
                      Squad = x.Author.UserSquads.FirstOrDefault().Squad.Name
                  }
              });
        }

        public IQueryable<Article> GetArticlesByTopicId(string topicId)
        {
            return _context.Articles.Where(article => article.ArticleTopicId == topicId);
        }

        public async Task<bool> AddLikeToArticleTopic(UserLike model)
        {
            await _context.UserLikes.AddAsync(model);
            return await SaveChangesAsync();
        }

        public async Task<bool> CheckIfUserLikeArticleTopic(User user, string topicId)
        {
            UserLike res = await _context.UserLikes.Where(u => u.LikerId == user.Id && u.TopicId == topicId).FirstOrDefaultAsync();
            if (res == null) return false;
            return true;
        }

        public async Task<int> GetTotalLikes(string topicId)
        {
            return await _context.UserLikes.CountAsync(u => u.TopicId == topicId);
        }
        public async Task<bool> RemoveLikeFromArticleTopic(UserLike model)
        {
             _context.UserLikes.Remove(model);
            return await SaveChangesAsync();

        }
        public async Task<Dictionary<int, int>> GetArticlesByMonth(int year)
        {
            var articles = (from article in GetAllArticlesAsync()
                            where article.DateCreated.Year == year
                            group article by article.DateCreated.Month into newArticles
                            select new { Key = newArticles.Key, Count = newArticles.Count() })
                            .ToDictionary(x => x.Key, y => y.Count);
            await Task.CompletedTask;
            return articles;
        }
    }
}