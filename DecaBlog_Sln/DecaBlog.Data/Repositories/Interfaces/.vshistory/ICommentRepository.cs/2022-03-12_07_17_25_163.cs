using DecaBlog.Models;
using DecaBlog.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecaBlog.Data.Repositories.Interfaces
{
    public interface ICommentRepository
    {
        Task<bool> AddComment(UserComment comment);
        Task<UserComment> VoteComment(string commentId, string userId);
        Task<UserComment> UnvoteComment(string commentId, string userId);
        Task<UserComment> GetCommentById(string id);
        IQueryable<CommentToReturnDto> GetAllComments();
        IQueryable<UserComment> GetCommentsByCommenterId(string commenterId);
        Task<bool> DeleteCommentAsync(UserComment userComment);
        Task<bool> SaveChangesAsync();
    }
}