using DecaBlog.Data.Repositories.Interfaces;
using DecaBlog.Models;

using System.Linq;
using System.Threading.Tasks;

namespace DecaBlog.Data.Repositories.Implementations
{
    public class NotificationsRepository : INotificationsRepository
    {
        private readonly DecaBlogDbContext _ctx;

        public NotificationsRepository(DecaBlogDbContext ctx)
        {
            _ctx = ctx;
        }

        public void AddNotificationAsync(Notification notification)
        {
            _ctx.Notifications.Add(notification);
        }

        public IQueryable<Notification> GetUnreadNotificationsForUser(string userId)
        {
            return _ctx.Notifications.Where(not => !not.IsRead && !not.IsDeprecated);
        }

        public IQueryable<Notification> GetUserNotificationsAsync(string userId)
        {
            return _ctx.Notifications.Where(x => x.UserId == userId && !x.IsDeprecated);
        }

        public async Task SaveChangesAsync()
        {
            await _ctx.SaveChangesAsync();
        }

        public async Task<bool> UpdateNotificationAsync(Notification notification)
        {
            _ctx.Notifications.Update(notification);
            return await _ctx.SaveChangesAsync() > 0;
        }
    }
}
