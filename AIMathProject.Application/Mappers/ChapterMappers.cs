using AIMathProject.Application.Dto;
using AIMathProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Mappers
{
    public static class ChapterMappers
    {

        public static List<ChapterDto> ToChapterDtoList(this List<Chapter> chapters)
        {
            List<ChapterDto> ListDtoReturn = new List<ChapterDto> { };
            foreach (var chapter in chapters)
            {
                ListDtoReturn.Add(ToChapterDto(chapter));
            }
            return ListDtoReturn;
        }
        public static ChapterDto ToChapterDto(this Chapter chapter)
        {
            ChapterDto dto = new ChapterDto
            {
                Grade = chapter.Grade,
                ChapterOrder = chapter.ChapterOrder,
                ChapterName = chapter.ChapterName,
                Semester = chapter.Semester,
                Lessons = null
            };
            return dto;
        }

    }
}
