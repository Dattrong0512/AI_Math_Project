﻿using AI_Math_Project.Data.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace AI_Math_Project.DTO.LessionProgressDto
{
    public class LessonProgressDto
    {
        public int LearningProgressId { get; set; }

        public int? LessonId { get; set; }

        public short? LearningProgress { get; set; }

        public bool? IsCompleted { get; set; }

        public LessonDto? Lesson { get; set; }
    

}
}
