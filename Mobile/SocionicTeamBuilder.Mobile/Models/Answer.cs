using System;
using System.Collections.Generic;
using System.Text;

namespace SocionicTeamBuilder.Mobile.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public byte QuestionNumber { get; set; }
        public string Text { get; set; }
        public byte Score { get; set; }
    }
}
