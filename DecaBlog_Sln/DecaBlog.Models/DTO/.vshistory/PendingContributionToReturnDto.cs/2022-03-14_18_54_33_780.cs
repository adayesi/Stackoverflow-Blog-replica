using System;

namespace DecaBlog.Models.DTO
{
    public class PendingContributionToReturnDto
    {
        public string ArticleId { get; set; }
        public string ContributorId { get; set; }
        public string TopicId { get; set; }
        public string Topic { get; set; }
        public string Subtopic { get; set; }
        public string Keywords { get; set; }
        public string ArticleText { get; set; }
        public string DateUpdated { get; set; }
        public string DateCreated { get; set; }
    }
}
