using AI_Math_Project.DTO.LessionProgressDto;

namespace AI_Math_Project.Interfaces
{
    public interface ILessionProgressRepository 
    {
        Task<List<LessionProgressDto>> GetAllInfLessionProgress(int id);
        Task<List<LessionProgressDto>> GetAllInfLessionProgressClassified(int id, int semester);
        Task<LessionProgressDto?> UpdateLearningProgress(int idProgress, short learningProgress);
        Task<LessionProgressDto?> GetInfoOneLessionProgress(int lpId);
    }
}
