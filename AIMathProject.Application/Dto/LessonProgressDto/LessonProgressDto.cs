using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Dto.LessonProgressDto
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
