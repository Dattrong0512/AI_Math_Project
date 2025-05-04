using System;
using System.ComponentModel.DataAnnotations;

namespace AIMathProject.Application.Dto.ExerciseWithChapterDto
{
    public class LessonWithChapterDto
    {
        public string LessonName { get; set; } = null!;
        public ChapterSummaryDto? Chapter { get; set; }
    }
}