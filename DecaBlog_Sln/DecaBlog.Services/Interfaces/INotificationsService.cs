using DecaBlog.Helpers;
using DecaBlog.Models;
using DecaBlog.Models.DTO;

using System.Threading.Tasks;

namespace DecaBlog.Services.Interfaces
{
    public interface INotificationsService
    {
        Task AddCommentNotification(User commenter, string topicId);
        Task AddLikeNotification(User commenter, string topicId);
        Task<PaginatedListDto<NotificationToReturnDto>> GetUserNofitifations(string userId, int pageNumber, int perPage);
    }
}
