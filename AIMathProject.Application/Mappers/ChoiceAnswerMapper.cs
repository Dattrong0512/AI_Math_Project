using AIMathProject.Application.Dto.AnswerDto;
using AIMathProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Mappers
{
    public static class ChoiceAnswerMapper
    {
        public static ChoiceAnswerDto ToChoiceAnswerDto(this ChoiceAnswer choiceaw)
        {
            ChoiceAnswerDto choiceawchoice = new ChoiceAnswerDto
            {
                AnswerId = choiceaw.AnswerId,

                Content = choiceaw.Content,

                IsCorrect = choiceaw.IsCorrect,

                ImgUrl = choiceaw.ImgUrl
            };
            return choiceawchoice;
        }
        public static List<ChoiceAnswerDto> ToChoiAnswerDtoList(this ICollection<ChoiceAnswer> choiceList)
        {
            List<ChoiceAnswerDto> dto = new List<ChoiceAnswerDto>() { };
            foreach (var choice in choiceList)
            {
                dto.Add(choice.ToChoiceAnswerDto());
            }
            return dto;
        }
    }
}
