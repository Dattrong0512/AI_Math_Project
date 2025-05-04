using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Domain.Interfaces
{
    public interface IExerciseSummaryRepository<T> where T : class
    {
        Task<List<T>> GetExercisesWithChapterInfoByEnrollmentId(int enrollmentId);
    }
}
