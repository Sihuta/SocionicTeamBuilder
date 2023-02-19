namespace SocionicTeamBuilder.BLL.DTO
{
    public class EmployeeDTO
    {
        public EmployeeDTO() { }

        public int Id { get; set; }
        public string Enterprise { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string SocionicType { get; set; }
        public string WorkingProfile { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}
