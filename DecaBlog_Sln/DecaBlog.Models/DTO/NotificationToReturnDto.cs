using System;

namespace DecaBlog.Models.DTO
{
    public class NotificationToReturnDto
    {
        public string ActivityId { get; set; }
        public string NoticeText { get; set; }
        public string ActionPerformedBy { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
