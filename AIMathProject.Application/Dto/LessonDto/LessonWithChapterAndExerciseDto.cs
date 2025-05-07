using AIMathProject.Application.Dto.ExerciseWithChapterDto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Dto.LessonDto
{
    public class LessonWithChapterAndExerciseDto
    {
        public short? LessonOrder { get; set; }

        [StringLength(100)]
        public string LessonName { get; set; } = null!;

        [StringLength(255)]
        public string? LessonVideoUrl { get; set; }

        [StringLength(255)]
        public string? LessonPdfUrl { get; set; }

        public ChapterSummaryDto? Chapter { get; set; }

        public List<ExerciseDto.ExerciseDto> Exercises { get; set; } = new List<ExerciseDto.ExerciseDto>();
    }
}
