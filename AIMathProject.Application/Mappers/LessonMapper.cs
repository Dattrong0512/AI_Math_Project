using AIMathProject.Application.Dto.LessonDto;
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

        public static LessonDto ToLessonDto(this Lesson lesson)
        {
            LessonDto lessonDto = new LessonDto
            {
                LessonOrder = lesson.LessonOrder,
                LessonName = lesson.LessonName,
                LessonPdfUrl = lesson.LessonPdfUrl,
                LessonVideoUrl = lesson.LessonVideoUrl,
                ChapterOrder = null
            };
            return lessonDto;
        }
        public static Lesson ToLessonFromLessonDto(this LessonDto lessonDto, int chapter_id)
        {
            Lesson lesson = new Lesson
            {

                LessonOrder = lessonDto.LessonOrder,
                LessonName = lessonDto.LessonName,
                ChapterId = chapter_id,
                LessonPdfUrl = lessonDto.LessonPdfUrl,
                LessonVideoUrl = lessonDto.LessonVideoUrl

            };
            return lesson;

        }

    }
}
