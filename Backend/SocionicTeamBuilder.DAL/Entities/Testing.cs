using System;
using System.Collections.Generic;

namespace SocionicTeamBuilder.DAL.Entities
{
    public partial class Testing
    {
        public Testing()
        {
            Questions = new HashSet<Question>();
            TestingResults = new HashSet<TestingResult>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<TestingResult> TestingResults { get; set; }
    }
}
