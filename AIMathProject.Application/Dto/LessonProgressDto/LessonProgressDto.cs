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

        [StringLength(15)]
        public string? Status { get; set; }

        public short? LearningProgress { get; set; }

        public LessonDto.LessonDto? Lesson { get; set; }


    }
}
