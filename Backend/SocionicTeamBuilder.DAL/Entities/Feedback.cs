using System;
using System.Collections.Generic;

namespace SocionicTeamBuilder.DAL.Entities
{
    public partial class Feedback
    {
        public int Id { get; set; }
        public int TeamMemberId { get; set; }
        public DateTime DateTime { get; set; }
        public string Mood { get; set; }
        public string Details { get; set; }

        public virtual TeamMember TeamMember { get; set; }
    }
}
