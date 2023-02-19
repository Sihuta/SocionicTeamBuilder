using SocionicTeamBuilder.DAL.Entities;
using SocionicTeamBuilder.BLL.DTO;
using SocionicTeamBuilder.BLL.Infrastructure;
using SocionicTeamBuilder.BLL.Interfaces;
using SocionicTeamBuilder.BLL.Models;
using SocionicTeamBuilder.DAL.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace SocionicTeamBuilder.BLL.Services
{
    public class TeamBuilderService : ITeamBuilderService
    {
        private readonly IBlacklistService blacklistService;
        private readonly IUnitOfWork unitOfWork;
        private readonly Mapper mapper;

        public TeamBuilderService(IUnitOfWork unitOfWork, Mapper mapper, IBlacklistService blacklistService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.blacklistService = blacklistService;
        }

        public TeamBuilderService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<int> CreateAsync(TeamDTO team)
        {
            var t = mapper.Map<TeamDTO, Team>(team);
            
            await unitOfWork.TeamRepository.CreateAsync(t);
            await unitOfWork.CommitAsync();

            return t.Id;
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }

        public async Task<TeamDTO> GetByTeamIdAsync(int id)
        {
            return mapper.Map<Team, TeamDTO>(
                await unitOfWork.TeamRepository.GetAsync(id));
        }

        public async Task<IEnumerable<CreatedGroupOfTeamsDTO>> GetVariantsOfTeamBuildingByBlacklistAsync(int taskId)
        {
            var task = await unitOfWork.TaskRepository.GetAsync(taskId);
            var teamBuilder = new TeamBuilder(unitOfWork, task, blacklistService);

            return mapper.Map(await teamBuilder.GetCreatedTeamsByBlacklistAsync());
        }

        public async Task<IEnumerable<CreatedGroupOfTeamsDTO>> GetVariantsOfTeamBuildingAsync(int taskId)
        {
            var task = await unitOfWork.TaskRepository.GetAsync(taskId);
            var teamBuilder = new TeamBuilder(unitOfWork, task);
            
            return mapper.Map(await teamBuilder.GetCreatedTeamsAsync());
        }

        public async Task<IEnumerable<TeamFullInfoDTO>> GetTeamsAsync(int taskId)
        {
            var teamMembers = unitOfWork.TeamMemberRepository.Find(tm => tm.TaskId == taskId);
            var teams = await unitOfWork.TeamRepository.GetAllAsync();

            List<TeamFullInfoDTO> res = new List<TeamFullInfoDTO>();

            foreach (var t in teams)
            {
                if (teamMembers.Any(tm => tm.TeamId == t.Id))
                {
                    var tms = teamMembers.Where(tm => tm.TeamId == t.Id);

                    var team = new TeamFullInfoDTO()
                    {
                        TeamId = t.Id,
                        WayOfBuilding = t.WayOfBuilding,
                        IsTestedOnPractice = CheckIfTestedOnPractice(t.Id)
                    };

                    foreach (var tm in tms)
                    {
                        team.EmployeeIdList.Add(tm.EmployeeId);
                    }

                    res.Add(team);
                }
            }

            return res;
        }

        private bool CheckIfTestedOnPractice(int teamId)
        {
            var membersId = unitOfWork.TeamMemberRepository.Find(tm => tm.TeamId == teamId).Select(tm => tm.Id);
            foreach (var id in membersId)
            {
                if (unitOfWork.IotMeasurementRepository.Find(i => i.TeamMemberId == id).Any())
                {
                    return true;
                }
            }

            return false;
        }

        public async Task DeleteByTaskIdAsync(int taskId)
        {
            var tms = unitOfWork.TeamMemberRepository.Find(tm => tm.TaskId == taskId);
            foreach (var tm in tms)
            {
                if (tm.TeamId != null)
                {
                    await unitOfWork.TeamRepository.DeleteAsync((int)tm.TeamId);
                }
            }

            await unitOfWork.CommitAsync();
        }

        public async Task<TeamFullInfoDTO> GetTeamsAsync(int taskId, int employeeId)
        {
            var teams = await GetTeamsAsync(taskId);

            foreach (var t in teams)
            {
                if (t.EmployeeIdList.Contains(employeeId))
                {
                    return t;
                }
            }

            return null;
        }
    }
}