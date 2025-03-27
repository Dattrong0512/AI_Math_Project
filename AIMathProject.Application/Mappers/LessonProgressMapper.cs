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
        public static LessonProgressDto ToLessonProgressDto(this LessonProgress lession)
        {
            LessonProgressDto lessionDto = new LessonProgressDto
            {
                LearningProgressId = lession.LearningProgressId,
                LessonId = lession.LessonId,
                LearningProgress = lession.LearningProgress,
                IsCompleted = lession.IsCompleted,

                Lesson = lession.Lesson != null ? lession.Lesson.ToLessonDto() : null
            };

            return lessionDto;
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
