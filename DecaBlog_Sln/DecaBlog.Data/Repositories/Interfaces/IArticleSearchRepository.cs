using DecaBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DecaBlog.Models.DTO;

namespace DecaBlog.Data.Repositories.Interfaces
{
    public interface IArticleSearchRepository
    {
        IQueryable<ArticleTopic> ArticleByTopicName(IEnumerable<string> topicName);
        IQueryable<SearchArticleToReturnDto> ArticleSearchByKeyword(string[] searchKeywords);
        List<RelatedArticleToReturnDto> RelatedArticlesByKeyWord(string articleId, string[] searchKeywords);
        public IQueryable<ArticleTopic> SearchArticleTopicByAuthorName(string[] authorName);
    }
}