using AIMathProject.Application.Dto.AnswerDto;
using AIMathProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Mappers
{
    public static class FillAnswerMapper
    {
        public static FillAnswerDto ToFillAnswerDto(this FillAnswer fa)
        {
            FillAnswerDto dto = new FillAnswerDto()
            {
                AnswerId = fa.AnswerId,
                CorrectAnswer = fa.CorrectAnswer,
                Position = fa.Position

            };
            return dto;
        }
        public static List<FillAnswerDto> ToFillAnswerDtoList(this ICollection<FillAnswer> FillList)
        {
            List<FillAnswerDto> dto = new List<FillAnswerDto>() { };
            foreach (var choice in FillList)
            {
                dto.Add(choice.ToFillAnswerDto());
            }
            return dto;
        }
    }
}
