using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIMathProject.Domain.Entities;

namespace AIMathProject.Application.Dto.ExerciseDetailResultDto
{
    public class ExerciseDetailResultDto
    {
        public int? QuestionId { get; set; }
        public bool? IsCorrect { get; set; }

        public int? ExerciseDetailId { get; set; }

        public int? ExerciseResultId { get; set; }

        public ExerciseDetailDto.ExerciseDetailDto? ExerciseDetail { get; set; }
    }
}
