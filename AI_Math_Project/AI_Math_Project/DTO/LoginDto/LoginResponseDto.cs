using System.ComponentModel.DataAnnotations;

namespace AI_Math_Project.DTO.LoginDto
{
    public class LoginResponseDto
    {  
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public string? Role { get; set; }
        public int? AccountId { get; set; }
    }
}
