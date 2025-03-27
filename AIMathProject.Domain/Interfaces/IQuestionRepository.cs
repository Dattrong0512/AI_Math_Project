using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Domain.Interfaces
{
    public interface IQuestionRepository<T> where T : class
    {
        Task<ICollection<T>> GetAllQuestionByLessonID(int grade, int lessionOrder);
    }
}
