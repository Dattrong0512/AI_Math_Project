
using AI_Math_Project.Data.Model;
using AI_Math_Project.DTO;
using System.Net;

namespace AI_Math_Project.Mappers.ChapterMappers
{
    public static class ChapterMappers
    {

        public static List<ChapterDto> ToChapterDtoList(this List<Chapter> chapters)
        {
            List<ChapterDto> ListDtoReturn = new List<ChapterDto> { };
            foreach(var chapter in chapters)
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
                Lessons = null
            };
            return dto;
        }

    }
}
