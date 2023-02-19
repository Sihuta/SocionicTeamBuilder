using SocionicTeamBuilder.BLL.Enums;
using System.Collections;

namespace SocionicTeamBuilder.BLL.Models
{
    public class CreatedTeam : IEnumerable<int>
    {
        public CreatedTeam()
        {
            EmployeeIdList = new List<int>(); 
        }

        public CreatedTeam(WayOfBuilding wayOfBuilding, string category, List<int> empIdLst)
        {
            WayOfBuilding = wayOfBuilding;
            Category = category;
            EmployeeIdList = empIdLst;
        }

        public int Count { get => EmployeeIdList.Count; }

        public WayOfBuilding WayOfBuilding { get; private set; }
        public string Category { get; private set; }
        public List<int> EmployeeIdList { get; private set; }

        public IEnumerator<int> GetEnumerator()
        {
            return EmployeeIdList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
