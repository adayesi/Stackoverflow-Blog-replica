using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecaBlog.Models.DTO
{
    public class ArticleByTopicNameDto
    {
        public string TopicName { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PerPage { get; set; } = 20;
    }
}
