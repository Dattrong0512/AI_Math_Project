using AI_Math_Project.Data.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace AI_Math_Project.DTO.EnrollmentDto
{
    public class EnrollmentDto
    {
        public int EnrollmentId { get; set; }

        public int? UserId { get; set; }


        public short? Grade { get; set; }

        public DateOnly? EnrollmentDate { get; set; }


        public decimal? AvgScore { get; set; }

        public short? Semester { get; set; }


        public short? StartYear { get; set; }


        public short? EndYear { get; set; }

    }
}
