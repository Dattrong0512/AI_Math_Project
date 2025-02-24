using AI_Math_Project.Data;
using AI_Math_Project.Data.Model;
using AI_Math_Project.DTO;
using AI_Math_Project.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace AI_Math_Project.Repository
{
    public class ChapterRepository : IChapterRepository
    {
        private readonly ApplicationDBContext _context;

        public ChapterRepository(ApplicationDBContext _dBContext)
        {
            _context = _dBContext ;
        }


        public async Task<List<Chapter>> GetAllChapters()
        {

            var listChapter = await _context.Chapters.ToListAsync();

            return listChapter;
        }
        public async Task<List<ChapterDto>> GetAllDetailChapters()
        {


            var queryChapter = await _context.Chapters
                .Join(
                    _context.Lessons, // Bảng cần JOIN (Lesson)
                    c => c.ChapterId,  // Khóa chính từ bảng Chapter
                    l => l.ChapterId,  // Khóa ngoại từ bảng Lesson
                    (c, l) => new ChapterDto // Chọn dữ liệu cần thiết
                    {
                        Grade = c.Grade,
                        ChapterOrder = c.ChapterOrder,
                        ChapterName = c.ChapterName,
                        Lessons = new List<LessionDto> // Mỗi dòng chỉ có 1 bài học
                        {

                            new LessionDto
                            {
                                LessonOrder = l.LessonOrder,
                                LessonName = l.LessonName
                            }
                        }
                    })
                .ToListAsync();

            return queryChapter;

        }

    }
}
