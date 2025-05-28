using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Dto.ExerciseDetailResultDto
{
    public class ExerciseDetailResultForGetDto
    {
        public bool? IsCorrect { get; set; }
        public string? QuestionType { get; set; }


        public virtual List<AnswerDto.UserChoiceAnswerDto> UserChoiceAnswers { get; set; } = new List<AnswerDto.UserChoiceAnswerDto>();
        public virtual List<AnswerDto.UserFillAnswerDto> UserFillAnswers { get; set; } = new List<AnswerDto.UserFillAnswerDto>();
        
        public ExerciseDetailDto.ExerciseDetailDto? ExerciseDetail
        {
            get; set;
        }
    }
}
