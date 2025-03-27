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
    }
}
