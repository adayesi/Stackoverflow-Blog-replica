using System.Net.Http;
using System.Threading.Tasks;

namespace DecaBlog.Commons.HttpClients.Firebase
{
    public class FirebaseClient: IFirebaseClient
    {
        private readonly HttpClient _httpClient;

        public FirebaseClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task SendCommentNotification(int unreadNotificatons, string userId)
        {
            return _httpClient.PutAsync($"notifications/{userId}.json", new StringContent(unreadNotificatons.ToString()));
        }
    }
}
