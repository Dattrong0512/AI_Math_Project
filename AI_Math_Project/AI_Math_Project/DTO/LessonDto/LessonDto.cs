﻿using System.ComponentModel.DataAnnotations;

namespace AI_Math_Project.DTO
{
    public class LessonDto
    {
        public short? LessonOrder { get; set; }


        [StringLength(100)]
        public string LessonName { get; set; } = null!;

        [StringLength(255)]
        public string? LessonContent { get; set; }
    }

}
