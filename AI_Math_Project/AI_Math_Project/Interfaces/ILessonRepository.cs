using AI_Math_Project.DTO;
using AI_Math_Project.DTO.LessionProgressDto;
using Microsoft.AspNetCore.Mvc;

namespace AI_Math_Project.Interfaces
{
    public interface ILessonRepository
    {
        Task<LessonDto> GetDetailLessonById(int grade, int chapter_order, int id);
        Task<bool> CreateLesson( int grade, int chapterorder,  LessonDto lessionDto);
        Task<List<LessonDto>> GetDetailLessonByName(int grade, string lesson_name);
    }
}
