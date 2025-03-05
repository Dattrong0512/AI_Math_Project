using AI_Math_Project.Data.Model;
using AI_Math_Project.DTO.AnswerDto;
using AI_Math_Project.DTO.LessionProgressDto;
using AI_Math_Project.DTO.QuestionDto;
using AI_Math_Project.Mappers;
using AI_Math_Project.Mappers.AnswerMapper;
using System.Collections.Generic;

namespace AI_Math_Project.Mappers.QuestionMapper
{
    public static class QuestionMapper
    {
        public static QuestionDto ToQuestionDto(this Question question)
        {
            QuestionDto dto = new QuestionDto
            {
                QuestionId = question.QuestionId,
                QuestionType = question.QuestionType,
                Difficulty = question.Difficulty,
                LessonId = question.LessonId,
                ImgUrl = question.ImgUrl,
                QuestionContent = question.QuestionContent,
                PdfSolution = question.PdfSolution,
                ChoiceAnswers = question.ChoiceAnswers != null ? question.ChoiceAnswers.ToChoiAnswerDtoList() : null,
                FillAnswers = question.FillAnswers != null ? question.FillAnswers.ToFillAnswerDtoList() : null,
                MatchingAnswers = question.MatchingAnswers != null ? question.MatchingAnswers.ToMatchingAnswerDtoList() : null
            };
            return dto;
        }
        public static List<QuestionDto> ToQuestionDtoList(this List<Question> Listquestion)
        {
            List<QuestionDto> listLPDto = new List<QuestionDto> { };
            foreach (var qs in Listquestion)
            {
                listLPDto.Add(qs.ToQuestionDto());
            }
            return listLPDto;
        }
    }
}
