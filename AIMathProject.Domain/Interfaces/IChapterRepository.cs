using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Domain.Interfaces
{
    public interface IChapterRepository<T> where T : class
    {
        Task<ICollection<T>> GetAllChapters();
        Task<ICollection<T>> GetAllDetailChapters();
        Task<ICollection<T>> GetAllDetailChaptersClassified(int grade);
    }
}
