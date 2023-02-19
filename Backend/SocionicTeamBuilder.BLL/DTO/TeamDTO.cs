namespace SocionicTeamBuilder.BLL.DTO
{
    public class TeamDTO
    {
        public TeamDTO() { }

        public int Id { get; set; }
        public string WayOfBuilding { get; set; }
        public bool IsApproved { get; set; }
        public bool IsTestedOnPractice { get; set; }
    }
}
