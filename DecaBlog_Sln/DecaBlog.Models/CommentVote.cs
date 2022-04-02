using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecaBlog.Models
{
    public class CommentVote
    {
        public string Id { get; set; } 
        public string VoterId { get; set; }
        public string CommentId { get; set; }
        public int Vote { get; set; } = 1;

        [ForeignKey("CommentId")]
        public UserComment Comment { get; set; }

        [ForeignKey("VoterId")]
        public User Voter { get; set; }
        public CommentVote()
        {
            Id = Guid.NewGuid().ToString();
        }

    }
}
