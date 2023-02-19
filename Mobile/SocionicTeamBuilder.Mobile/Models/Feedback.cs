using System;
using System.Collections.Generic;
using System.Text;

namespace SocionicTeamBuilder.Mobile.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        public int TeamMemberId { get; set; }
        public DateTime DateTime { get; set; }
        public string Mood { get; set; }
        public string Details { get; set; }

        public string MoodIconPath { get; set; }
    }
}
