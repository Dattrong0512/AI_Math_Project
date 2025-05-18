using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Domain.Interfaces
{
    public interface IExerciseDetailResultRepository<T> where T : class
    {
        Task<bool> UpsertExerciseDetailResult(int enrollment_id, int exercise_id, List<T> edrDto);
    }
}
