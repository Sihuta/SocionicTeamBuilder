using SocionicTeamBuilder.BLL.DTO;
using SocionicTeamBuilder.BLL.Infrastructure;
using SocionicTeamBuilder.BLL.Interfaces;
using SocionicTeamBuilder.DAL.Entities;
using SocionicTeamBuilder.DAL.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace SocionicTeamBuilder.BLL.Services
{
    public class TeamMemberService : ITeamMemberService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly Mapper mapper;

        public TeamMemberService(IUnitOfWork unitOfWork, Mapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task CreateAsync(TeamMemberDTO teamMember)
        {
            teamMember.TeamId = null;
            
            await unitOfWork.TeamMemberRepository.CreateAsync(mapper.Map<TeamMemberDTO, TeamMember>(teamMember));
            await unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await unitOfWork.TeamMemberRepository.DeleteAsync(id);
            await unitOfWork.CommitAsync();
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }

        public async Task UpdateAsync(TeamMemberDTO teamMember)
        {
            unitOfWork.TeamMemberRepository.Update(mapper.Map<TeamMemberDTO, TeamMember>(teamMember));
            await unitOfWork.CommitAsync();
        }

        public TeamMemberDTO GetByTeamAndEmployeeId(int teamId, int employeeId)
        {
            return mapper.Map<TeamMember, TeamMemberDTO>(unitOfWork.TeamMemberRepository
                .Find(i => i.TeamId == teamId && i.EmployeeId == employeeId).SingleOrDefault());
        }

        public TeamMemberDTO GetByTaskAndEmployeeId(int taskId, int employeeId)
        {
            return mapper.Map<TeamMember, TeamMemberDTO>(unitOfWork.TeamMemberRepository
                .Find(i => i.TaskId == taskId && i.EmployeeId == employeeId).SingleOrDefault());
        }

        public async Task<IEnumerable<TeamMemberDTO>> GetAllAsync()
        {
            return mapper.Map<TeamMember, TeamMemberDTO>(
                await unitOfWork.TeamMemberRepository.GetAllAsync());
        }

        public IEnumerable<TeamMemberDTO> GetByTaskId(int taskId)
        {
            return mapper.Map<TeamMember, TeamMemberDTO>(unitOfWork.TeamMemberRepository
                .Find(i => i.TaskId == taskId));
        }
    }
}
