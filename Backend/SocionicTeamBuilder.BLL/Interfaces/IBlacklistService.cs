using SocionicTeamBuilder.BLL.DTO;

namespace SocionicTeamBuilder.BLL.Interfaces
{
    public interface IBlacklistService
    {
        void Dispose();
        Task CreateAsync(BlacklistDTO blacklistDTO);
        Task DeleteAsync(int employeeId, int enemyId);
        BlacklistDTO Get(int employeeId);
    }
}
