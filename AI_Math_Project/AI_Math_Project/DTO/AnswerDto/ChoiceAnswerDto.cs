using AI_Math_Project.Data.Model;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AI_Math_Project.DTO.AnswerDto
{
    public class ChoiceAnswerDto
    {
        public int AnswerId { get; set; }

        public string? Content { get; set; }

        public bool? IsCorrect { get; set; }

        public string? ImgUrl { get; set; }
    }
}
