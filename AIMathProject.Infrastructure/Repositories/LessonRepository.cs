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
            var lessonId = from Chapter in _context.Chapters
                           join Lesson in _context.Lessons
                           on Chapter.ChapterId equals Lesson.ChapterId
                           where (Chapter.Grade == grade && Lesson.LessonOrder == lesson_order)
                           select (Lesson.LessonId);


            var questionId = (from Excercise in _context.Exercises
                              join ExcerciseDt in _context.ExerciseDetails
                              on Excercise.ExerciseId equals ExcerciseDt.ExerciseId
                              where Excercise.LessonId == lessonId.FirstOrDefault()
                              select ExcerciseDt.QuestionId)
                          .ToList();

            var questionList = await _context.Questions
                .Where(qs => questionId.Contains(qs.QuestionId))
                .ToListAsync();

            List<Question> questionListReturn = new List<Question>() { };
            foreach (var question in questionList)
            {
                if (question.QuestionType == "multiple_choice")
                {
                    var Answer = _context.ChoiceAnswers
                        .Where(choice => choice.QuestionId == question.QuestionId)
                        .ToList();
                    question.ChoiceAnswers = Answer;

                }
                else if (question.QuestionType == "matching")
                {
                    var Answer = _context.MatchingAnswers
                        .Where(match => match.QuestionId == question.QuestionId)
                        .ToList();
                    question.MatchingAnswers = Answer;

                }
                else if (question.QuestionType == "fill_in_blank")
                {
                    var Answer = _context.FillAnswers
                        .Where(match => match.QuestionId == question.QuestionId)
                        .ToList();
                    question.FillAnswers = Answer;

                }
                questionListReturn.Add(question);
            }
            var lesson = await _context.Lessons
                   .Where(l => l.LessonId == lessonId.FirstOrDefault())
                   .Select(l => new LessonDto
                   {
                       LessonOrder = l.LessonOrder,
                       LessonName = l.LessonName,
                       LessonVideoUrl = l.LessonVideoUrl,
                       LessonPdfUrl = l.LessonPdfUrl,
                       ChapterOrder = l.Chapter.ChapterOrder,
                       Questions = questionListReturn.ToQuestionDtoList()
                   })
                   .FirstOrDefaultAsync();
            return lesson ?? new LessonDto();
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
                             LessonPdfUrl = Lesson.LessonPdfUrl,
                             LessonVideoUrl = Lesson.LessonVideoUrl,
                             ChapterOrder = Chapter.ChapterOrder
                         };

            var lessonList = await lesson.ToListAsync();

            var filteredLessons = lessonList
                .Where(l => RemoveDiacriticsUtils.RemoveDiacritics(l.LessonName.ToLower()).Contains(RemoveDiacriticsUtils.RemoveDiacritics(lesson_name.ToLower())))
                .ToList();

            return filteredLessons;
        }

        public async Task<List<LessonWithChapterAndExerciseDto>> GetLessonsWithExercises(int grade)
        {
            var lessonsWithExercises = await _context.Lessons
                .Include(l => l.Chapter)
                .Include(l => l.Exercises)
                .Where(l => l.Chapter.Grade == grade && l.Exercises.Any())
                .Select(l => new LessonWithChapterAndExerciseDto
                {
                    LessonOrder = l.LessonOrder,
                    LessonName = l.LessonName,
                    LessonVideoUrl = l.LessonVideoUrl,
                    LessonPdfUrl = l.LessonPdfUrl,
                    Chapter = new ChapterSummaryDto
                    {
                        ChapterId = (int)l.ChapterId,
                        Grade = l.Chapter.Grade,
                        ChapterOrder = l.Chapter.ChapterOrder,
                        ChapterName = l.Chapter.ChapterName,
                        Semester = l.Chapter.Semester
                    },
                    Exercises = l.Exercises.Select(e => new Application.Dto.ExerciseDto.ExerciseDto
                    {
                        ExerciseName = e.ExerciseName,
                        LessonId = e.LessonId,
                        ExerciseResults = new List<Application.Dto.ExerciseResultDto.ExerciseResultDto>()
                    }).ToList()
                })
                .ToListAsync();

            return lessonsWithExercises;
        }
    }
}
