using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AI_Math_Project.DTO.LoginDto
{
    public class RefreshTokenRequestDto
    {
        public int AccountId { get; set; }

        public string? Token { get; set; }
    }
}
