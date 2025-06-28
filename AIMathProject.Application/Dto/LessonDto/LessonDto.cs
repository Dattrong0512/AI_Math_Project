using AIMathProject.Application.Dto.ExerciseDto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Dto.LessonDto
{
    public class LessonDto
    {
        public int LessonId { get; set; }
        public short? LessonOrder { get; set; }

        [StringLength(100)]
        public string LessonName { get; set; } = null!;

        [StringLength(255)]
        public string? LessonVideoUrl { get; set; }

        [StringLength(255)]
        public string? LessonPdfUrl { get; set; }

        public ExerciseExtraForLessonDto? MainExercise { get; set; } = null!;

        public short? ChapterOrder { get; set; }

        public List<ExerciseDto.ExerciseExtraForLessonDto> ExtraExercise { get; set; } = new List<ExerciseDto.ExerciseExtraForLessonDto>();
    }
}
