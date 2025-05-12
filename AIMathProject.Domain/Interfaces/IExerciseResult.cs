using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Domain.Interfaces
{
    public interface IExerciseResultRepository<T> where T : class
    {
        Task<T> GetDetailExerciseResultById(int enrollment_id, int exercise_id);
        Task<List<T>> GetAllExerciseResultsByEnrollmentId(int enrollment_id);
    }
}
