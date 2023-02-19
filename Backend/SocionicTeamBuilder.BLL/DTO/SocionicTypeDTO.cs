namespace SocionicTeamBuilder.BLL.DTO
{
    public partial class SocionicTypeDTO
    {
        public SocionicTypeDTO() { }

        public int Id { get; set; }
        public string Quadra { get; set; }
        public string Pseudonym { get; set; }
        public string Name { get; set; }
        public string JungDichotomies { get; set; }
        public string RaininSigns { get; set; }
        public string SmallGroup { get; set; }
        public string Description { get; set; }
        public string WorkingProfile { get; set; }
        public int Mbvalue { get; set; }
        public string PlanningStyle { get; set; }
    }
}
