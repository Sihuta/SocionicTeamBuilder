using SocionicTeamBuilder.BLL.DTO;
using SocionicTeamBuilder.BLL.Enums;

namespace SocionicTeamBuilder.BLL.Interfaces
{
    public interface IQuestionService
    {
        QuestionDTO GetByNumber(int testId, int num);
        IEnumerable<QuestionDTO> GetAllByTestingId(int testId);
        int GetQuestionsCount(int testId);
        QuestionType GetTypeByNumber(int testId, int num);
        void Dispose();
    }
}
