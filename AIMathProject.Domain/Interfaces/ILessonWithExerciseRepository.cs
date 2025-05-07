using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AIMathProject.Domain.Interfaces
{
    public interface ILessonWithExerciseRepository<T> where T : class
    {
        public Task<List<T>> GetLessonsWithExercises(int grade);
    }
}
