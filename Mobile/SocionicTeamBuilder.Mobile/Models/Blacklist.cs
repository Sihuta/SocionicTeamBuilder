using System;
using System.Collections.Generic;
using System.Text;

namespace SocionicTeamBuilder.Mobile.Models
{
    public class Blacklist
    {
        public int EmployeeId { get; set; }
        public List<int> Enemies { get; set; }
    }
}
