namespace SocionicTeamBuilder.BLL.DTO
{
    public partial class QuestionDTO
    {
        public QuestionDTO() { }

        public int TestingId { get; set; }
        public string DichotomyAbbreveation { get; set; }
        public string Type { get; set; }
        public byte Number { get; set; }
        public string Text { get; set; }
    }
}
