using System;
using System.Collections.Generic;

namespace SocionicTeamBuilder.DAL.Entities
{
    public partial class SocionicType
    {
        public SocionicType()
        {
            TestingResults = new HashSet<TestingResult>();
        }

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

        public virtual ICollection<TestingResult> TestingResults { get; set; }
    }
}
