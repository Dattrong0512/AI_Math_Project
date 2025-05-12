using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Dto.ExerciseDto
{
    public class ExerciseExtraForLessonDto
    {
        public string ExerciseName { get; set; } = null!;

        public int? ExerciseId { get; set; }

        public virtual List<ExerciseDetailDto.ExerciseDetailDto> ExerciseResults { get; set; } = new List<ExerciseDetailDto.ExerciseDetailDto>();
    }
}
