﻿using AIMathProject.Application.Dto.ExerciseResultDto;
using AIMathProject.Domain.Interfaces;
using AIMathProject.Application.Mappers;
using AIMathProject.Application.Dto.ExerciseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIMathProject.Infrastructure.Data;
using AIMathProject.Application.Dto.ExerciseDetailResultDto;
using Microsoft.EntityFrameworkCore;
using AIMathProject.Application.Dto.ExerciseDetailDto;
using AIMathProject.Application.Dto.ExerciseWithChapterDto;
using AIMathProject.Application.Dto.QuestionDto;

namespace AIMathProject.Infrastructure.Repositories
{
    public class ExerciseRepository : IExerciseRepository<ExerciseExtraForLessonDto>, IExerciseSummaryRepository<ExerciseWithChapterDto>
    {
        private readonly ApplicationDbContext _context;

        public ExerciseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ExerciseExtraForLessonDto>> GetExercisesWithResultsByEnrollmentId(int enrollmentId, int grade)
        {
            // Get all exercises that have been unlocked for this enrollment
            var unlockedExerciseIds = await _context.EnrollmentUnlockExercises
                .Where(eue => eue.EnrollmentId == enrollmentId)
                .Select(eue => eue.ExerciseId.Value)
                .ToListAsync();

            // Get only exercises that are:
            // 1. Locked (IsLocked = true)
            // 2. AND specifically unlocked for this enrollment in EnrollmentUnlockExercises
            var exercises = await _context.Exercises
                .Include(e => e.Lesson)
                    .ThenInclude(l => l.Chapter)
                .Where(e => e.Lesson.Chapter.Grade == grade &&
                          (e.IsLocked == true) &&
                          unlockedExerciseIds.Contains(e.ExerciseId))
                .ToListAsync();

            var exerciseDtos = new List<ExerciseExtraForLessonDto>();

            // Process each exercise
            foreach (var exercise in exercises)
            {
                // Create the basic exercise DTO
                var exerciseDto = new ExerciseExtraForLessonDto
                {
                    ExerciseName = exercise.ExerciseName,
                    ExerciseId = exercise.ExerciseId,
                    IsLocked = true, // Always true based on our filter
                    Description = exercise.Description,
                    ExerciseDetails = new List<ExerciseDetailDto>()
                };

                // Get exercise details for this exercise with all question data
                var exerciseDetails = await (from ed in _context.ExerciseDetails
                                             where ed.ExerciseId == exercise.ExerciseId
                                             select ed)
                                            .Include(ed => ed.Question)
                                                .ThenInclude(q => q.ChoiceAnswers)
                                            .Include(ed => ed.Question)
                                                .ThenInclude(q => q.MatchingAnswers)
                                            .Include(ed => ed.Question)
                                                .ThenInclude(q => q.FillAnswers)
                                            .ToListAsync();

                // Map exercise details directly to ExerciseDetailDto
                exerciseDto.ExerciseDetails = exerciseDetails
                    .Select(ed => new ExerciseDetailDto
                    {
                        Question = ed.Question?.ToQuestionDto()
                    })
                    .ToList();

                exerciseDtos.Add(exerciseDto);
            }

            return exerciseDtos;
        }


        public async Task<List<ExerciseWithChapterDto>> GetExercisesWithChapterInfoByEnrollmentId(int enrollmentId)
        {
            var exercises = await _context.Exercises
                .Where(e => e.ExerciseResults.Any(er => er.EnrollmentId == enrollmentId))
                .Include(e => e.Lesson)
                    .ThenInclude(l => l.Chapter)
                .Include(e => e.ExerciseResults.Where(er => er.EnrollmentId == enrollmentId))
                    .ThenInclude(er => er.ExerciseDetailResults)
                        .ThenInclude(edr => edr.ExerciseDetail)
                            .ThenInclude(ed => ed.Question)
                .ToListAsync();

            var exerciseDtos = exercises.Select(e => new ExerciseWithChapterDto
            {
                ExerciseId = e.ExerciseId,
                ExerciseName = e.ExerciseName,
                // Thêm thông tin về Lesson và Chapter
                Lesson = e.Lesson != null ? new LessonWithChapterDto
                {
                    LessonName = e.Lesson.LessonName,
                    Chapter = e.Lesson.Chapter != null ? new ChapterSummaryDto
                    {
                        ChapterId = e.Lesson.Chapter.ChapterId,
                        ChapterName = e.Lesson.Chapter.ChapterName,
                        ChapterOrder = e.Lesson.Chapter.ChapterOrder,
                        Grade = e.Lesson.Chapter.Grade,
                        Semester = e.Lesson.Chapter.Semester
                    } : null
                } : null,
                ExerciseResults = e.ExerciseResults
                    .Where(er => er.EnrollmentId == enrollmentId)
                    .Select(er => new ExerciseResultSummaryDto
                    {
                        Score = er.Score,
                        ExerciseDetailResults = er.ExerciseDetailResults != null ?
                            er.ExerciseDetailResults.Select(edr => new ExerciseDetailResultSummaryDto
                            {
                                IsCorrect = edr.IsCorrect,
                                Question = edr.ExerciseDetail?.Question != null ? new QuestionSummaryDto
                                {
                                    Difficulty = edr.ExerciseDetail.Question.Difficulty,
                                    ImgUrl = edr.ExerciseDetail.Question.ImgUrl,
                                    QuestionContent = edr.ExerciseDetail.Question.QuestionContent
                                } : null
                            }).ToList() : new List<ExerciseDetailResultSummaryDto>()
                    }).ToList()
            }).ToList();

            return exerciseDtos;
        }
    }
}
