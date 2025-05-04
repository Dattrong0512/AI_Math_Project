using System;
using System.Collections.Generic;

namespace AIMathProject.Application.Dto.ExerciseWithChapterDto
{
    public class ChapterSummaryDto
    {
        public int ChapterId { get; set; }
        public short? Grade { get; set; }
        public short? ChapterOrder { get; set; }
        public string? ChapterName { get; set; }
        public short? Semester { get; set; }
    }
}