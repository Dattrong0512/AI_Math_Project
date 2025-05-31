using AIMathProject.Application.Dto.AnswerDto;
using AIMathProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Mappers
{
    public static class MatchingAnswerMapper
    {
        public static MatchingAnswerDto ToMatchingAnswerDto(this MatchingAnswer ma)

        {
            MatchingAnswerDto dto = new MatchingAnswerDto()
            {
                AnswerId = ma.AnswerId,

                AnswerContent1 = ma.AnswerContent1,

                AnswerContent2 = ma.AnswerContent2
            };
            return dto;
        }
        public static List<MatchingAnswerDto> ToMatchingAnswerDtoList(this ICollection<MatchingAnswer> MatchingList)
        {
            List<MatchingAnswerDto> dto = new List<MatchingAnswerDto>() { };
            foreach (var choice in MatchingList)
            {
                dto.Add(choice.ToMatchingAnswerDto());
            }
            return dto;
        }
    }
}
