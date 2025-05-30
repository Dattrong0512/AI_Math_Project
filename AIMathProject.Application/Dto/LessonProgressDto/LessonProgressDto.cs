using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Dto.LessonProgressDto
{
    public class LessonProgressDto
    {
        public int LearningProgressId { get; set; }

        public int? LessonId { get; set; }

        public int? Process { get; set; }

        public string? Status { get; set; }

        public LessonDto.LessonDto? Lesson { get; set; }


    }
}
