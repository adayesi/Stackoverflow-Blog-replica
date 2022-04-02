using System;
using System.Collections.Generic;

namespace DecaBlog.Models.DTO
{
    public class ArticleToReturnDto
    {
        public Topic Topic { get; set; }
        public HashSet<string> Tags { get; set; } = new HashSet<string>();
        public List<ArticleContributor> Contributors { get; set; } = new List<ArticleContributor>();
        public List<SubTopic> Articles { get; set; }
        public int TotalLikes { get; set; }
        public int TotalComments { get; set; }
        public DateTime DatePublished { get; set; }
    }

    public class Topic
    {
        public string TopicId { get; set; }
        public string TopicName { get; set; }
        public string Abstract { get; set; }
        public ArticleCategory Category { get; set; }
        public string PublicId { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime Date { get; set; }
    }

    public class SubTopic
    {
        public string SubId { get; set; }
        public string SubTopicName { get; set; }
        public string Text { get; set; }
        public Publisher Publisher { get; set; }
        public ArticleContributor Contributor_ { get; set; }
        public DateTime Date { get; set; }
    }

    public class Publisher
    {
        public string AuthorId { get; set; }
        public string FullName { get; set; }
    }

    public class ArticleContributor
    {
        public string AuthorId { get; set; }
        public string FullName { get; set; }
    }

    public class ArticleCategory
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
