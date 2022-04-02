using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecaBlog.Models.DTO
{
    public class StackToUpdateDto
    {
        [Required]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "Must be between 3 to 10 characters")]
        public string Name { get; set; }
        [Required]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Must be between 3 to 150 characters")]
        public string Description { get; set; }
    }
}
