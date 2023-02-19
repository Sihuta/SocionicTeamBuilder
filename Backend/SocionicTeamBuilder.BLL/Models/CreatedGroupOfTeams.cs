using System.Collections;

namespace SocionicTeamBuilder.BLL.Models
{
    public class CreatedGroupOfTeams : IEnumerable<CreatedTeam>
    {
        public CreatedGroupOfTeams()
        {
            CreatedTeams = new List<CreatedTeam>(); 
        }

        public CreatedGroupOfTeams(List<CreatedTeam> teams)
        {
            CreatedTeams = new List<CreatedTeam>(teams);
        }

        public CreatedGroupOfTeams(CreatedGroupOfTeams groupOfTeams)
        {
            CreatedTeams = new List<CreatedTeam>(groupOfTeams);
        }

        public List<CreatedTeam> CreatedTeams { get; set; }
        public int Count { get => CreatedTeams.Count; }

        public void Add(CreatedTeam team)
        {
            CreatedTeams.Add(team);
        }

        public bool SameAs(CreatedGroupOfTeams groupOfTeams)
        {
            if (Count != groupOfTeams.Count)
            {
                return false;
            }

            var lst1 = GetListsFromPairs(this);
            var lst2 = GetListsFromPairs(groupOfTeams);

            var intersection = lst1.Intersect(lst2);

            return intersection.Count() == lst1.Count && intersection.Count() == lst2.Count;
        }

        public bool HasIntersectionWith(CreatedTeam team)
        {
            foreach (var t in this)
            {
                var intersection = t.Intersect(team);
                if (intersection.Count() > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsCreated(int leftCount, int teamCount)
        {
            return leftCount == 0 && Count == teamCount;
        }

        public CreatedTeam this[int ind]
        {
            get
            {
                return CreatedTeams[ind];
            }
            set
            {
                CreatedTeams[ind] = value;
            }
        }

        public IEnumerator<CreatedTeam> GetEnumerator()
        {
            return CreatedTeams.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private List<List<int>> GetListsFromPairs(CreatedGroupOfTeams groupedTeams)
        {
            var lst = new List<List<int>>();
            foreach (var team in groupedTeams)
            {
                var temp = team.EmployeeIdList;
                temp.Sort();
                lst.Add(temp);
            }

            return lst;
        }
    }
}
