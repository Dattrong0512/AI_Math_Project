using AI_Math_Project.Data;
using AI_Math_Project.DTO.EnrollmentDto;
using AI_Math_Project.Interfaces;
using AI_Math_Project.Mappers.EnrollmentMapper;
using Microsoft.EntityFrameworkCore;

namespace AI_Math_Project.Repository
{
    public class EnrollmentRepository : IEnrollmentRepository
    {

        private readonly ApplicationDBContext _context;
        public EnrollmentRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<List<EnrollmentDto>> GetAllEnrollmentByID(int id)
        {

            var ListER = await _context.Enrollments.Where(er => er.UserId == id).ToListAsync();
            return ListER.ToListEnrollmentDtoMapper();

        }
    }
}
