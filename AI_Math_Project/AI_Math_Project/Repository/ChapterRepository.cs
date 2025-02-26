using AI_Math_Project.Data;
using AI_Math_Project.Data.Model;
using AI_Math_Project.DTO;
using AI_Math_Project.Interfaces;
using AI_Math_Project.Mappers.ChapterMappers;
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


        public async Task<List<ChapterDto>> GetAllChapters()
        {

            var listChapter = await _context.Chapters.ToListAsync();

            var listChapterDto = ChapterMappers.ToChapterDtoList(listChapter);
            return listChapterDto;
        }
        public async Task<List<ChapterDto>> GetAllDetailChapters()
        {
            // Truy vấn các Chapter từ cơ sở dữ liệu
            var chapters = await _context.Chapters.ToListAsync();

            // Khởi tạo danh sách kết quả
            var result = new List<ChapterDto>();

            // Lặp qua từng Chapter và lấy các bài học (Lessons) liên quan
            foreach (var chapter in chapters)
            {
                // Lấy các bài học tương ứng với ChapterId của chương hiện tại
                var lessons = await _context.Lessons
                    .Where(l => l.ChapterId == chapter.ChapterId)
                    .Select(l => new LessionDto
                    {
                        LessonOrder = l.LessonOrder,
                        LessonName = l.LessonName
                    })
                    .ToListAsync();

                // Thêm Chapter và Lessons vào kết quả
                result.Add(new ChapterDto
                {
                    Grade = chapter.Grade,
                    ChapterOrder = chapter.ChapterOrder,
                    ChapterName = chapter.ChapterName,
                    Lessons = lessons
                });
            }

            return result;
        }
    }
}
