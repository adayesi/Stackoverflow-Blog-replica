using System;
using System.ComponentModel.DataAnnotations;

namespace DecaBlog.Models
{
    public class Notification
    {
        [Key]
        public string ActivityId { get; set; }
        [Required]
        public string UserId { get; set; }
        public User User { get; set; }
        [Required]
        public string NoticeText { get; set; }
        [Required]
        public string ActionPerformedBy { get; set; }
        [Required]
        public bool IsRead { get; set; }
        [Required]
        public bool IsDeprecated { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
