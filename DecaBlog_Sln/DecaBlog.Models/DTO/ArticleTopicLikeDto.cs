﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecaBlog.Models.DTO
{
    public class ArticleTopicLikeDto
    {
        public string TopicId { get; set; }
        public int TotalLikes { get; set; }
    }
}
