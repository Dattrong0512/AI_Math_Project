using AIMathProject.Application.Dto;
using AIMathProject.Application.Dto.LessonDto;
using AIMathProject.Application.Mappers;
using AIMathProject.Domain.Interfaces;
using AIMathProject.Infrastructure.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Infrastructure.Repositories
{
    public class ChapterRepository : IChapterRepository<ChapterDto>
    {
        private readonly ApplicationDbContext _context;

        private readonly IMapper _mapper;

        public ChapterRepository(ApplicationDbContext _dBContext, IMapper mapper)
        {
            _context = _dBContext;
            _mapper = mapper;
        }


        public async Task<ICollection<ChapterDto>> GetAllChapters()
        {

            var listChapter = await _context.Chapters.ToListAsync();
            var listChapterDto = ChapterMappers.ToChapterDtoList(listChapter);
            return listChapterDto;
        }
        public async Task<ICollection<ChapterDto>> GetAllDetailChapters()
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
                        LessonVideoUrl = l.LessonVideoUrl,
                        LessonPdfUrl = l.LessonPdfUrl
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

        public async Task<ICollection<ChapterDto>> GetAllDetailChaptersClassified(int grade)
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
                        LessonVideoUrl = l.LessonVideoUrl,
                        LessonPdfUrl= l.LessonPdfUrl
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
