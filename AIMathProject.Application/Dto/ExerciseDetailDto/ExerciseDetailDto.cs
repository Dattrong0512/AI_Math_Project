using AIMathProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Dto.ExerciseDetailDto
{
    public class ExerciseDetailDto
    {
        public QuestionDto.QuestionDto? Question { get; set; }
    }
}
