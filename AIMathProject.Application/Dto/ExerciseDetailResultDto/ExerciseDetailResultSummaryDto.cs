using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIMathProject.Domain.Entities;
using AIMathProject.Application.Dto.QuestionDto;

namespace AIMathProject.Application.Dto.ExerciseDetailResultDto
{
    public class ExerciseDetailResultSummaryDto
    {
        public bool? IsCorrect { get; set; }
        public QuestionSummaryDto? Question { get; set; }
    }
}
