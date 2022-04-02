namespace DecaBlog.Models.DTO
{
    public class SearchArticleByKeywordSearchParams
    {
        public string KeyWords { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PerPage { get; set; } = 20;
    }
}
