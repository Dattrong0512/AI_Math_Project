using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Dto
{
    public class LessonDto
    {
        public short? LessonOrder { get; set; }

        [StringLength(100)]
        public string LessonName { get; set; } = null!;

        [StringLength(255)]
        public string? LessonVideoUrl { get; set; }

        [StringLength(255)]
        public string? LessonPdfUrl { get; set; }

        public short? ChapterOrder { get; set; }

        public List<QuestionDto.QuestionDto>? Questions { get; set; } = new List<QuestionDto.QuestionDto>();
    }
}
