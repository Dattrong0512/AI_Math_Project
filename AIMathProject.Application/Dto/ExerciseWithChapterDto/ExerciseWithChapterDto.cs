using System;
using System.Collections.Generic;

namespace AIMathProject.Application.Dto.ExerciseWithChapterDto
{
    public class ExerciseWithChapterDto
    {
        public int ExerciseId { get; set; }
        public string ExerciseName { get; set; } = null!;
        public virtual LessonWithChapterDto? Lesson { get; set; }
        public virtual List<ExerciseResultDto.ExerciseResultSummaryDto> ExerciseResults { get; set; } = new List<ExerciseResultDto.ExerciseResultSummaryDto>();
    }
}