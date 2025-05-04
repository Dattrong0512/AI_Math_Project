using AIMathProject.Application.Dto.QuestionDto;
using AIMathProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Mappers
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

        public static QuestionDto ToQuestionSummaryDto(this Question question)
        {
            QuestionDto dto = new QuestionDto
            {
                Difficulty = question.Difficulty,
                ImgUrl = question.ImgUrl,
                QuestionContent = question.QuestionContent,
            };
            return dto;
        }
    }
}
