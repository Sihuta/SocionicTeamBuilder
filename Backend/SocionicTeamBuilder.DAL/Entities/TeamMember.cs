using System;
using System.Collections.Generic;

namespace SocionicTeamBuilder.DAL.Entities
{
    public partial class TeamMember
    {
        public TeamMember()
        {
            Feedbacks = new HashSet<Feedback>();
            IoTmeasurements = new HashSet<IotMeasurement>();
        }

        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int? TeamId { get; set; }
        public int TaskId { get; set; }
        public string Position { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Task Task { get; set; }
        public virtual Team Team { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<IotMeasurement> IoTmeasurements { get; set; }
    }
}
