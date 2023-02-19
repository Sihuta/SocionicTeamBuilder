using SocionicTeamBuilder.BLL.Enums;
using SocionicTeamBuilder.DAL.Interfaces;

namespace SocionicTeamBuilder.BLL.Models
{
    public class TestCheker
    {
        public bool IsExactResult = true;

        private readonly Dictionary<string, int> scoresByDichotomies;
        private readonly IUnitOfWork unitOfWork;

        public TestCheker(IUnitOfWork unitOfWork, Dictionary<string, int> scores)
        {
            this.unitOfWork = unitOfWork;
            scoresByDichotomies = scores;
        }

        public int GetFinalScore()
        {
            int res = 0;
            foreach (var d in Enum.GetNames(typeof(Dichotomy)))
            {
                int score = scoresByDichotomies[d];
                int controlSum = unitOfWork.DichotomyRepository.Find(i => i.DichotomyAbbreveation == d)
                    .First().ControlSum;

                if (score > controlSum)
                {
                    res += unitOfWork.DichotomyRepository.Find(i => i.DichotomyAbbreveation == d)
                        .First().IfMoreThanSumValue;
                }
                else
                {
                    if (score == controlSum)
                    {
                        IsExactResult = false;
                    }
                    res += unitOfWork.DichotomyRepository.Find(i => i.DichotomyAbbreveation == d)
                        .First().IfLessThanSumValue;
                }
            }

            return res;
        }
    }
}
