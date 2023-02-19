using SocionicTeamBuilder.BLL.DTO;
using SocionicTeamBuilder.BLL.Enums;
using SocionicTeamBuilder.BLL.Infrastructure;
using SocionicTeamBuilder.BLL.Interfaces;
using SocionicTeamBuilder.DAL.Entities;
using SocionicTeamBuilder.DAL.Interfaces;

namespace SocionicTeamBuilder.BLL.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly Mapper mapper;

        public QuestionService(IUnitOfWork unitOfWork, Mapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }

        public QuestionDTO GetByNumber(int testId, int num)
        {
            return mapper.Map<Question, QuestionDTO>(unitOfWork.QuestionRepository
                .Find(x => x.TestingId == testId)
                .First(y => y.Number == num));
        }

        public IEnumerable<QuestionDTO> GetAllByTestingId(int testId)
        {
            return mapper.Map<Question, QuestionDTO>(unitOfWork.QuestionRepository.Find(i => i.TestingId == testId));
        }

        public int GetQuestionsCount(int testId)
        {
            return unitOfWork.QuestionRepository.Find(i => i.TestingId == testId).Count();
        }

        public QuestionType GetTypeByNumber(int testId, int num)
        {
            string type = unitOfWork.QuestionRepository
                .Find(x => x.TestingId == testId)
                .First(y => y.Number == num).Type;

            return GetEnumByString(type);
        }

        private QuestionType GetEnumByString(string str)
        {
            return str switch
            {
                "multiple choice" => QuestionType.MultipleChoice,
                _ => QuestionType.MultipleChoice,
            };
        }
    }
}
