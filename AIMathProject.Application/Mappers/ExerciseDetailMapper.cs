using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIMathProject.Application.Dto.ExerciseDetailDto;
using AIMathProject.Application.Dto.QuestionDto;
using AIMathProject.Domain.Entities;
namespace AIMathProject.Application.Mappers
{
    public static class ExerciseDetailMapper
    {
        public static ExerciseDetailDto ToExerciseDetailDto(this ExerciseDetail edd)
        {
            ExerciseDetailDto dto = new ExerciseDetailDto
            {
                Question = edd.Question?.ToQuestionDto()
            };
            return dto;
        }
    }
}
