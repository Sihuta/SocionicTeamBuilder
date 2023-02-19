using SocionicTeamBuilder.BLL.DTO;

namespace SocionicTeamBuilder.BLL.Interfaces
{
    public interface ITaskService
    {
        void Dispose();
        Task<int> CreateAsync(TaskDTO task);
        Task UpdateAsync(TaskDTO task);
        Task DeleteTaskAsync(int id);
        Task<IEnumerable<TaskDTO>> GetTasksAsync();
        IEnumerable<TaskDTO> GetTasksWithoutTeams();
        Task<TaskDTO> GetAsync(int id);
        Task<IEnumerable<TaskDTO>> GetByEmployeeIdAsync(int employeeId);
    }
}
