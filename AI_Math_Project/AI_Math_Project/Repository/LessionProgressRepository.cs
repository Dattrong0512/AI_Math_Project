﻿using AI_Math_Project.Data;
using AI_Math_Project.Data.Model;
using AI_Math_Project.DTO;
using AI_Math_Project.DTO.LessionProgressDto;
using AI_Math_Project.Interfaces;
using AI_Math_Project.Mappers.LessionProgressMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Identity.Client;
using System.Linq;




namespace AI_Math_Project.Repository
{
    public class LessionProgressRepository : ILessionProgressRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly ILogger<LessionProgressRepository> _logger;
        public LessionProgressRepository(ApplicationDBContext context, ILogger<LessionProgressRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<LessionProgressDto>> GetAllInfLessionProgress(int id)
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


            List<LessionProgressDto> listLPDto = new List<LessionProgressDto> { };

            foreach (var lp in listLP)
            {
                var lessionDto = await _context.Lessons
                    .Where(l => l.LessonId == lp.LessonId)
                    .Select(l => new LessionDto
                    {
                        LessonOrder = l.LessonOrder,
                        LessonName = l.LessonName,
                        LessonContent = l.LessonContent
                    }
                    ).FirstOrDefaultAsync();

                listLPDto.Add(new LessionProgressDto {

                    LearningProgressId = lp.LearningProgressId,
                    LessonId = lp.LessonId,

                     LearningProgress = lp.LearningProgress,

                     IsCompleted = lp.IsCompleted,

                     Lesson = lessionDto
                });

            }
            return listLPDto;
        }


        public async Task<List<LessionProgressDto>> GetAllInfLessionProgressClassified(int id, int semester)
        {

            var enrollmentId = _context.Enrollments
                .Where(en => en.UserId == id && en.Semester == semester)
                .Select(en => en.EnrollmentId)
                .ToList()
                ;
            _logger.LogInformation($"Enrollment id is: {string.Join(", ", enrollmentId)}");

            var listLP = _context.LessonProgresses
                .Where(lp => lp.EnrollmentId.HasValue && enrollmentId.Contains(lp.EnrollmentId.Value))
                .ToList();


            List<LessionProgressDto> listLPDto = new List<LessionProgressDto> { };

            foreach (var lp in listLP)
            {
                var lessionDto = await _context.Lessons
                    .Where(l => l.LessonId == lp.LessonId)
                    .Select(l => new LessionDto
                    {
                        LessonOrder = l.LessonOrder,
                        LessonName = l.LessonName,
                        LessonContent = l.LessonContent
                    }
                    ).FirstOrDefaultAsync();

                listLPDto.Add(new LessionProgressDto
                {

                    LearningProgressId = lp.LearningProgressId,
                    LessonId = lp.LessonId,

                    LearningProgress = lp.LearningProgress,

                    IsCompleted = lp.IsCompleted,

                    Lesson = lessionDto
                });

            }
            return listLPDto;
        }


        public async Task<LessionProgressDto?> UpdateLearningProgress(int idProgress, short learningProgress)
        {

            int record = await _context.LessonProgresses
            .Where(lp => lp.LearningProgressId == idProgress)
            .ExecuteUpdateAsync(setter => setter
                .SetProperty(lp => lp.LearningProgress, learningProgress)
             );


            if (record == 0)
            {
                return null;
            }
            else
                return await GetInfoOneLessionProgress(idProgress);

        }
        public async Task<LessionProgressDto?> GetInfoOneLessionProgress(int lpId)
        {
            LessonProgress? lp = await _context.LessonProgresses
                .Where(lp => lp.LearningProgressId == lpId)
                .Include(lp => lp.Lesson)
                .FirstOrDefaultAsync();

            if (lp == null)
            {
                return null; 
            }

            return lp.ToLessionProgressDto(); // ✅ Trả về DTO
        }

    }
}
