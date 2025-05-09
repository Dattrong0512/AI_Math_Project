using AIMathProject.Application.Dto.LessonProgressDto;
using AIMathProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Mappers
{
    public static class LessonProgressMapper
    {
        public static LessonProgressDto ToLessonProgressDto(this LessonProgress lesson)
        {
            LessonProgressDto lessonDto = new LessonProgressDto
            {
                LearningProgressId = lesson.LearningProgressId,
                LessonId = lesson.LessonId,
                Status = lesson.Status,
                Lesson = lesson.Lesson != null ? lesson.Lesson.ToLessonDto() : null
            };

            return lessonDto;
        }
        public static List<LessonProgressDto> ToLessonProgressDtoList(this List<LessonProgress> listLP)
        {
            List<LessonProgressDto> listLPDto = new List<LessonProgressDto> { };
            foreach (var LP in listLP)
            {
                listLPDto.Add(LP.ToLessonProgressDto());
            }
            return listLPDto;

        }
    }
}
