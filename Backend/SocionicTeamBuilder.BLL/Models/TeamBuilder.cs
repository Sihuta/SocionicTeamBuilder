using SocionicTeamBuilder.BLL.Enums;
using SocionicTeamBuilder.BLL.Interfaces;
using SocionicTeamBuilder.DAL.Entities;
using SocionicTeamBuilder.DAL.Interfaces;
using Task = SocionicTeamBuilder.DAL.Entities.Task;

namespace SocionicTeamBuilder.BLL.Models
{
    public class TeamBuilder
    {
        private readonly IBlacklistService blacklistService;
        private readonly IUnitOfWork unitOfWork;
        private readonly Task task;

        private Dictionary<PlanningStyle, List<int>> empsOfEachPlanningStyle;
        private Dictionary<Quadra, List<int>> empsOfEachQuadra;
        private Dictionary<SmallGroup, List<int>> empsOfEachSmallGroup;

        public TeamBuilder(IUnitOfWork unitOfWork, Task task)
        {
            this.unitOfWork = unitOfWork;
            this.task = task;
        }

        public TeamBuilder(IUnitOfWork unitOfWork, Task task, IBlacklistService blacklistService)
            : this(unitOfWork, task)
        {
            this.blacklistService = blacklistService;
        }

        public async Task<IEnumerable<CreatedGroupOfTeams>> GetCreatedTeamsByBlacklistAsync()
        {
            var resultOfTeamBuilding = await BuildGroupsOfTeamsAsync();
            UseBlacklist(resultOfTeamBuilding);

            return resultOfTeamBuilding;
        }

        public async Task<List<CreatedGroupOfTeams>> GetCreatedTeamsAsync()
        {
            return await BuildGroupsOfTeamsAsync();
        }

        private void UseBlacklist(List<CreatedGroupOfTeams> createdGroupsOfTeams)
        {
            bool delete;
            for (int i = 0; i < createdGroupsOfTeams.Count; ++i)
            {
                delete = false;
                var group = createdGroupsOfTeams[i];

                foreach (var team in group.CreatedTeams)
                {
                    foreach (var employeeId in team.EmployeeIdList)
                    {
                        var blacklist = blacklistService.Get(employeeId).Enemies;
                        var intersection = team.EmployeeIdList.Intersect(blacklist);
                        
                        if (intersection.Any())
                        {
                            delete = true;
                            break;
                        }
                    }

                    if (delete)
                    {
                        break;
                    }
                }

                if (delete)
                {
                    createdGroupsOfTeams.RemoveAt(i);
                }
            }
        }

        private async Task<List<CreatedGroupOfTeams>> BuildGroupsOfTeamsAsync()
        {
            List<int> empIdList = GetEmpIdList();
            List<int> teamSizes = GetTeamSizes();

            if (!CheckIfEveryMemberHasSocionicType(empIdList))
                throw new Exception("Not everybody has defined socionic type!");

            await CreateDictsByWaysOfTeamBuildingAsync(empIdList);

            var teams = GetCombinationsOfTeams(teamSizes, empIdList);
            var resultOfTeamBuilding = GetGroupedCombinationsOfTeams(teams, task.EmployeeCount, new CreatedGroupOfTeams()).ToList();
            RemoveDuplicates(resultOfTeamBuilding);

            return resultOfTeamBuilding;
        }

        private IEnumerable<CreatedGroupOfTeams> GetGroupedCombinationsOfTeams
            (CreatedGroupOfTeams teams, int empCount, CreatedGroupOfTeams tempTeams)
        {
            int teamCount = task.TeamCount;
            for (int i = 0; i < teams.Count; ++i)
            {
                int leftCount = empCount - teams[i].Count;
                var groupOfTeams = new CreatedGroupOfTeams(tempTeams);

                if (!groupOfTeams.HasIntersectionWith(teams[i]))
                {
                    groupOfTeams.Add(teams[i]);
                }

                if (groupOfTeams.IsCreated(leftCount, teamCount))
                {
                    yield return groupOfTeams;
                }
                else
                {
                    var possibleGroupOfTeams = teams.Take(i).Where(n => n.Count <= empCount).ToList();
                    if (possibleGroupOfTeams.Count > 0)
                    {
                        foreach (var createdGroupOfTeams in GetGroupedCombinationsOfTeams
                            (new CreatedGroupOfTeams(possibleGroupOfTeams), leftCount, groupOfTeams))
                        {
                            if (createdGroupOfTeams.Count == teamCount)
                            {
                                yield return createdGroupOfTeams;
                            }
                        }
                    }
                }
            }
        }

        private CreatedGroupOfTeams GetCombinationsOfTeams(List<int> teamSizes, List<int> empIdLst)
        {
            var teams = new CreatedGroupOfTeams();
            var actTeams = new Action<CreatedTeam>((item) =>
            {
                if (!teams.Contains(item))
                {
                    teams.Add(item);
                }
            });

            foreach (var size in teamSizes)
            {
                GenerateCombinationsOfTeams(empIdLst, size, actTeams);
            }

            return teams;
        }

        private void GenerateCombinationsOfTeams(List<int> empIdLst, int teamSize, Action<CreatedTeam> actionResult)
        {
            var team = new int[teamSize];
            GenerateCombinations(empIdLst, team, 0, teamSize, actionResult);
        }

        private void GenerateCombinations(List<int> empIdLst, int[] team, int index, int teamSize, Action<CreatedTeam> actionResult)
        {
            if (teamSize == 0)
            {
                var teamLst = team.ToList();

                AddIfTeamIsCompatibleToWayOfBuilding(actionResult, teamLst, empsOfEachPlanningStyle, WayOfBuilding.ByPlanningStyles);
                AddIfTeamIsCompatibleToWayOfBuilding(actionResult, teamLst, empsOfEachQuadra, WayOfBuilding.ByQuadras);
                AddIfTeamIsCompatibleToWayOfBuilding(actionResult, teamLst, empsOfEachSmallGroup, WayOfBuilding.BySmallGroups);

                return;
            }
            while (index <= empIdLst.Count - teamSize)
            {
                team[team.Length - teamSize] = empIdLst[index++];
                GenerateCombinations(empIdLst, team, index, teamSize - 1, actionResult);
            }
        }

        private void RemoveDuplicates(List<CreatedGroupOfTeams> resultOfTeamBuilding)
        {
            if (resultOfTeamBuilding.Count < 2)
            {
                return;
            }

            for (int i = 0; i < resultOfTeamBuilding.Count - 1; ++i)
            {
                for (int j = 1; j < resultOfTeamBuilding.Count; ++j)
                {
                    if (i != j && resultOfTeamBuilding[i].SameAs(resultOfTeamBuilding[j]))
                    {
                        resultOfTeamBuilding.RemoveAt(j);
                    }
                }
            }
        }

        private void AddIfTeamIsCompatibleToWayOfBuilding<T>
            (Action<CreatedTeam> actionResult, List<int> team, Dictionary<T, List<int>> dictWayOfBuilding, WayOfBuilding wayOfBuilding)
        {
            foreach (var item in dictWayOfBuilding)
            {
                var inersection = item.Value.Intersect(team);
                if (inersection.Count() == team.Count)
                {
                    actionResult(new CreatedTeam(wayOfBuilding, Enum.GetName(typeof(T), item.Key), team));

                    return;
                }
            }
        }

        private List<int> GetEmpIdList()
        {
            var empIdList = new List<int>();
            var members = unitOfWork.TeamMemberRepository.Find(i => i.TaskId == task.Id);
            
            foreach (var m in members)
            {
                empIdList.Add(m.EmployeeId);
            }

            return empIdList;
        }
        private List<int> GetTeamSizes()
        {
            var teamSizes = new List<int>();
            int min = task.MinTeamSize;
            int max = (task.EmployeeCount % min) + min;
            
            for (int i = min; i <= max; ++i)
            {
                teamSizes.Add(i);
            }

            return teamSizes;
        }

        private bool CheckIfEveryMemberHasSocionicType(List<int> empIdLst)
        {
            foreach (var id in empIdLst)
            {
                if (!unitOfWork.TestingResultRepository.Find(i => i.EmployeeId == id).Any())
                {
                    return false;
                }
            }
            return true;
        }
        private async System.Threading.Tasks.Task CreateDictsByWaysOfTeamBuildingAsync(List<int> empIdLst)
        {
            empsOfEachPlanningStyle = new Dictionary<PlanningStyle, List<int>>();
            empsOfEachQuadra = new Dictionary<Quadra, List<int>>();
            empsOfEachSmallGroup = new Dictionary<SmallGroup, List<int>>();

            foreach (var empId in empIdLst)
            {
                var socionicTypeId = GetSocionicTypeId(empId);
                var socionicType = await GetSocionicTypeAsync(socionicTypeId);

                var planningStyle = GetPlanningStyleFromStr(socionicType.PlanningStyle);
                var quadra = GetQuadraFromStr(socionicType.Quadra);
                var smallGroup = GetSmallGroupFromStr(socionicType.SmallGroup);

                AddEmpIdToDict(empsOfEachPlanningStyle, planningStyle, empId);
                AddEmpIdToDict(empsOfEachQuadra, quadra, empId);
                AddEmpIdToDict(empsOfEachSmallGroup, smallGroup, empId);
            }
        }
        private void AddEmpIdToDict<T>(Dictionary<T, List<int>> dict, T wayOfBuilding, int empId)
        {
            if (!dict.ContainsKey(wayOfBuilding))
            {
                var lst = new List<int>();

                lst.Add(empId);
                dict.Add(wayOfBuilding, lst);
            }
            else
            {
                dict[wayOfBuilding].Add(empId);
            }
        }

        private int GetSocionicTypeId(int empId)
        {
            return unitOfWork.TestingResultRepository
                    .Find(i => i.EmployeeId == empId)
                    .OrderByDescending(t => t.TestingDate)
                    .First().SocionicTypeId;
        }
        private async Task<SocionicType> GetSocionicTypeAsync(int socionicTypeId)
        {
            return await unitOfWork.SocionicTypeRepository.GetAsync(socionicTypeId);
        }

        private PlanningStyle GetPlanningStyleFromStr(string str)
        {
            PlanningStyle style = PlanningStyle.Free;
            switch (str)
            {
                case "Free":
                    style = PlanningStyle.Free;
                    break;
                case "Stable":
                    style = PlanningStyle.Stable;
                    break;
                case "Stage":
                    style = PlanningStyle.Stage;
                    break;
                case "Variant":
                    style = PlanningStyle.Variant;
                    break;
            }
            return style;
        }
        private Quadra GetQuadraFromStr(string str)
        {
            Quadra quadra = Quadra.Alpha;
            switch (str)
            {
                case "Alpha":
                    quadra = Quadra.Alpha;
                    break;
                case "Beta":
                    quadra = Quadra.Beta;
                    break;
                case "Gamma":
                    quadra = Quadra.Gamma;
                    break;
                case "Delta":
                    quadra = Quadra.Delta;
                    break;
            }
            return quadra;
        }
        private SmallGroup GetSmallGroupFromStr(string str)
        {
            SmallGroup group = SmallGroup.SocialClub;
            switch (str)
            {
                case "club of investigators":
                    group = SmallGroup.InvestigatorsClub;
                    break;
                case "social club":
                    group = SmallGroup.SocialClub;
                    break;
                case "humanitarian club":
                    group = SmallGroup.HumanitarianClub;
                    break;
                case "club of practitioners":
                    group = SmallGroup.PractitionersClub;
                    break;
            }
            return group;
        }
    }
}