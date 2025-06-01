using AIMathProject.Application.Dto.QuestionDto;
using AIMathProject.Application.Mappers;
using AIMathProject.Domain.Entities;
using AIMathProject.Domain.Interfaces;
using AIMathProject.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Infrastructure.Repositories
{
    public class QuestionRepository : IQuestionRepository<QuestionDto>
    {
        private readonly ApplicationDbContext _context;

        public QuestionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<QuestionDto>> GetAllQuestionByLessonID(int grade, int lessonOrder)
        {
            var lessionId = from Chapter in _context.Chapters
                            join Lession in _context.Lessons
                            on Chapter.ChapterId equals Lession.ChapterId
                            where (Chapter.Grade == grade && Lession.LessonOrder == lessonOrder)
                            select (Lession.LessonId);

            var questionId = (from Excercise in _context.Exercises
                              join ExcerciseDt in _context.ExerciseDetails
                              on Excercise.ExerciseId equals ExcerciseDt.ExerciseId
                              where Excercise.LessonId == lessionId.FirstOrDefault()
                              select ExcerciseDt.QuestionId)
                          .ToList();

            var questionList = await _context.Questions
                .Where(qs => questionId.Contains(qs.QuestionId))
                .ToListAsync();

            List<Question> questionListReturn = new List<Question>() { };
            foreach (var question in questionList)
            {
                if (question.QuestionType == "multiple_choice" || question.QuestionType == "single_choice")
                {
                    var Answer = _context.ChoiceAnswers
                        .Where(choice => choice.QuestionId == question.QuestionId)
                        .ToList();
                    question.ChoiceAnswers = Answer;

                }
                else if (question.QuestionType == "image_matching" || question.QuestionType == "text_image_matching")
                {
                    var Answer = _context.MatchingAnswers
                        .Where(match => match.QuestionId == question.QuestionId)
                        .ToList();
                    question.MatchingAnswers = Answer;

                }
                else if (question.QuestionType == "fill_in_blank" || question.QuestionType == "vertical_calculation_add" || question.QuestionType == "vertical_calculation_sub" || question.QuestionType == "vertical_calculation_multi" || question.QuestionType == "vertical_calculation_div")
                {
                    var Answer = _context.FillAnswers
                        .Where(match => match.QuestionId == question.QuestionId)
                        .ToList();
                    question.FillAnswers = Answer;

                }
                questionListReturn.Add(question);
            }
            return questionListReturn.ToQuestionDtoList();
        }
    }
}
