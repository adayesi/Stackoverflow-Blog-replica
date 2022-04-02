using DecaBlog.Models.DTO;
using System.Threading.Tasks;

namespace DecaBlog.Services.Interfaces
{
    public interface ICommentService
    {
        Task<CommentVoteDto> VoteComment(string commentId, string voteId);
        Task<CommentVoteDto> UnVoteComment(string commentId, string voteId);
        Task<PaginatedListDto<CommentDto>> GetCommentByCommenterIdAsync(string commenterId,int pageNumber, int perPage);
        Task<bool> DeleteCommentById(string id);
        Task<AddedCommentDto> AddComment(CommentToAddDto model, string topicId);
        Task<CommentToReturnDto> GetCommentById(string id);
        Task<PaginatedListDto<CommentToReturnDto>> GetCommentsByTopicId(string Topicid, int pageNumber, int perPage);
        Task<PaginatedListDto<CommentToReturnDto>> GetAllComments(int pageNumber, int perPage);

    }
}