using SocionicTeamBuilder.DAL.Entities;
using SocionicTeamBuilder.BLL.DTO;
using SocionicTeamBuilder.BLL.Infrastructure;
using SocionicTeamBuilder.BLL.Interfaces;
using SocionicTeamBuilder.BLL.Models;
using SocionicTeamBuilder.DAL.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace SocionicTeamBuilder.BLL.Services
{
    public class TestingService : ITestingService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly Mapper mapper;

        public TestingService(IUnitOfWork unitOfWork, Mapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task DeleteAsync(int id)
        {
            await unitOfWork.TestingRepository.DeleteAsync(id);
            await unitOfWork.CommitAsync();
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }

        public IEnumerable<TestingDTO> Find(Func<TestingDTO, bool> predicate)
        {
            return mapper.Map<Testing, TestingDTO>(unitOfWork.TestingRepository
                .Find(mapper.Map<Func<TestingDTO, bool>, Func<Testing, bool>>(predicate)));
        }

        public async Task<TestingDTO> GetAsync(int id)
        {
            return mapper.Map<Testing, TestingDTO>(
                await unitOfWork.TestingRepository.GetAsync(id));
        }

        public async Task<IEnumerable<TestingDTO>> GetAllAsync()
        {
            return mapper.Map<Testing, TestingDTO>(
                await unitOfWork.TestingRepository.GetAllAsync());
        }

        public async Task CreateAsync(TestingDTO item)
        {
            await unitOfWork.TestingRepository.CreateAsync(mapper.Map<TestingDTO, Testing>(item));
            await unitOfWork.CommitAsync();
        }

        public async Task<string> GetSocionicTypeFromTestResultAsync(int testingId, int employeeId, Dictionary<string, int> scores)
        {
            var testCheker = new TestCheker(unitOfWork, scores);
            int finalScore = testCheker.GetFinalScore();
            
            TestingResult mbtestingResult = new TestingResult()
            {
                EmployeeId = employeeId,
                TestingId = testingId,
                SocionicTypeId = unitOfWork.SocionicTypeRepository
                    .Find(x => x.Mbvalue == finalScore)
                    .SingleOrDefault().Id,
                IsAccurate = testCheker.IsExactResult,
                TestingDate = DateTime.Now
            };

            await unitOfWork.TestingResultRepository.CreateAsync(mbtestingResult);
            await unitOfWork.CommitAsync();

            var res = await unitOfWork.SocionicTypeRepository.GetAsync(mbtestingResult.SocionicTypeId);
            return res.Name;
        }
    }
}
