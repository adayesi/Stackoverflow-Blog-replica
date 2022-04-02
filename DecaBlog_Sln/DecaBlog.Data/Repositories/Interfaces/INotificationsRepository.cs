
using DecaBlog.Models;

using System.Linq;
using System.Threading.Tasks;

namespace DecaBlog.Data.Repositories.Interfaces
{
    public interface INotificationsRepository
    {
        void AddNotificationAsync(Notification notification);
        Task<bool> UpdateNotificationAsync(Notification notification);
        IQueryable<Notification> GetUserNotificationsAsync(string userId);
        IQueryable<Notification> GetUnreadNotificationsForUser(string userId);
        Task SaveChangesAsync();
    }
}
