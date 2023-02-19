using System;
using System.Collections.Generic;
using System.Text;

namespace SocionicTeamBuilder.Mobile.Models
{
    public class Employee
    {
        public Employee() { }

        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string SocionicType { get; set; }
        public DateTime DateOfBirth { get; set; }

        public string Enterprise { get; set; }
    }
}
