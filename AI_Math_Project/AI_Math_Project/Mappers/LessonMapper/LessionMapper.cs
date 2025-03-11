using AI_Math_Project.Data.Model;
using AI_Math_Project.DTO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AI_Math_Project.Mappers
{
    public static class LessionMapper
    {

        public static LessonDto ToLessonDto(this Lesson lession)
        {
            LessonDto lessionDto = new LessonDto
            {

                LessonOrder = lession.LessonOrder,
                LessonName = lession.LessonName,

                LessonContent = lession.LessonContent
            };
            return lessionDto;
        }
        public static Lesson ToLessonFromLessonDto(this LessonDto lessonDto, int chapter_id)
        {
            Lesson lesson = new Lesson
            {

                LessonOrder = lessonDto.LessonOrder,


                LessonName = lessonDto.LessonName,


                ChapterId = chapter_id,


                LessonContent = lessonDto.LessonContent

            };
            return lesson;

        }

    }
}
