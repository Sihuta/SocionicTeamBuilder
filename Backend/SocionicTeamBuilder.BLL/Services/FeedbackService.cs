using SocionicTeamBuilder.BLL.DTO;
using SocionicTeamBuilder.BLL.Infrastructure;
using SocionicTeamBuilder.BLL.Interfaces;
using SocionicTeamBuilder.DAL.Entities;
using SocionicTeamBuilder.DAL.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace SocionicTeamBuilder.BLL.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly Mapper mapper;

        public FeedbackService(IUnitOfWork unitOfWork, Mapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task CreateAsync(FeedbackDTO feedbackDTO)
        {
            var feedback = mapper.Map<FeedbackDTO, Feedback>(feedbackDTO);

            await unitOfWork.FeedbackRepository.CreateAsync(feedback);
            await unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await unitOfWork.FeedbackRepository.DeleteAsync(id);
            await unitOfWork.CommitAsync();
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }

        public IEnumerable<FeedbackDTO> Get(int teamMemberId)
        {
            return mapper.Map<Feedback, FeedbackDTO>(unitOfWork.FeedbackRepository
                .Find(f => f.TeamMemberId == teamMemberId).OrderByDescending(f => f.DateTime));
        }

        public async Task<FeedbackDTO> GetAsync(int id)
        {
            return mapper.Map<Feedback, FeedbackDTO>(await unitOfWork.FeedbackRepository.GetAsync(id));
        }

        public IEnumerable<FeedbackDTO> GetByDateRange(int teamMemberId, DateTime startDate, DateTime endDate)
        {
            return mapper.Map<Feedback, FeedbackDTO>(unitOfWork.FeedbackRepository
                .Find(f => f.TeamMemberId == teamMemberId && f.DateTime.Date >= startDate.Date && f.DateTime.Date <= endDate.Date)
                .OrderByDescending(f => f.DateTime));
        }

        public async Task UpdateAsync(FeedbackDTO feedbackDTO)
        {
            unitOfWork.FeedbackRepository.Update(mapper.Map<FeedbackDTO, Feedback>(feedbackDTO));
            await unitOfWork.CommitAsync();
        }
    }
}
