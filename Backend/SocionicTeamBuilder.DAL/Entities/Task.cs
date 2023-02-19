using System;
using System.Collections.Generic;

namespace SocionicTeamBuilder.DAL.Entities
{
    public partial class Task
    {
        public Task()
        {
            TeamMembers = new HashSet<TeamMember>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public byte EmployeeCount { get; set; }
        public byte TeamCount { get; set; }
        public byte MinTeamSize { get; set; }
        public bool TimeIsLimited { get; set; }
        public bool TeamsCreated { get; set; }

        public virtual ICollection<TeamMember> TeamMembers { get; set; }
    }
}
