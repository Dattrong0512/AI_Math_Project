using AI_Math_Project.Data.Model;
using AI_Math_Project.DTO;

namespace AI_Math_Project.Interfaces
{
    public interface IChapterRepository
    {
        Task<List<ChapterDto>> GetAllChapters();
        Task<List<ChapterDto>> GetAllDetailChapters();
    }
}
