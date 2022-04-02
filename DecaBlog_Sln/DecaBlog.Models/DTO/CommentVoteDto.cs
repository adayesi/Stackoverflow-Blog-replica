using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecaBlog.Models.DTO
{
    public class CommentVoteDto
    {
        public string CommentId { get; set; }
        public int Vote { get; set; }
    }
}
