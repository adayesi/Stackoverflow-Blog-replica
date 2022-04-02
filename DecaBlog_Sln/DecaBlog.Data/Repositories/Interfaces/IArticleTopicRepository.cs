using DecaBlog.Models;
using DecaBlog.Models.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DecaBlog.Data.Repositories.Interfaces
{
    public interface IArticleTopicRepository
    {
        Task<bool> AddArticleTopic(ArticleTopic model);
        Task<List<ArticleTopic>> GetArticleByCategory(string id);
        Task<bool> DeleteArticle(ArticleTopic model);
        Task<ArticleTopic> GetArticleById(string id);
        Task<bool> EditAsync(ArticleTopic model);
        IQueryable<ArticleTopic> GetPopularArticleTopics();
        Task<bool> SaveChanges();
        IQueryable<ArticleTopic> GetArticleTopics();
        IQueryable<AllArticlesToReturnDto> GetArticlesByAuthor(User author);
    }
}
