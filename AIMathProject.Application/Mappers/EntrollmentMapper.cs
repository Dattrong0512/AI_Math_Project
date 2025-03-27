using AIMathProject.Application.Dto.EnrollmentDto;
using AIMathProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Mappers
{
    public static class EnrollmentMapper
    {
        public static EnrollmentDto ToEnrollmentDtoMapper(this Enrollment enrollment)
        {
            EnrollmentDto dto = new EnrollmentDto
            {
                EnrollmentId = enrollment.EnrollmentId,
                UserId = enrollment.UserId,
                Grade = enrollment.Grade,
                EnrollmentDate = enrollment.EnrollmentDate,
                AvgScore = enrollment.AvgScore,
                Semester = enrollment.Semester,
                StartYear = enrollment.StartYear,
                EndYear = enrollment.EndYear
            };
            return dto;

        }
        public static List<EnrollmentDto> ToListEnrollmentDtoMapper(this List<Enrollment> enrollment)
        {
            List<EnrollmentDto> listDto = new List<EnrollmentDto> { };
            foreach (var er in enrollment)
            {
                listDto.Add(er.ToEnrollmentDtoMapper());
            }
            return listDto;

        }
    }
}
