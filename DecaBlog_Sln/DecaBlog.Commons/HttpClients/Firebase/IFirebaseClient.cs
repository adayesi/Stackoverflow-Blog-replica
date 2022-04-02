using System.Threading.Tasks;

namespace DecaBlog.Commons.HttpClients.Firebase
{
    public interface IFirebaseClient
    {
        Task SendCommentNotification(int unreadNotificatons, string userId);
    }
}
