using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocionicTeamBuilder.BLL.DTO
{
    public class BlacklistDTO
    {
        public BlacklistDTO() { }

        public int EmployeeId { get; set; }
        public List<int> Enemies { get; set; }
    }
}
