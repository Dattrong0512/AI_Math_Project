
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AIMathProject.Application.Dto
{
    public class ChapterDto
    {
        public short? Grade { get; set; }
        public short? ChapterOrder { get; set; }
        public string? ChapterName { get; set; }

        public short? Semester { get; set; }
        public List<LessonDto>? Lessons { get; set; }

    }
}
