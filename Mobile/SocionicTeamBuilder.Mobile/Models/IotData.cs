using System;
using System.Collections.Generic;
using System.Text;

namespace SocionicTeamBuilder.Mobile.Models
{
    public class IotData
    {
        public int Id { get; set; }
        public int TeamMemberId { get; set; }
        public DateTime DateTime { get; set; }
        public byte? HeartBeat { get; set; }
        public double BodyTemperature { get; set; }
        public byte Pulse { get; set; }
        public bool IsFixedOne { get; set; }
    }
}
