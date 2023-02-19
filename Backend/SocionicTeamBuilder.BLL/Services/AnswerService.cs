using SocionicTeamBuilder.BLL.DTO;
using SocionicTeamBuilder.BLL.Infrastructure;
using SocionicTeamBuilder.BLL.Interfaces;
using SocionicTeamBuilder.DAL.Entities;
using SocionicTeamBuilder.DAL.Interfaces;

namespace SocionicTeamBuilder.BLL.Services
{
    public class AnswerService : IAnswerService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly Mapper mapper;

        public AnswerService(IUnitOfWork unitOfWork, Mapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }

        public IEnumerable<AnswerDTO> GetAllByQuestionNum(int num)
        {
            return mapper.Map<Answer, AnswerDTO>(unitOfWork.AnswerRepository.Find(i => i.QuestionNumber == num));
        }
    }
}
