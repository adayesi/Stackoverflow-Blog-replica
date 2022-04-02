namespace DecaBlog.Models.DTO
{
    public class ArticleTopicNameSearchParams
    {
        public string AuthorName { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PerPage { get; set; } = 20;
    }
}
