using SocionicTeamBuilder.BLL.DTO;

namespace SocionicTeamBuilder.BLL.Interfaces
{
    public interface ITeamBuilderService
    {
        Task<IEnumerable<CreatedGroupOfTeamsDTO>> GetVariantsOfTeamBuildingAsync(int taskId);
        Task<IEnumerable<CreatedGroupOfTeamsDTO>> GetVariantsOfTeamBuildingByBlacklistAsync(int taskId);
        Task<IEnumerable<TeamFullInfoDTO>> GetTeamsAsync(int taskId);
        Task<TeamFullInfoDTO> GetTeamsAsync(int taskId, int employeeId);
        Task<int> CreateAsync(TeamDTO team);
        Task DeleteByTaskIdAsync(int taskId);
        void Dispose();
    }
}
