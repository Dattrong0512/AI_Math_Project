using AIMathProject.Application.Dto;
using AIMathProject.Application.Mappers;
using AIMathProject.Domain.Entities;
using AIMathProject.Domain.Interfaces;
using AIMathProject.Infrastructure.Data;
using AIMathProject.Infrastructure.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Infrastructure.Repositories
{
    public class LessonRepository : ILessonRepository<LessonDto>
    {
        private readonly ApplicationDbContext _context;

        public LessonRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateLesson(int grade, int chapterorder, LessonDto lessionDto)
        {
            if (lessionDto is null)
            {
                throw new Exception("Lesson is allowed null");

            }
            else
            {
                var chapterID = await _context.Chapters.
                Where(ct => ct.Grade == grade && ct.ChapterOrder == chapterorder)
                .Select(ct => ct.ChapterId)
                .SingleOrDefaultAsync();

                Lesson lesson = lessionDto.ToLessonFromLessonDto(chapterID);

                await _context.AddAsync(lesson);
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<LessonDto> GetDetailLessonById(int grade, int chapter_order, int id)
        {
            var chapterID = await _context.Chapters.
                 Where(ct => ct.Grade == grade && ct.ChapterOrder == chapter_order)
                 .Select(ct => ct.ChapterId)
                 .SingleOrDefaultAsync();

            Lesson? lesson = await _context.Lessons.Where(ls => ls.LessonOrder == id && ls.ChapterId == chapterID).FirstOrDefaultAsync();
            if (lesson == null)
            {
                throw new Exception("Not found");
            }
            else
            {
                return lesson.ToLessonDto();
            }
        }

        public async Task<ICollection<LessonDto>> GetDetailLessonByName(int grade, string lesson_name)
        {
            var lesson = from Chapter in _context.Chapters
                         join Lesson in _context.Lessons
                         on Chapter.ChapterId equals Lesson.ChapterId
                         where Chapter.Grade == grade
                         select new LessonDto
                         {
                             LessonName = Lesson.LessonName,
                             LessonOrder = Lesson.LessonOrder,
                             LessonContent = Lesson.LessonContent
                         };

            var lessonList = await lesson.ToListAsync();

            var filteredLessons = lessonList
                .Where(l => RemoveDiacriticsUtils.RemoveDiacritics(l.LessonName.ToLower()).Contains(RemoveDiacriticsUtils.RemoveDiacritics(lesson_name.ToLower())))
                .ToList();

            return filteredLessons;
        }
    }
}
