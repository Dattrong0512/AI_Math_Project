using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Domain.Interfaces
{
    public interface ILessonProgressRepository<T> where T : class
    {
        Task<ICollection<T>> GetAllInfLessonProgress(int id);
        Task<ICollection<T>> GetAllInfLessonProgressClassified(int id, int semester);
        Task<T> UpdateLearningProgress(int idProgress, string status);
        Task<T> GetInfoOneLessonProgress(int lpId);
    }
}
