using SocionicTeamBuilder.BLL.Enums;
using SocionicTeamBuilder.BLL.Models;

namespace SocionicTeamBuilder.BLL.DTO
{
    public class CreatedTeamDTO
    {
        public CreatedTeamDTO(CreatedTeam team)
        {
            WayOfBuilding = Enum.GetName(typeof(WayOfBuilding), team.WayOfBuilding);
            Category = team.Category;
            EmployeeIdList = team.EmployeeIdList;
        }

        public int Count { get => EmployeeIdList.Count; }
        public string WayOfBuilding { get; private set; }
        public string Category { get; private set; }
        public List<int> EmployeeIdList { get; private set; }
    }
}
