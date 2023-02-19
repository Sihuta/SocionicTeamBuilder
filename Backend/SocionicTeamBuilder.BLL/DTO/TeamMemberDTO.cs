namespace SocionicTeamBuilder.BLL.DTO
{
    public class TeamMemberDTO
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int? TeamId { get; set; }
        public int TaskId { get; set; }
        public string Position { get; set; }
    }
}
