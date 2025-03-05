using AI_Math_Project.DTO.QuestionDto;

namespace AI_Math_Project.Interfaces
{
    public interface IQuestionRepository
    {
        Task<List<QuestionDto>> GetAllQuestionByLessionID(int grade, int lessionOrder);
    }
}
