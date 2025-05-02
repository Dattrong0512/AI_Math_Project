using AIMathProject.Application.Dto;
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
            var enrollmentId = _context.Enrollments
                .Where(en => en.UserId == id)
                .Select(en => en.EnrollmentId)
                .ToList()
                ;
            _logger.LogInformation($"Enrollment id is: {string.Join(", ", enrollmentId)}");

            var listLP = _context.LessonProgresses
                .Where(lp => lp.EnrollmentId.HasValue && enrollmentId.Contains(lp.EnrollmentId.Value))
                .ToList();


            List<LessonProgressDto> listLPDto = new List<LessonProgressDto> { };

            foreach (var lp in listLP)
            {
                var lessionDto = await _context.Lessons
                    .Where(l => l.LessonId == lp.LessonId)
                    .Select(l => new LessonDto
                    {
                        LessonOrder = l.LessonOrder,
                        LessonName = l.LessonName,
                        LessonVideoUrl = l.LessonVideoUrl,
                        LessonPdfUrl = l.LessonPdfUrl
                    }
                    ).FirstOrDefaultAsync();

                listLPDto.Add(new LessonProgressDto
                {

                    LearningProgressId = lp.LearningProgressId,
                    LessonId = lp.LessonId,

                    Status = lp.Status,

                    Lesson = lessionDto
                });

            }
            return listLPDto;
        }

    

        public async Task<ICollection<LessonProgressDto>> GetAllInfLessonProgressClassified(int id, int grade, int semester)
        {

            var enrollmentId = _context.Enrollments
                .Where(en => en.UserId == id && en.Semester == semester && en.Grade == grade)
                .Select(en => en.EnrollmentId)
                .ToList()
                ;
            _logger.LogInformation($"Enrollment id is: {string.Join(", ", enrollmentId)}");

            var listLP = _context.LessonProgresses
                .Where(lp => lp.EnrollmentId.HasValue && enrollmentId.Contains(lp.EnrollmentId.Value))
                .ToList();


            List<LessonProgressDto> listLPDto = new List<LessonProgressDto> { };

            foreach (var lp in listLP)
            {
                var lessonDto = await _context.Lessons
                    .Where(l => l.LessonId == lp.LessonId)
                    .Select(l => new LessonDto
                    {
                        LessonOrder = l.LessonOrder,
                        LessonName = l.LessonName,
                        LessonPdfUrl = l.LessonPdfUrl,
                        LessonVideoUrl = l.LessonVideoUrl
                    }
                    ).FirstOrDefaultAsync();

                listLPDto.Add(new LessonProgressDto
                {

                    LearningProgressId = lp.LearningProgressId,
                    LessonId = lp.LessonId,
                    Status = lp.Status,
                    Lesson = lessonDto
                });

            }
            return listLPDto;
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

        public async Task<LessonProgressDto> UpdateLearningProgress(int idProgress, string status)
        {
            var progress = await _context.LessonProgresses
        .FirstOrDefaultAsync(lp => lp.LearningProgressId == idProgress);

            // Nếu không tìm thấy, trả về null
            if (progress == null)
            {
                return null;
            }
            // Cập nhật trạng thái
            progress.Status = status;

            // Lưu thay đổi vào cơ sở dữ liệu
            await _context.SaveChangesAsync();
            return await GetInfoOneLessonProgress(idProgress);
        }
    }
}
