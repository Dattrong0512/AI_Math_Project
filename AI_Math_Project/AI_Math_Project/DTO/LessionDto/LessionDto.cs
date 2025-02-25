using System.ComponentModel.DataAnnotations;

namespace AI_Math_Project.DTO
{
    public class LessionDto
    {
        public short? LessonOrder { get; set; }


        [StringLength(100)]
        public string LessonName { get; set; } = null!;
        //public int ? ChapterId { get; set; }
    }

}
