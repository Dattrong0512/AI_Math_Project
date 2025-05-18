using AIMathProject.Application.Dto.LessonDto;
using AIMathProject.Application.Dto.LessonProgressDto;
using AIMathProject.Application.Mappers;
using AIMathProject.Domain.Entities;
using AIMathProject.Domain.Interfaces;
using AIMathProject.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Infrastructure.Repositories
{
    public class LessonProgressRepository : ILessonProgressRepository<LessonProgressDto>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<LessonProgressRepository> _logger;

        public LessonProgressRepository(ApplicationDbContext context, ILogger<LessonProgressRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ICollection<LessonProgressDto>> GetAllInfLessonProgress(int id)
        {
            _logger.LogInformation($"Fetching all lesson progress for enrollment ID: {id}");

            var lessonProgressDtos = await (
                from lp in _context.LessonProgresses
                join l in _context.Lessons on lp.LessonId equals l.LessonId
                join c in _context.Chapters on l.ChapterId equals c.ChapterId
                where lp.EnrollmentId == id
                select new LessonProgressDto
                {
                    LearningProgressId = lp.LearningProgressId,
                    LessonId = lp.LessonId,
                    Status = lp.Status,
                    Lesson = new LessonDto
                    {
                        LessonOrder = l.LessonOrder,
                        LessonName = l.LessonName,
                        LessonVideoUrl = l.LessonVideoUrl,
                        LessonPdfUrl = l.LessonPdfUrl
                    }
                }).ToListAsync();
            return lessonProgressDtos;
        }



        public async Task<ICollection<LessonProgressDto>> GetAllInfLessonProgressClassified(int id, int grade, int semester)
        {
            _logger.LogInformation($"Fetching lesson progress for enrollment ID: {id} filtered by grade: {grade} and semester: {semester}");

            var lessonProgressDtos = await (
                from lp in _context.LessonProgresses
                join l in _context.Lessons on lp.LessonId equals l.LessonId
                join c in _context.Chapters on l.ChapterId equals c.ChapterId
                where lp.EnrollmentId == id && c.Grade == grade && c.Semester == semester
                select new LessonProgressDto
                {
                    LearningProgressId = lp.LearningProgressId,
                    LessonId = lp.LessonId,
                    Status = lp.Status,
                    Lesson = new LessonDto
                    {
                        LessonOrder = l.LessonOrder,
                        LessonName = l.LessonName,
                        LessonVideoUrl = l.LessonVideoUrl,
                        LessonPdfUrl = l.LessonPdfUrl
                    }
                }).ToListAsync();

            return lessonProgressDtos;
        }

        public async Task<LessonProgressDto> GetInfoOneLessonProgress(int lpId)
        {
            LessonProgress? lp = await _context.LessonProgresses
                .Where(lp => lp.LearningProgressId == lpId)
                .Include(lp => lp.Lesson)
                .FirstOrDefaultAsync();

            if (lp == null)
            {
                return null;
            }

            return lp.ToLessonProgressDto();
        }

        public async Task<LessonProgressDto> UpdateLearningProgress(int lessonId, int enrollmentId, string status)
        {
            var progress = await _context.LessonProgresses
            .FirstOrDefaultAsync(lp => lp.LessonId == lessonId && lp.EnrollmentId == enrollmentId);
            if (progress == null)
            {
                return null;
            }
            progress.Status = status;
            await _context.SaveChangesAsync();
            return await GetInfoOneLessonProgress(progress.LearningProgressId);
        }
    }
}
