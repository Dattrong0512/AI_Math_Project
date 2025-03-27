using AIMathProject.Application.Dto;
using AIMathProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Mappers
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
