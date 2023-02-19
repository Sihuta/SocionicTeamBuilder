using SocionicTeamBuilder.BLL.DTO;

namespace SocionicTeamBuilder.BLL.Interfaces
{
    public interface ITeamMemberService
    {
        void Dispose();
        Task CreateAsync(TeamMemberDTO teamMember);
        Task UpdateAsync(TeamMemberDTO teamMember);
        Task DeleteAsync(int id);
        Task<IEnumerable<TeamMemberDTO>> GetAllAsync();
        IEnumerable<TeamMemberDTO> GetByTaskId(int taskId);
        TeamMemberDTO GetByTeamAndEmployeeId(int teamId, int employeeId);
        TeamMemberDTO GetByTaskAndEmployeeId(int taskId, int employeeId);
    }
}