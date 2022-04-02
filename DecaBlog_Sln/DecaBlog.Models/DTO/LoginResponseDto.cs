using System.Collections.Generic;

namespace DecaBlog.Models.DTO
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public IList<string> Role { get; set; } = new List<string>();
    }
}
