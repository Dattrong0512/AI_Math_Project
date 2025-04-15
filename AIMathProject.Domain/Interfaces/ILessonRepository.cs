using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Domain.Interfaces
{
    public interface ILessonRepository<T> where T : class
    {
        Task<T> GetDetailLessonById(int grade, int lesson_order);
        Task<bool> CreateLesson(int grade, int chapterorder, T lessonDto);
        Task<ICollection<T>> GetDetailLessonByName(int grade, string lesson_name);
    }
}
