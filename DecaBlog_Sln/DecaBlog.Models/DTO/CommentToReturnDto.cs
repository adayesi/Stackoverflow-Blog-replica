using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecaBlog.Models.DTO
{
    public class CommentToReturnDto
    {
        public string Id { get; set; }
        public string CommentText { get; set; }
        public string TopicId { get; set; }
        public int Votes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public CommentatorDto Commentator { get; set; }
    }
    public class CommentatorDto
    {
        public string CommentatorId { get; set; }
        public string FullName { get; set; }
        public string PhotoUrl { get; set; }
    }
}
