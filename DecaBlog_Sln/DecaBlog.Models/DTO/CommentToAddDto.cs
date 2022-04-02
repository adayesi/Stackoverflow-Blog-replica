using System.ComponentModel.DataAnnotations;

namespace DecaBlog.Models.DTO
{
    public class CommentToAddDto
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string CommentText { get; set; }
    }
}
