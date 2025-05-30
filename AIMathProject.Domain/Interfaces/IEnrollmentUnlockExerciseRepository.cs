using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMathProject.Domain.Interfaces
{
    public interface IEnrollmentUnlockExerciseRepository<T> where T : class
    {
        Task<List<int>> GetUnlockedExercisesByEnrollmentId(int enrollmentId);
        Task<(bool success, string message)> UnlockExercise(int exerciseId, int enrollmentId);
    }
}