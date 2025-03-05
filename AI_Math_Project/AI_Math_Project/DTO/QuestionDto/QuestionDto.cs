using AI_Math_Project.Data.Model;
using AI_Math_Project.DTO.AnswerDto;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AI_Math_Project.DTO.QuestionDto
{
    public class QuestionDto
    {

        public int QuestionId { get; set; }

        public string? QuestionType { get; set; }


        public short? Difficulty { get; set; }


        public int? LessonId { get; set; }


        public string? ImgUrl { get; set; }


        public string? QuestionContent { get; set; }

        public string? PdfSolution { get; set; }

        public List<ChoiceAnswerDto>? ChoiceAnswers { get; set; } = new List<ChoiceAnswerDto>();

        public List<FillAnswerDto>? FillAnswers { get; set; } = new List<FillAnswerDto>();

        public List<MatchingAnswerDto>? MatchingAnswers { get; set; } = new List<MatchingAnswerDto>();


    }
}
