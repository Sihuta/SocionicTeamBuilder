using System;
using System.Collections.Generic;

namespace SocionicTeamBuilder.DAL.Entities
{
    public partial class Question
    {
        public Question()
        {
            Answers = new HashSet<Answer>();
        }

        public int TestingId { get; set; }
        public string DichotomyAbbreveation { get; set; }
        public string Type { get; set; }
        public byte Number { get; set; }
        public string Text { get; set; }

        public virtual Dichotomy DichotomyAbbreveationNavigation { get; set; }
        public virtual Testing Testing { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
    }
}
