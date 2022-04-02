using System.ComponentModel.DataAnnotations;

namespace DecaBlog.Models.DTO
{
    public class ContributionToUpdateDto
    {
        public string SubTopic { get; set; } = null;
        [Required]
        public string ArtlcleText { get; set; }
        public string Keywords { get; set; }
    }
}
