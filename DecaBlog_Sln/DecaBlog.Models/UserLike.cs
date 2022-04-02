using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DecaBlog.Models
{
    public class UserLike : BaseEntity
    {
        [Required]
        public string LikerId { get; set; }
        [Required]
        public string TopicId { get; set; }
        //navigation props
        [ForeignKey("TopicId")]
        public ArticleTopic ArticleTopic { get; set; }

        [ForeignKey("LikerId")]
        public User User { get; set; }
    }
}
