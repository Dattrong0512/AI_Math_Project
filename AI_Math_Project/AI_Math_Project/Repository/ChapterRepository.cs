using AI_Math_Project.Data;
using AI_Math_Project.DTO;
using AI_Math_Project.Interfaces;
using AI_Math_Project.Mappers.ChapterMappers;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Cryptography;

namespace AI_Math_Project.Repository
{
    public class ChapterRepository : IChapterRepository
    {
        private readonly ApplicationDBContext _context;

        public ChapterRepository(ApplicationDBContext _dBContext)
        {
            _context = _dBContext ;
        }


        public async Task<List<ChapterDto>> GetAllChapters()
        {

            var listChapter = await _context.Chapters.ToListAsync();

            var listChapterDto = ChapterMappers.ToChapterDtoList(listChapter);
            return listChapterDto;
        }
        public async Task<List<ChapterDto>> GetAllDetailChapters()
        {
      
            var chapters = await _context.Chapters.ToListAsync();

            var result = new List<ChapterDto>();


            
         
            foreach (var chapter in chapters)
            {
       
                var lessons = await _context.Lessons
                    .Where(l => l.ChapterId == chapter.ChapterId)
                    .Select(l => new LessonDto
                    {
                        LessonOrder = l.LessonOrder,
                        LessonName = l.LessonName,
                        LessonContent = l.LessonContent
                    })
                    .ToListAsync();

         
                result.Add(new ChapterDto
                {
                    Grade = chapter.Grade,
                    ChapterOrder = chapter.ChapterOrder,
                    ChapterName = chapter.ChapterName,
                    Semester  = chapter.Semester,
                    Lessons = lessons
                });
            }

            return result;
        }

        public async Task<List<ChapterDto>> GetAllDetailChaptersClassified(int grade)
        {
          
            var chapters = await _context.Chapters.Where(c => c.Grade == grade).
                ToListAsync();

            var result = new List<ChapterDto>();

            foreach (var chapter in chapters)
            {
               
                var lessons = await _context.Lessons
                    .Where(l => l.ChapterId == chapter.ChapterId)
                    .Select(l => new LessonDto
                    {
                        LessonOrder = l.LessonOrder,
                        LessonName = l.LessonName,
                        LessonContent = l.LessonContent
                    })
                    .ToListAsync();

                result.Add(new ChapterDto
                {
                    Grade = chapter.Grade,
                    ChapterOrder = chapter.ChapterOrder,
                    ChapterName = chapter.ChapterName,
                    Semester = chapter.Semester,
                    Lessons = lessons
                });
            }
            return result;
        }
    }
}
