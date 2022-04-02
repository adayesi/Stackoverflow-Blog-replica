using AutoMapper;

using DecaBlog.Commons.HttpClients.Firebase;
using DecaBlog.Data.Repositories.Implementations;
using DecaBlog.Data.Repositories.Interfaces;
using DecaBlog.Helpers;
using DecaBlog.Models;
using DecaBlog.Models.DTO;
using DecaBlog.Services.Interfaces;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace DecaBlog.Services.Implementations
{
    public class NotificationsService: INotificationsService
    {
        private readonly INotificationsRepository _notificationsRepository;
        private readonly IFirebaseClient _firebaseClient;
        private readonly IMapper _mapper;
        private readonly IArticleRepository _articleRepository;

        public NotificationsService(INotificationsRepository notificationsRepository, IFirebaseClient firebaseClient, IArticleRepository articleRepository)
        {
            _notificationsRepository = notificationsRepository;
            _firebaseClient = firebaseClient;
            _articleRepository = articleRepository;
        }

         

        public async Task AddCommentNotification(User commenter, string topicId)
        {
            var articlesOwners = _articleRepository.GetArticlesByTopicId(topicId).Select(x => x.UserId).Distinct();
            foreach(var article in articlesOwners)
            {
                var notification = new Notification
                {
                    ActivityId = Guid.NewGuid().ToString(),
                    UserId = article,
                    ActionPerformedBy = commenter.Id,
                    NoticeText = $"{commenter.FirstName} {commenter.LastName} commented on your article",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                _notificationsRepository.AddNotificationAsync(notification);
            }

            await _notificationsRepository.SaveChangesAsync();

            foreach(var article in articlesOwners)
            {
                var unreadNotification = _notificationsRepository.GetUnreadNotificationsForUser(article).Count();
                _firebaseClient.SendCommentNotification(unreadNotification, article);
            }

        }

        public async Task AddLikeNotification(User commenter, string topicId)
        {
            var articleOwners = _articleRepository.GetArticlesByTopicId(topicId).Select(x => x.UserId).Distinct();
            foreach (var article in articleOwners)
            {
                var notification = new Notification
                {
                    ActivityId = Guid.NewGuid().ToString(),
                    UserId = article,
                    ActionPerformedBy = commenter.Id,
                    NoticeText = $"{commenter.FirstName} {commenter.LastName} liked your article",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                _notificationsRepository.AddNotificationAsync(notification);
            }

            await _notificationsRepository.SaveChangesAsync();

            foreach (var article in articleOwners)
            {
                var unreadNotification = _notificationsRepository.GetUnreadNotificationsForUser(article).Count();
                _firebaseClient.SendCommentNotification(unreadNotification, article);
            }
        }

        public async Task<PaginatedListDto<NotificationToReturnDto>> GetUserNofitifations(string userId, int pageNumber, int perPage)
        {
            var notifications = _notificationsRepository.GetUserNotificationsAsync(userId).Select(x => new NotificationToReturnDto {
                ActionPerformedBy = x.ActionPerformedBy,
                ActivityId = x.ActivityId,
                NoticeText = x.NoticeText,
                CreatedAt = x.CreatedAt,
                IsRead = x.IsRead
            });
            await Task.CompletedTask;
            return PagedList<NotificationToReturnDto>.Paginate(notifications, pageNumber, perPage);
        }
    }
}
