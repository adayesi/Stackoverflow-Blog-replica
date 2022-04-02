using DecaBlog.Data.Repositories.Implementations;
using DecaBlog.Data.Repositories.Interfaces;
using DecaBlog.Models.DTO;
using DecaBlog.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DecaBlog.Models;
using Microsoft.AspNetCore.Identity;
using DecaBlog.Helpers;

namespace DecaBlog.Services.Implementations
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly INotificationsService _notifications;

        public CommentService(ICommentRepository commentRepository, IMapper mapper, UserManager<User> userManager, INotificationsService notifications)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
            _userManager = userManager;
            _notifications = notifications;
        }

        public async Task<CommentVoteDto> VoteComment(string CommentId, string VoterId)
        {
            var comment = await _commentRepository.GetCommentById(CommentId);
            if (comment == null) return null;
            var response = await _commentRepository.VoteComment(CommentId, VoterId);
            var res = new CommentVoteDto();
            if (response != null)
            {
                res.CommentId = CommentId;
                res.Vote = response.Votes.Count(x => x.CommentId == CommentId);
                return res;
            }
            return null;
        }
        public async Task<CommentVoteDto> UnVoteComment(string CommentId, string VoterId)
        {
            var comment = await _commentRepository.GetCommentById(CommentId);
            if (comment == null) return null;
            var response = await _commentRepository.UnvoteComment(CommentId, VoterId);
            var res = new CommentVoteDto();
            if (response != null)
            {
                res.CommentId = CommentId;
                res.Vote = response.Votes.Count(x => x.CommentId == CommentId);
                return res;
            }
            return null;
        }

        public async Task<AddedCommentDto> AddComment(CommentToAddDto model, string topicId)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            //Mapping the comment
            var commentMapper = _mapper.Map<UserComment>(model);
            commentMapper.CommenterId = user.Id;
            commentMapper.CommentText = model.CommentText;
            commentMapper.TopicId = topicId;
            commentMapper.User = user;
            //Add the comment to the Database
            var saveComment = await _commentRepository.AddComment(commentMapper);
            if (!saveComment)
                return null;

            await _notifications.AddCommentNotification(user, topicId);
            return _mapper.Map<AddedCommentDto>(commentMapper);
        }

        public async Task<CommentToReturnDto> GetCommentById(string id)
        {
            var comment = await _commentRepository.GetCommentById(id);

            if (comment == null)
                return null;


            CommentToReturnDto returnDto = new CommentToReturnDto()
            {
                Id = comment.Id,
                TopicId = comment.TopicId,
                CommentText = comment.CommentText,
                Votes = comment.Votes.Count,
                CreatedAt = comment.DateCreated,
                UpdatedAt = comment.DateUpdated,
                Commentator = new CommentatorDto
                {
                    CommentatorId = comment.CommenterId,
                    FullName = $"{comment.User.FirstName} {comment.User.LastName}"
                }
            };
            return returnDto;
        }


        public async Task<PaginatedListDto<CommentToReturnDto>> GetCommentsByTopicId(string Topicid , int  pageNumber, int perPage)
        {
           var comments = _commentRepository.GetCommentsByTopicId(Topicid);

            if (comments == null)
                 return null;

            List<CommentToReturnDto> commentsToRetrun = new List<CommentToReturnDto>();

            foreach (var comment in comments)
            {
                commentsToRetrun.Add(new CommentToReturnDto
                {

                    Id = comment.Id,
                    TopicId = comment.TopicId,
                    CommentText = comment.CommentText,
                    Votes = comment.Votes.Count,
                    CreatedAt = comment.DateCreated,
                    UpdatedAt = comment.DateUpdated,
                    Commentator = new CommentatorDto
                    {
                        CommentatorId = comment.CommenterId,
                        PhotoUrl = comment.User.PhotoUrl,
                        FullName = $"{comment.User.FirstName} {comment.User.LastName}",
                    }
                }
                );
            }
            commentsToRetrun = commentsToRetrun.OrderByDescending(x => x.CreatedAt).ToList();
            var paginatedList = PagedList<CommentToReturnDto>.Paginate(commentsToRetrun, pageNumber, perPage);
            await Task.CompletedTask;
            return paginatedList;
        }

        public async Task<PaginatedListDto<CommentToReturnDto>> GetAllComments(int pageNumber, int perPage)
        {
            var comments = _commentRepository.GetAllComments();
            var paginatedList = PagedList<CommentToReturnDto>.Paginate(comments, pageNumber, perPage);
            await Task.CompletedTask;
            return paginatedList;
        }

        public Task<PaginatedListDto<CommentDto>> GetCommentByCommenterIdAsync(string commenterId, int pageNumber, int perPage)
        {
            var comment = _commentRepository.GetCommentsByCommenterId(commenterId);
            if (comment == null) return null;
            var commentToReturn = comment.Select(c => _mapper.Map<CommentDto>(c));
            var paginatedList = PagedList<CommentDto>.Paginate(commentToReturn, pageNumber, perPage: perPage);
            return Task.Run(() => paginatedList);
        }

        public async Task<bool> DeleteCommentById(string id)
        {
            var comment = await CommentAsync(id);
            if (comment is null) return false;
            return await _commentRepository.DeleteCommentAsync(comment);
        }

        private async Task<UserComment> CommentAsync(string id)
        {
            return await _commentRepository.GetCommentById(id);
        }
    }
}
