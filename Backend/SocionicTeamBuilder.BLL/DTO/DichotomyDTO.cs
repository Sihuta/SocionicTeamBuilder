namespace SocionicTeamBuilder.BLL.DTO
{
    public class DichotomyDTO
    {
        public DichotomyDTO() { }

        public string DichotomyAbbreveation { get; set; }
        public byte ControlSum { get; set; }
        public int IfMoreThanSumValue { get; set; }
        public int IfLessThanSumValue { get; set; }
    }
}
