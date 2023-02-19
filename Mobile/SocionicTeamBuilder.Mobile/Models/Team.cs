using System;
using System.Collections.Generic;
using System.Text;

namespace SocionicTeamBuilder.Mobile.Models
{
    public class Team
    {
        public int TeamId { get; set; }
        public List<int> EmployeeIdList { get; set; }
        public string WayOfBuilding { get; set; }
        public bool IsTestedOnPractice { get; set; }
    }
}
