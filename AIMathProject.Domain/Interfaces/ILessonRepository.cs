using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Domain.Interfaces
{
    public interface ILessonRepository<T> where T : class
    {
        Task<T> GetDetailLessonById(int grade, int chapter_order, int id);
        Task<bool> CreateLesson(int grade, int chapterorder, T lessionDto);
        Task<ICollection<T>> GetDetailLessonByName(int grade, string lesson_name);
    }
}
