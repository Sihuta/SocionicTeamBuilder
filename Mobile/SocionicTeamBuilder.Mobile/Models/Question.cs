using System;
using System.Collections.Generic;
using System.Text;

namespace SocionicTeamBuilder.Mobile.Models
{
    public class Question
    {
        public int TestingId { get; set; }
        public string DichotomyAbbreveation { get; set; }
        public string Type { get; set; }
        public byte Number { get; set; }
        public string Text { get; set; }
    }
}
