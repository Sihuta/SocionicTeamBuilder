using SocionicTeamBuilder.BLL.DTO;

namespace SocionicTeamBuilder.BLL.Interfaces
{
    public interface IFeedbackService
    {
        void Dispose();
        Task CreateAsync(FeedbackDTO feedbackDTO);
        Task UpdateAsync(FeedbackDTO feedbackDTO);
        Task DeleteAsync(int id);
        IEnumerable<FeedbackDTO> GetByDateRange(int teamMemberId, DateTime startDate, DateTime endDate);
        IEnumerable<FeedbackDTO> Get(int teamMemberId);
        Task<FeedbackDTO> GetAsync(int id);
    }
}
