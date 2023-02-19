using System;
using System.Collections.Generic;

namespace SocionicTeamBuilder.DAL.Entities
{
    public partial class Team
    {
        public Team()
        {
            TeamMembers = new HashSet<TeamMember>();
        }

        public int Id { get; set; }
        public string WayOfBuilding { get; set; }
        public bool IsTestedOnPractice { get; set; }

        public virtual ICollection<TeamMember> TeamMembers { get; set; }
    }
}
