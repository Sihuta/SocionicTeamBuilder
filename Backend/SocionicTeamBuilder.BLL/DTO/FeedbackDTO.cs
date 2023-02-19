using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocionicTeamBuilder.BLL.DTO
{
    public class FeedbackDTO
    {
        public FeedbackDTO() { }

        public int Id { get; set; }
        public int TeamMemberId { get; set; }
        public DateTime DateTime { get; set; }
        public string Mood { get; set; }
        public string Details { get; set; }
    }
}
