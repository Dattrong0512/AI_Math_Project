using AI_Math_Project.Data.Model;
using AI_Math_Project.DTO.AnswerDto;
using Castle.DynamicProxy;

namespace AI_Math_Project.Mappers.AnswerMapper
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
            foreach(var choice in choiceList)
            {
                dto.Add(choice.ToChoiceAnswerDto());
            }
            return dto;
        }
    }
}
                          


  
