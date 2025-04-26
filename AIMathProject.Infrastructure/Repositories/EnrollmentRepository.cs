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
        public async Task<EnrollmentDto> UpdateEnrollmentGrade(int enrollmentId, short? newGrade)
        {
            var enrollment = await _context.Enrollments.FirstOrDefaultAsync(e => e.EnrollmentId == enrollmentId);
            if (enrollment == null)
            {
                return null;
            }
            enrollment.Grade = newGrade;
            await _context.SaveChangesAsync();
            return await GetEnrollmentById(enrollment.EnrollmentId);
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
