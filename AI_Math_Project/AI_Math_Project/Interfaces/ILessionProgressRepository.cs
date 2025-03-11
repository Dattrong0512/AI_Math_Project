using AI_Math_Project.DTO.LessionProgressDto;

namespace AI_Math_Project.Interfaces
{
    public interface ILessionProgressRepository 
    {
        Task<List<LessonProgressDto>> GetAllInfLessionProgress(int id);
        Task<List<LessonProgressDto>> GetAllInfLessionProgressClassified(int id, int semester);
        Task<LessonProgressDto?> UpdateLearningProgress(int idProgress, short learningProgress);
        Task<LessonProgressDto?> GetInfoOneLessionProgress(int lpId);
    }
}
