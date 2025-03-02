using AI_Math_Project.Data.Model;
using AI_Math_Project.DTO;
using AI_Math_Project.DTO.LessionProgressDto;
using System.Runtime.CompilerServices;

namespace AI_Math_Project.Mappers.LessionProgressMapper
{
    public static class LessionProgressMapper
    {
        public static LessionProgressDto ToLessionProgressDto(this LessonProgress lession)
        {
            LessionProgressDto lessionDto = new LessionProgressDto
            {
                LearningProgressId = lession.LearningProgressId,
                LessonId = lession.LessonId,
                LearningProgress = lession.LearningProgress,
                IsCompleted = lession.IsCompleted,

                Lesson = lession.Lesson != null ? lession.Lesson.ToLessionDto() : null
            };

            return lessionDto;
        }
        public static List<LessionProgressDto> ToLessionProgressDtoList(this List<LessonProgress> listLP)
        {
            List<LessionProgressDto> listLPDto = new List<LessionProgressDto> { };
            foreach (var LP in listLP)
            {
                listLPDto.Add(LP.ToLessionProgressDto());
            }
            return listLPDto;


        }


    }
}
