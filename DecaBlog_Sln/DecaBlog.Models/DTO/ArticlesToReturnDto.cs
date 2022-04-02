using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecaBlog.Models.DTO
{
    public class ArticlesToReturnDto
    {
        public string TopicId { get; set; }
        public string Topic { get; set; }
        public string Abstract { get; set; }
        public string Tags { get; set; }
        public string CoverPhotoUrl { get; set; }
        public AuthorDto Author { get; set; }
        public DateTime DateCreated { get; set; }
    }
}