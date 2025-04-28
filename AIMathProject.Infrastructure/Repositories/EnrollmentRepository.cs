using AIMathProject.Application.Dto.EnrollmentDto;
using AIMathProject.Application.Mappers;
using AIMathProject.Domain.Interfaces;
using AIMathProject.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Infrastructure.Repositories
{
    public class EnrollmentRepository : IEnrollmentRepository<EnrollmentDto>
    {
        private readonly ApplicationDbContext _context;

        public EnrollmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<EnrollmentDto>> GetAllEnrollmentByID(int id)
        {
            var ListER = await _context.Enrollments.Where(er => er.UserId == id).ToListAsync();
            return ListER.ToListEnrollmentDtoMapper();
        }
        public async Task<EnrollmentDto> UpdateEnrollment(EnrollmentDto updatedEnrollmentDto)
        {
            var enrollment = await _context.Enrollments.FirstOrDefaultAsync(e => e.EnrollmentId == updatedEnrollmentDto.EnrollmentId);
            if (enrollment == null)
            {
                return null;
            }

            if (updatedEnrollmentDto.UserId.HasValue)
            {
                enrollment.UserId = updatedEnrollmentDto.UserId.Value;
            }

            if (updatedEnrollmentDto.Grade.HasValue)
            {
                enrollment.Grade = updatedEnrollmentDto.Grade.Value;
            }

            if (updatedEnrollmentDto.EnrollmentDate.HasValue)
            {
                enrollment.EnrollmentDate = updatedEnrollmentDto.EnrollmentDate.Value;
            }

            if (updatedEnrollmentDto.AvgScore.HasValue)
            {
                enrollment.AvgScore = updatedEnrollmentDto.AvgScore.Value;
            }

            if (updatedEnrollmentDto.Semester.HasValue)
            {
                enrollment.Semester = updatedEnrollmentDto.Semester.Value;
            }

            if (updatedEnrollmentDto.StartYear.HasValue)
            {
                enrollment.StartYear = updatedEnrollmentDto.StartYear.Value;
            }

            if (updatedEnrollmentDto.EndYear.HasValue)
            {
                enrollment.EndYear = updatedEnrollmentDto.EndYear.Value;
            }
            await _context.SaveChangesAsync();
            return enrollment.ToEnrollmentDtoMapper();
        }

        public async Task<EnrollmentDto> GetEnrollmentById(int enrollmentId)
        {
            var enrollment = await _context.Enrollments
                .FirstOrDefaultAsync(e => e.EnrollmentId == enrollmentId);

            if (enrollment == null)
            {
                return null;
            }

            return enrollment.ToEnrollmentDtoMapper();
        }
    }

}
