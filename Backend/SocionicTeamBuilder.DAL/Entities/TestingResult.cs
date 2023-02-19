using System;
using System.Collections.Generic;

namespace SocionicTeamBuilder.DAL.Entities
{
    public partial class TestingResult
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int TestingId { get; set; }
        public int SocionicTypeId { get; set; }
        public bool IsAccurate { get; set; }
        public DateTime TestingDate { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual SocionicType SocionicType { get; set; }
        public virtual Testing Testing { get; set; }
    }
}
