using AIMathProject.Application.Dto.ExerciseDetailDto;
using AIMathProject.Application.Dto.ExerciseDto;
using AIMathProject.Application.Dto.ExerciseWithChapterDto;
using AIMathProject.Application.Dto.LessonDto;
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
    public class LessonRepository : ILessonRepository<LessonDto>, ILessonWithExerciseRepository<LessonWithChapterAndExerciseDto>
    {
        private readonly ApplicationDbContext _context;

        public LessonRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateLesson(int grade, int chapterorder, LessonDto lessionDto)
        {
            // Existing code remains unchanged
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

        public async Task<LessonDto> GetDetailLessonById(int grade, int lesson_order)
        {
            var lessonQuery = from chapter in _context.Chapters
                              join lesson in _context.Lessons on chapter.ChapterId equals lesson.ChapterId
                              where chapter.Grade == grade && lesson.LessonOrder == lesson_order
                              select new
                              {
                                  Lesson = lesson,
                                  ChapterOrder = chapter.ChapterOrder
                              };

            var lessonData = await lessonQuery.FirstOrDefaultAsync();
            if (lessonData == null)
                return new LessonDto();

            var mainExercise = await (from e in _context.Exercises
                                      where e.LessonId == lessonData.Lesson.LessonId &&
                                            e.ExerciseName.Contains(lessonData.Lesson.LessonName)
                                      select e).FirstOrDefaultAsync();

            // Khởi tạo MainExercise DTO
            ExerciseExtraForLessonDto mainExerciseDto = null;

            if (mainExercise != null)
            {
                var exerciseDetails = await (from ed in _context.ExerciseDetails
                                             join q in _context.Questions on ed.QuestionId equals q.QuestionId
                                             where ed.ExerciseId == mainExercise.ExerciseId
                                             select ed)
                                  .Include(ed => ed.Question)
                                      .ThenInclude(q => q.ChoiceAnswers)
                                  .Include(ed => ed.Question)
                                      .ThenInclude(q => q.MatchingAnswers)
                                  .Include(ed => ed.Question)
                                      .ThenInclude(q => q.FillAnswers)
                                  .ToListAsync();

                mainExerciseDto = new ExerciseExtraForLessonDto
                {
                    ExerciseId = mainExercise.ExerciseId,
                    ExerciseName = mainExercise.ExerciseName,
                    IsLocked = mainExercise.IsLocked,
                    Description = mainExercise.Description,
                    TimeLimit = mainExercise.TimeLimit,
                    ExerciseDetails = exerciseDetails.Select(ed => new ExerciseDetailDto
                    {
                        Question = ed.Question?.ToQuestionDto()
                    }).ToList()
                };
            }


            // Lấy danh sách các bài tập phụ kèm chi tiết câu hỏi
            var extraExercisesQuery = from e in _context.Exercises
                                      where e.LessonId == lessonData.Lesson.LessonId &&
                                           (mainExercise == null || e.ExerciseId != mainExercise.ExerciseId)
                                      select e;

            var extraExercises = await extraExercisesQuery.ToListAsync();
            var extraExerciseDtos = new List<ExerciseExtraForLessonDto>();

            foreach (var exercise in extraExercises)
            {
                // Tạo DTO cho mỗi exercise phụ
                var exerciseDto = new ExerciseExtraForLessonDto
                {
                    ExerciseId = exercise.ExerciseId,
                    ExerciseName = exercise.ExerciseName,
                    IsLocked = exercise.IsLocked,
                    TimeLimit = exercise.TimeLimit,
                    Description = exercise.Description,
                    ExerciseDetails = new List<ExerciseDetailDto>()
                };

                // Chỉ lấy exercise details nếu bài tập không bị khóa
                if (!(exercise.IsLocked ?? false))
                {
                    // Lấy tất cả exercise details cho mỗi exercise phụ
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

                    exerciseDto.ExerciseDetails = exerciseDetails.Select(ed => new ExerciseDetailDto
                    {
                        Question = ed.Question?.ToQuestionDto()
                    }).ToList();
                }

                extraExerciseDtos.Add(exerciseDto);
            }

            // Tạo và trả về LessonDto
            var lessonDto = new LessonDto
            {
                LessonId = lessonData.Lesson.LessonId,
                LessonOrder = lessonData.Lesson.LessonOrder,
                LessonName = lessonData.Lesson.LessonName,
                LessonVideoUrl = lessonData.Lesson.LessonVideoUrl,
                LessonPdfUrl = lessonData.Lesson.LessonPdfUrl,
                ChapterOrder = lessonData.ChapterOrder,
                MainExercise = mainExerciseDto,
                ExtraExercise = extraExerciseDtos,
            };

            return lessonDto;
        }

        public async Task<ICollection<LessonDto>> GetDetailLessonByName(int grade, string lesson_name)
        {
            // Lấy thông tin bài học cùng với lesson ID
            var lessonsQuery = from chapter in _context.Chapters
                               join lesson in _context.Lessons on chapter.ChapterId equals lesson.ChapterId
                               where chapter.Grade == grade
                               select new
                               {
                                   LessonId = lesson.LessonId,
                                   LessonName = lesson.LessonName,
                                   LessonOrder = lesson.LessonOrder,
                                   LessonPdfUrl = lesson.LessonPdfUrl,
                                   LessonVideoUrl = lesson.LessonVideoUrl,
                                   ChapterOrder = chapter.ChapterOrder
                               };

            var lessonList = await lessonsQuery.ToListAsync();

            // Lọc bài học theo tên
            var filteredLessons = lessonList
                .Where(l => RemoveDiacriticsUtils.RemoveDiacritics(l.LessonName.ToLower())
                       .Contains(RemoveDiacriticsUtils.RemoveDiacritics(lesson_name.ToLower())))
                .ToList();

            // Danh sách kết quả
            var result = new List<LessonDto>();

            // Với mỗi bài học phù hợp, tìm bài tập chính và bài tập phụ
            foreach (var l in filteredLessons)
            {
                // Tìm bài tập chính
                var mainExercise = await (from e in _context.Exercises
                                          where e.LessonId == l.LessonId &&
                                                e.ExerciseName.Contains(l.LessonName)
                                          select e).FirstOrDefaultAsync();
                ExerciseExtraForLessonDto mainExerciseDto = null;

                if (mainExercise != null)
                {
                    var exerciseDetails = await (from ed in _context.ExerciseDetails
                                                 join q in _context.Questions on ed.QuestionId equals q.QuestionId
                                                 where ed.ExerciseId == mainExercise.ExerciseId
                                                 select ed)
                                      .Include(ed => ed.Question)
                                          .ThenInclude(q => q.ChoiceAnswers)
                                      .Include(ed => ed.Question)
                                          .ThenInclude(q => q.MatchingAnswers)
                                      .Include(ed => ed.Question)
                                          .ThenInclude(q => q.FillAnswers)
                                      .ToListAsync();

                    mainExerciseDto = new ExerciseExtraForLessonDto
                    {
                        ExerciseId = mainExercise.ExerciseId,
                        ExerciseName = mainExercise.ExerciseName,
                        IsLocked = mainExercise.IsLocked,
                        TimeLimit = mainExercise.TimeLimit,
                        Description = mainExercise.Description,
                        ExerciseDetails = exerciseDetails.Select(ed => new ExerciseDetailDto
                        {
                            Question = ed.Question?.ToQuestionDto()
                        }).ToList()
                    };
                }
                // Tìm các bài tập phụ kèm chi tiết câu hỏi
                var extraExercisesQuery = from e in _context.Exercises
                                          where e.LessonId == l.LessonId &&
                                               (mainExercise == null || e.ExerciseId != mainExercise.ExerciseId)
                                          select e;

                var extraExercises = await extraExercisesQuery.ToListAsync();
                var extraExerciseDtos = new List<ExerciseExtraForLessonDto>();

                foreach (var exercise in extraExercises)
                {
                    // Tạo DTO cho mỗi exercise phụ
                    var exerciseDto = new ExerciseExtraForLessonDto
                    {
                        ExerciseId = exercise.ExerciseId,
                        ExerciseName = exercise.ExerciseName,
                        IsLocked = exercise.IsLocked,
                        TimeLimit = exercise.TimeLimit,
                        Description = exercise.Description,
                        ExerciseDetails = new List<ExerciseDetailDto>() // Initialize with empty list
                    };

                    // Chỉ lấy exercise details nếu bài tập không bị khóa
                    if (!(exercise.IsLocked ?? false))
                    {
                        // Lấy tất cả exercise details cho mỗi exercise phụ
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

                        exerciseDto.ExerciseDetails = exerciseDetails.Select(ed => new ExerciseDetailDto
                        {
                            Question = ed.Question?.ToQuestionDto()
                        }).ToList();
                    }

                    extraExerciseDtos.Add(exerciseDto);
                }

                // Thêm vào kết quả
                result.Add(new LessonDto
                {
                    LessonName = l.LessonName,
                    LessonOrder = l.LessonOrder,
                    LessonPdfUrl = l.LessonPdfUrl,
                    LessonVideoUrl = l.LessonVideoUrl,
                    ChapterOrder = l.ChapterOrder,
                    MainExercise = mainExerciseDto,
                    ExtraExercise = extraExerciseDtos
                });
            }

            return result;
        }

        public async Task<List<LessonWithChapterAndExerciseDto>> GetLessonsWithExercises(int grade)
        {
            var lessonsWithExerciseDetails = await _context.Lessons
                .Include(l => l.Chapter)
                .Include(l => l.Exercises)
                    .ThenInclude(e => e.ExerciseDetails)
                .Where(l => l.Chapter.Grade == grade &&
                            l.Exercises.Any(e => e.ExerciseDetails.Any()))
                .Select(l => new LessonWithChapterAndExerciseDto
                {
                    LessonOrder = l.LessonOrder,
                    LessonName = l.LessonName,
                    LessonVideoUrl = l.LessonVideoUrl,
                    LessonPdfUrl = l.LessonPdfUrl,
                    Chapter = new Application.Dto.ExerciseWithChapterDto.ChapterSummaryDto
                    {
                        ChapterId = (int)l.ChapterId,
                        Grade = l.Chapter.Grade,
                        ChapterOrder = l.Chapter.ChapterOrder,
                        ChapterName = l.Chapter.ChapterName,
                        Semester = l.Chapter.Semester
                    },
                    Exercises = l.Exercises
                        .Where(e => e.ExerciseDetails.Any())
                        .Select(e => new Application.Dto.ExerciseDto.ExerciseDto
                        {
                            ExerciseName = e.ExerciseName,
                            LessonId = e.LessonId,
                            ExerciseId = e.ExerciseId,
                            IsLocked = e.IsLocked,
                            Description = e.Description,
                            TimeLimit = e.TimeLimit,
                            ExerciseResults = new List<Application.Dto.ExerciseResultDto.ExerciseResultDto>() // Always empty as requested
                        })
                        .ToList()
                })
                .ToListAsync();
            return lessonsWithExerciseDetails;
        }
    }
}