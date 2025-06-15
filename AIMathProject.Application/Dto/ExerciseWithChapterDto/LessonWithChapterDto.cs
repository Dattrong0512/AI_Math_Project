using System;
using System.ComponentModel.DataAnnotations;

namespace AIMathProject.Application.Dto.ExerciseWithChapterDto
{
    public class LessonWithChapterDto
    {
        public short Grade { get; set; }
        public string LessonName { get; set; } = null!;
        public string ChapterName { get; set; } = null!;
    }
}