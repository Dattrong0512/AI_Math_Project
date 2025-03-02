using AI_Math_Project.DTO.EnrollmentDto;

namespace AI_Math_Project.Interfaces
{
    public interface IEnrollmentRepository
    {
        Task<List<EnrollmentDto>> GetAllEnrollmentByID(int id);

    }
}
