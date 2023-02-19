using System;
using System.Collections.Generic;

namespace SocionicTeamBuilder.DAL.Entities
{
    public partial class Enemy
    {
        public int Id { get; set; }
        public int Employee1Id { get; set; }
        public int Employee2Id { get; set; }

        public virtual Employee Employee1 { get; set; }
        public virtual Employee Employee2 { get; set; }
    }
}
