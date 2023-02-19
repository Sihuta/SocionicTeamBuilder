using System;
using System.Collections.Generic;

namespace SocionicTeamBuilder.DAL.Entities
{
    public partial class Answer
    {
        public int Id { get; set; }
        public byte QuestionNumber { get; set; }
        public string Text { get; set; }
        public byte Score { get; set; }

        public virtual Question QuestionNumberNavigation { get; set; }
    }
}
