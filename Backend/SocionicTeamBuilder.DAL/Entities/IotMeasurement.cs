using System;
using System.Collections.Generic;

namespace SocionicTeamBuilder.DAL.Entities
{
    public partial class IotMeasurement
    {
        public int Id { get; set; }
        public int TeamMemberId { get; set; }
        public DateTime DateTime { get; set; }
        public byte? HeartBeat { get; set; }
        public double BodyTemperature { get; set; }
        public byte Pulse { get; set; }
        public bool IsFixedOne { get; set; }
        public bool IsCritical { get; set; }

        public virtual TeamMember TeamMember { get; set; }
    }
}
