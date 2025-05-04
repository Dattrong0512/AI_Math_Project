using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Domain.Interfaces
{
    public interface IExerciseRepository<T> where T : class
    {
        Task<List<T>> GetExercisesWithResultsByEnrollmentId(int enrollmentId);
        
    }
}
