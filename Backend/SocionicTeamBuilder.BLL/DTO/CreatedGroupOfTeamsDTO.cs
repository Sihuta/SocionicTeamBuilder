using SocionicTeamBuilder.BLL.Enums;
using SocionicTeamBuilder.BLL.Models;

namespace SocionicTeamBuilder.BLL.DTO
{
    public class CreatedGroupOfTeamsDTO
    {
        public CreatedGroupOfTeamsDTO(CreatedGroupOfTeams groupOfTeams)
        {
            CreatedTeams = new List<CreatedTeamDTO>();

            var wayOfBuilding = groupOfTeams[0].WayOfBuilding;
            bool sameWays = true;

            foreach (var team in groupOfTeams)
            {
                var teamDTO = new CreatedTeamDTO(team);
                CreatedTeams.Add(teamDTO);

                if (wayOfBuilding != team.WayOfBuilding && sameWays)
                {
                    sameWays = false;
                    DescriptionCode = "diffWays"; //"Highly recommended to choose the variant with the same way of team building"
                }
            }

            if (sameWays)
            {
                switch (wayOfBuilding)
                {
                    case WayOfBuilding.ByPlanningStyles:
                        DescriptionCode = "bestIfDeadline"; //"Best practice when there is a strict deadline"
                        break;
                    case WayOfBuilding.ByQuadras:
                        DescriptionCode = "bestOne"; //"Best practice ever but only when time is not limited for task"
                        break;
                    case WayOfBuilding.BySmallGroups:
                        DescriptionCode = "worstOne"; //"The last appropriate variant when there isn't other options"
                        break;
                }
            }
        }

        public List<CreatedTeamDTO> CreatedTeams { get; set; }
        public int Count { get => CreatedTeams.Count; }
        public string DescriptionCode { get; set; }
    }
}