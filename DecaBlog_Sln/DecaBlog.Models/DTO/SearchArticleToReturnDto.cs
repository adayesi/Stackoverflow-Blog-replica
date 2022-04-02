using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecaBlog.Models.DTO
{
    public class SearchArticleToReturnDto
    {
        public string TopicId { get; set; }
        public string Topic { get; set; }
        public string Abstract { get; set; }
        public HashSet<string> Tags { get; set; }
        public string CoverPhotoUrl { get; set; }
        public AuthorDto Author { get; set; }
    }

    public class RelatedArticleToReturnDto
    {
        public string TopicId { get; set; }
        public string Topic { get; set; }
        public string Abstract { get; set; }
        public string CoverPhotoUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public AuthorForDto Author { get; set; }
    }

    public class AuthorForDto
    {
        public string AuthorId { get; set; }
        public string FullName { get; set; }
        public string AuthorPhotoUrl { get; set; }
    }

    public class SearchArticleEqualityComparer : IEqualityComparer<SearchArticleToReturnDto>
    {
        public bool Equals(SearchArticleToReturnDto x, SearchArticleToReturnDto y)
        {
            return x.TopicId == y.TopicId;
        }

        public int GetHashCode(SearchArticleToReturnDto obj)
        {
            return obj.GetHashCode();
        }
    }
}