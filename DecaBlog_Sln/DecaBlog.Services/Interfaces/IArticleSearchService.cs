using DecaBlog.Models.DTO;
using System.Threading.Tasks;

namespace DecaBlog.Services.Interfaces
{
    public interface IArticleSearchService
    {
        Task<PaginatedListDto<SearchArticleToReturnDto>> ArticleByTopicName(ArticleByTopicNameDto model);
        Task<PaginatedListDto<SearchArticleToReturnDto>> ArticleBySearchKeyword(string searchKeyword, int pageNumber, int pageSize);
        public Task<PaginatedListDto<SearchArticleToReturnDto>> SearchArticleTopicByAuthor(string author, int pageNumber, int perPage);
    }
}
