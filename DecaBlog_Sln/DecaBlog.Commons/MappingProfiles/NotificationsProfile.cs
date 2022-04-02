using AutoMapper;

using DecaBlog.Models;
using DecaBlog.Models.DTO;

namespace DecaBlog.Commons.MappingProfiles
{
    public class NotificationsProfile: Profile
    {
        public NotificationsProfile()
        {
            CreateMap<Notification, NotificationToReturnDto>();
        }
    }
}
