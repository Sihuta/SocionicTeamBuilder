using SocionicTeamBuilder.BLL.DTO;

namespace SocionicTeamBuilder.BLL.Interfaces
{
    public interface ITestingService
    {
        Task<TestingDTO> GetAsync(int id);
        Task<IEnumerable<TestingDTO>> GetAllAsync();
        Task<string> GetSocionicTypeFromTestResultAsync(int testingId, int employeeId, Dictionary<string, int> scores);
        void Dispose();
    }
}
