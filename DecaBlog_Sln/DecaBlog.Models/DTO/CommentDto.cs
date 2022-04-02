using System;

namespace DecaBlog.Models.DTO
{
    public class CommentDto
    {
        public Commentator Commentator { get; set; }

        public string CommentId { get; set; }

        public string TopicId { get; set; }

        public string CommentText { get; set; }

        public int Votes { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime CreatedAt { get; set; }
    }

    public class Commentator
    {
        public string CommentatorId { get; set; }
        public string FullName { get; set; }
    }
}