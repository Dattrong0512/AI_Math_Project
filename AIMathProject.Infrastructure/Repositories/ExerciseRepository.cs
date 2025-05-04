using AIMathProject.Application.Dto.ExerciseResultDto;
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
    public class ExerciseRepository : IExerciseRepository<ExerciseDto>, IExerciseSummaryRepository<ExerciseWithChapterDto>
    {
        private readonly ApplicationDbContext _context;

        public ExerciseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ExerciseDto>> GetExercisesWithResultsByEnrollmentId(int enrollmentId)
        {
            // Lấy tất cả các bài tập, bao gồm cả những bài không có ExerciseResults
            var exercises = await _context.Exercises
                .Include(e => e.ExerciseResults)
                    .ThenInclude(er => er.ExerciseDetailResults)
                        .ThenInclude(edr => edr.ExerciseDetail)
                            .ThenInclude(ed => ed.Question)
                .ToListAsync();

            // Load câu trả lời cho từng câu hỏi (nếu có)
            foreach (var exercise in exercises)
            {
                foreach (var exerciseResult in exercise.ExerciseResults.Where(er => er.EnrollmentId == enrollmentId))
                {
                    foreach (var edr in exerciseResult.ExerciseDetailResults)
                    {
                        var question = edr.ExerciseDetail?.Question;
                        if (question == null) continue;

                        switch (question.QuestionType)
                        {
                            case "multiple_choice":
                                question.ChoiceAnswers = await _context.ChoiceAnswers
                                    .Where(ca => ca.QuestionId == question.QuestionId)
                                    .ToListAsync();
                                break;

                            case "matching":
                                question.MatchingAnswers = await _context.MatchingAnswers
                                    .Where(ma => ma.QuestionId == question.QuestionId)
                                    .ToListAsync();
                                break;

                            case "fill_in_blank":
                                question.FillAnswers = await _context.FillAnswers
                                    .Where(fa => fa.QuestionId == question.QuestionId)
                                    .ToListAsync();
                                break;
                        }
                    }
                }
            }
            var exerciseDtos = exercises.Select(e => new ExerciseDto
            {
                ExerciseName = e.ExerciseName,
                LessonId = e.LessonId,
                ExerciseResults = e.ExerciseResults
                    .Where(er => er.EnrollmentId == enrollmentId)
                    .Select(er => new ExerciseResultDto
                    {
                        ExerciseId = er.ExerciseId,
                        EnrollmentId = er.EnrollmentId,
                        Score = er.Score,
                        DoneAt = er.DoneAt,
                        ExerciseDetailResults = er.ExerciseDetailResults.Select(edr => new ExerciseDetailResultDto
                        {
                            ExerciseDetailId = edr.ExerciseDetailId,
                            ExerciseResultId = edr.ExerciseResultId,
                            IsCorrect = edr.IsCorrect,
                            ExerciseDetail = edr.ExerciseDetail != null ? new ExerciseDetailDto
                            {
                                ExerciseId = edr.ExerciseDetail.ExerciseId,
                                QuestionId = edr.ExerciseDetail.QuestionId,
                                Question = edr.ExerciseDetail.Question?.ToQuestionDto()
                            } : null
                        }).ToList()
                    }).ToList()
            }).ToList();

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
