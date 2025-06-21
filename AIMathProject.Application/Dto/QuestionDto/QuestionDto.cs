using AIMathProject.Application.Dto.AnswerDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Dto.QuestionDto
{
    public class QuestionDto
    {
        public int QuestionId { get; set; }
        public string? QuestionType { get; set; }
        public short? Difficulty { get; set; }
        public int? LessonId { get; set; }
        public string? ImgUrl { get; set; }
        public string? QuestionContent { get; set; }
        public string? QuestionPrompt { get; set; }
        public string? PdfSolution { get; set; }
        public List<ChoiceAnswerDto>? ChoiceAnswers { get; set; } = new List<ChoiceAnswerDto>();
        public List<FillAnswerDto>? FillAnswers { get; set; } = new List<FillAnswerDto>();
        public List<MatchingAnswerDto>? MatchingAnswers { get; set; } = new List<MatchingAnswerDto>();
    }
}
