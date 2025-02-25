using System.ComponentModel.DataAnnotations.Schema;

namespace AI_Math_Project.DTO
{
    public class ChapterDto
    {
        //public int ChapterId { get; set; }
        public short? Grade { get; set; }
        public short? ChapterOrder { get; set; }
        public string ?ChapterName { get; set; }
     
        public List<LessionDto>? Lessons { get; set; }
    }
}
