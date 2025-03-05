using AI_Math_Project.Data;
using AI_Math_Project.Data.Model;
using AI_Math_Project.DTO.QuestionDto;
using AI_Math_Project.Interfaces;
using AI_Math_Project.Mappers.QuestionMapper;
using Microsoft.EntityFrameworkCore;

namespace AI_Math_Project.Repository
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly ApplicationDBContext _context;
        public QuestionRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<QuestionDto>> GetAllQuestionByLessionID(int grade, int lessionOrder)
        {

            var lessionId = from Chapter in _context.Chapters
                            join Lession in _context.Lessons
                            on Chapter.ChapterId equals Lession.ChapterId
                            where (Chapter.Grade == grade&&Lession.LessonOrder==lessionOrder)
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
            foreach(var question in questionList)
            {
                if (question.QuestionType== "multiple_choice")
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
            return questionListReturn.ToQuestionDtoList();





        }
    }
}
