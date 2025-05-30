using AIMathProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIMathProject.Application.Dto.ExerciseResultDto;

namespace AIMathProject.Application.Dto.ExerciseDto
{
    public class ExerciseDto
    {
        public string ExerciseName { get; set; } = null!;

        public int? ExerciseId { get; set; }

        public bool? IsLocked { get; set; }

        public string? Description { get; set; }

        public int? LessonId { get; set; }

        public virtual List<ExerciseResultDto.ExerciseResultDto> ExerciseResults { get; set; } = new List<ExerciseResultDto.ExerciseResultDto>();
    }
}
