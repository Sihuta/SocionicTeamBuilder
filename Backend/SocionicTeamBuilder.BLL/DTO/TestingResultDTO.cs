namespace SocionicTeamBuilder.BLL.DTO
{
    public class TestingResultDTO
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int TestingId { get; set; }
        public int SocionicTypeId { get; set; }
        public bool IsAccuracy { get; set; }
        public DateTime TestingDate { get; set; }
    }
}
