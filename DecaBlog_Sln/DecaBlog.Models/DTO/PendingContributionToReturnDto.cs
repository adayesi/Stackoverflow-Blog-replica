using System;

namespace DecaBlog.Models.DTO
{
    public class PendingContributionToReturnDto
    {
        public string ArticleId { get; set; }
        public User Contributor { get; set; }
        public string TopicId { get; set; }
        public string Topic { get; set; }
        public string Subtopic { get; set; }
        public string Keywords { get; set; }
        public string ArticleText { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime DateCreated { get; set; }

    }
}
