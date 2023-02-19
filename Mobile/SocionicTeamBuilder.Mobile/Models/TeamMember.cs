using System;
using System.Collections.Generic;
using System.Text;

namespace SocionicTeamBuilder.Mobile.Models
{
    public class TeamMember
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int? TeamId { get; set; }
        public int TaskId { get; set; }
        public string Position { get; set; }
    }
}
