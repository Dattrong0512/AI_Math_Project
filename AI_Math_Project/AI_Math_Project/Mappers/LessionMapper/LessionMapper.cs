using AI_Math_Project.Data.Model;
using AI_Math_Project.DTO;
using System.ComponentModel.DataAnnotations;

namespace AI_Math_Project.Mappers
{
    public static class LessionMapper
    {

        public static LessionDto ToLessionDto(this Lesson lession)
        {
            LessionDto lessionDto = new LessionDto
            {

                LessonOrder = lession.LessonOrder,
                LessonName = lession.LessonName,

                LessonContent = lession.LessonContent
            };
            return lessionDto;
        }

    }
}
