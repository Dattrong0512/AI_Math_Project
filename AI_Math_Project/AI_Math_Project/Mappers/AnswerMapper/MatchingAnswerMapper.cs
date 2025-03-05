using AI_Math_Project.Data.Model;
using AI_Math_Project.DTO.AnswerDto;

namespace AI_Math_Project.Mappers.AnswerMapper
{
    public static class MatchingAnswerMapper
    {
        public static MatchingAnswerDto ToMatchingAnswerDto(this MatchingAnswer ma)

        {
            MatchingAnswerDto dto = new MatchingAnswerDto()
            {
                AnswerId = ma.AnswerId,

                CorrectAnswer = ma.CorrectAnswer,

                ImgUrl = ma.ImgUrl
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
