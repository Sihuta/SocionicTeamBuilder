using System;
using System.Collections.Generic;
using System.Text;

namespace SocionicTeamBuilder.Mobile.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public byte EmployeeCount { get; set; }
        public byte TeamCount { get; set; }
        public byte MinTeamSize { get; set; }
        public bool TimeIsLimited { get; set; }
        public bool TeamsCreated { get; set; }
    }
}
