using AI_Math_Project.Data.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AI_Math_Project.DTO.AnswerDto
{
    public class FillAnswerDto
    {

        public int AnswerId { get; set; }

        public string CorrectAnswer { get; set; } = null!;

        public short Position { get; set; }

    }
}
