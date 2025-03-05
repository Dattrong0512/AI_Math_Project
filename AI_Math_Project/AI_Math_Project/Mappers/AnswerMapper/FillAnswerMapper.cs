using AI_Math_Project.Data.Model;
using AI_Math_Project.DTO.AnswerDto;
using System.Runtime.CompilerServices;

namespace AI_Math_Project.Mappers.AnswerMapper
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
