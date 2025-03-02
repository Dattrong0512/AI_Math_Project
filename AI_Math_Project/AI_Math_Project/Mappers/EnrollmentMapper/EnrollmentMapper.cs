
using AI_Math_Project.Data.Model;
using AI_Math_Project.DTO.EnrollmentDto;

namespace AI_Math_Project.Mappers.EnrollmentMapper
{
    public static class EnrollmentMapper
    {
      public static EnrollmentDto ToEnrollmentDtoMapper(this Enrollment enrollment)
        {
            EnrollmentDto dto = new EnrollmentDto             
            {
           EnrollmentId = enrollment.EnrollmentId,
           UserId = enrollment.UserId,
           Grade=enrollment.Grade,
           EnrollmentDate =enrollment.EnrollmentDate,
           AvgScore=enrollment.AvgScore,
           Semester =enrollment.Semester,
           StartYear=enrollment.StartYear,
           EndYear=enrollment.EndYear
             };
            return dto;

        }
        public static List<EnrollmentDto> ToListEnrollmentDtoMapper(this List<Enrollment> enrollment)
        {
            List<EnrollmentDto> listDto = new List<EnrollmentDto> { };
            foreach(var er in enrollment)
            {
                listDto.Add(er.ToEnrollmentDtoMapper());
            }
            return listDto;

        }
    }
}
