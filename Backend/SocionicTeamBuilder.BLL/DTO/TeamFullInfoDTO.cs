namespace SocionicTeamBuilder.BLL.DTO
{
    public class TeamFullInfoDTO
    {
        public TeamFullInfoDTO()
        {
            EmployeeIdList = new List<int>();
        }

        public int TeamId { get; set; }
        public List<int> EmployeeIdList { get; set; }
        public string WayOfBuilding { get; set; }
        public bool IsTestedOnPractice { get; set; }
    }
}
