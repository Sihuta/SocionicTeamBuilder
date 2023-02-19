using System;
using System.Collections.Generic;

namespace SocionicTeamBuilder.DAL.Entities
{
    public partial class Enterprise
    {
        public Enterprise()
        {
            Employees = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Activity { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
