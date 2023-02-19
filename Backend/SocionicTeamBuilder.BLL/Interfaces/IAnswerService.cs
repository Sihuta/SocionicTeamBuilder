using SocionicTeamBuilder.BLL.DTO;

namespace SocionicTeamBuilder.BLL.Interfaces
{
    public interface IAnswerService
    {
        IEnumerable<AnswerDTO> GetAllByQuestionNum(int num);
        void Dispose();
    }
}
