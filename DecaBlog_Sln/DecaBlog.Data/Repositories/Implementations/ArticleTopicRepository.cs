using DecaBlog.Data.Repositories.Interfaces;
using DecaBlog.Models;
using DecaBlog.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DecaBlog.Data.Repositories.Implementations
{
    public class ArticleTopicRepository : IArticleTopicRepository
    {
        private readonly DecaBlogDbContext _context;
        public ArticleTopicRepository(DecaBlogDbContext context)
        {
            _context = context;
        }
        public IQueryable<ArticleTopic> GetArticleTopics()
        {
            return _context.ArticleTopics;
        }
        public async Task<bool> AddArticleTopic(ArticleTopic model)
        {
            await _context.ArticleTopics.AddAsync(model);
            return await SaveChanges();
        }
        public async Task<List<ArticleTopic>> GetArticleByCategory(string id)
        {
            var articleToReturn = await _context.ArticleTopics.Where(x => x.CategoryId == id).ToListAsync();
            return articleToReturn;
        }
        public async Task<bool> DeleteArticle(ArticleTopic model)
        {
            //Checking for the TopicId if it exist on the Table
            var articleTopic = _context.ArticleTopics.FirstOrDefault(x => x.Id == model.Id);
            if (articleTopic != null)
            {
                var articleContributions = _context.Articles.Where(x => x.ArticleTopicId == model.Id).ToList();
                _context.Articles.RemoveRange(articleContributions);
            }
            _context.ArticleTopics.Remove(model);
            return await SaveChanges();
        }
        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<ArticleTopic> GetArticleById(string id)
        {
            return await _context.ArticleTopics.Include(x => x.User)
               .Include(x => x.Category)
               .Include(x => x.ArticleList).ThenInclude(c=>c.Contributor)
               .Include(x =>x.UserLike)
               .Include(x=>x.UserComment)
               .Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        public async Task<bool> EditAsync(ArticleTopic model)
        {
            _context.ArticleTopics.Update(model);
            return await SaveChanges();
        }


        public IQueryable<ArticleTopic> GetPopularArticleTopics()
        {
            return _context.ArticleTopics
                   .OrderByDescending(x => x.UserComment.Count)
                   .Include(x => x.Author)
                   .ThenInclude(x => x.UserStacks)
                   .Where(y => y.ArticleList
                   .Count(y => y.IsPublished == true) >= 1);
        }

        public IQueryable<AllArticlesToReturnDto> GetArticlesByAuthor(User author)
        {
            return _context.ArticleTopics
            .Include(x => x.ArticleList)
            .Include(u => u.Author)
            .ThenInclude(s => s.UserStacks)
            .ThenInclude(c => c.Stack)
            .Include(sq => sq.Author)
            .ThenInclude(s => s.UserSquads)
            .ThenInclude(x => x.Squad)
            .Where(z => z.Author == author)
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
    }
}
