using System.Collections.Generic;
using DecaBlog.Data.Repositories.Interfaces;
using DecaBlog.Models;
using DecaBlog.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DecaBlog.Data.Repositories.Implementations
{
    public class CommentRepository: ICommentRepository
    {
        private readonly DecaBlogDbContext _context;

        public CommentRepository(DecaBlogDbContext context)
        {
            _context = context;
        }

        private async Task<CommentVote> CheckCommentVote(string commentId, string userId)
        {

            return await _context.CommentVotes.Where(x => x.CommentId == commentId && x.VoterId == userId).FirstOrDefaultAsync();

        }
        public async Task<UserComment> VoteComment(string commentId, string userId)
        {
            var vote = await CheckCommentVote(commentId, userId);
            if (vote != null) return null;
            await _context.CommentVotes.AddAsync(new CommentVote {CommentId = commentId, VoterId = userId});
            await _context.SaveChangesAsync();
            return await _context.UserComments
                         .Include(c => c.Votes)
                         .Where(x => x.Id == commentId).FirstOrDefaultAsync();

        }
        public async Task<UserComment> UnvoteComment(string commentId, string userId)
        {
            var vote = await CheckCommentVote(commentId, userId);
            if (vote == null) return null;
            _context.CommentVotes.Remove(vote);
            await _context.SaveChangesAsync();
            return await _context.UserComments
                .Include(c => c.Votes)
                .Where(x => x.Id == commentId).FirstOrDefaultAsync();
        }


        public IQueryable<UserComment> GetCommentsByCommenterId(string commenterId)
        {
            return _context.UserComments
                .Include(c => c.User)
                .Include(z => z.Votes)
                .Where(x => x.CommenterId == commenterId);
        }

        public IQueryable<CommentToReturnDto> GetAllComments()
        {
            return _context.UserComments.Include(x => x.User).Include(s => s.Votes).Select(x => new CommentToReturnDto
            {
                Commentator = new CommentatorDto
                {
                    CommentatorId = x.User.Id,
                    FullName = $"{x.User.FirstName} {x.User.LastName}"
                },
                CommentText = x.CommentText,
                CreatedAt = x.DateCreated,
                Id = x.Id,
                TopicId = x.TopicId,
                UpdatedAt = x.DateUpdated,
                Votes = x.Votes.Count()
            }) ;
        }

        public async Task<bool> AddComment(UserComment model)
        {
            await _context.UserComments.AddAsync(model);
            return await SaveChangesAsync();
        }

        public async Task<UserComment> GetCommentById(string id)
        {
            return await _context.UserComments.Include(x => x.User)
                                              .Include(x => x.Votes).Where(x => x.Id == id).SingleOrDefaultAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteCommentAsync(UserComment userComment)
        {
            _context.UserComments.Remove(userComment);
            return await SaveChangesAsync();
        }

        public IQueryable<UserComment> GetCommentsByTopicId(string TopicId)
        {
            return  _context.UserComments.Include(x => x.User)
                .Where(x => x.TopicId == TopicId);
        }


    }
}