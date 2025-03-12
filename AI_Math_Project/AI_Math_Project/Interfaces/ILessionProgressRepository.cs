using AI_Math_Project.DTO.LessionProgressDto;

namespace AI_Math_Project.Interfaces
{
    public interface ILessonProgressRepository 
    {
        Task<List<LessonProgressDto>> GetAllInfLessonProgress(int id);
        Task<List<LessonProgressDto>> GetAllInfLessonProgressClassified(int id, int semester);
        Task<LessonProgressDto?> UpdateLearningProgress(int idProgress, short learningProgress);
        Task<LessonProgressDto?> GetInfoOneLessonProgress(int lpId);
    }
}
