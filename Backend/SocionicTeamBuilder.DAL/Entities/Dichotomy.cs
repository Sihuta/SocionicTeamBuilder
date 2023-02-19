using System;
using System.Collections.Generic;

namespace SocionicTeamBuilder.DAL.Entities
{
    public partial class Dichotomy
    {
        public Dichotomy()
        {
            Questions = new HashSet<Question>();
        }

        public string DichotomyAbbreveation { get; set; }
        public byte ControlSum { get; set; }
        public int IfMoreThanSumValue { get; set; }
        public int IfLessThanSumValue { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}
