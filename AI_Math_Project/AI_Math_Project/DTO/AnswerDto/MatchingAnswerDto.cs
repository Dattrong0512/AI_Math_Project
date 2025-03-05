using AI_Math_Project.Data.Model;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AI_Math_Project.DTO.AnswerDto
{
    public class MatchingAnswerDto
    {
        public int AnswerId { get; set; }

        public string CorrectAnswer { get; set; } = null!;

        public string ImgUrl { get; set; } = null!;

    }
}
