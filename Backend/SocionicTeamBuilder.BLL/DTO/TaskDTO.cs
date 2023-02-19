namespace SocionicTeamBuilder.BLL.DTO
{
    public class TaskDTO
    {
        public TaskDTO() { }

        public int Id { get; set; }
        public string Title { get; set; }
        public byte EmployeeCount { get; set; }
        public byte TeamCount { get; set; }
        public byte MinTeamSize { get; set; }
        public bool TimeIsLimited { get; set; }
        public bool TeamsCreated { get; set; }
    }
}
