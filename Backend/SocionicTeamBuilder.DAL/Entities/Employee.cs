using System;
using System.Collections.Generic;

namespace SocionicTeamBuilder.DAL.Entities
{
    public partial class Employee
    {
        public Employee()
        {
            EnemyEmployee1s = new HashSet<Enemy>();
            EnemyEmployee2s = new HashSet<Enemy>();
            TeamMembers = new HashSet<TeamMember>();
            TestingResults = new HashSet<TestingResult>();
        }

        public int Id { get; set; }
        public int? EnterpriseId { get; set; }
        public int? UserId { get; set; }
        public string FullName { get; set; }
        public DateTime? YearOfBirth { get; set; }

        public virtual Enterprise Enterprise { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Enemy> EnemyEmployee1s { get; set; }
        public virtual ICollection<Enemy> EnemyEmployee2s { get; set; }
        public virtual ICollection<TeamMember> TeamMembers { get; set; }
        public virtual ICollection<TestingResult> TestingResults { get; set; }
    }
}
