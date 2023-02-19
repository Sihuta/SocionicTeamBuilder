using SocionicTeamBuilder.BLL.DTO;
using SocionicTeamBuilder.BLL.Infrastructure;
using SocionicTeamBuilder.BLL.Interfaces;
using SocionicTeamBuilder.DAL.Interfaces;
using Task = SocionicTeamBuilder.DAL.Entities.Task;
using System.Linq;

namespace SocionicTeamBuilder.BLL.Services
{
    public class TaskService : ITaskService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly Mapper mapper;

        public TaskService(IUnitOfWork unitOfWork, Mapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<int> CreateAsync(TaskDTO task)
        {
            var newTask = mapper.Map<TaskDTO, Task>(task);

            await unitOfWork.TaskRepository.CreateAsync(newTask);
            await unitOfWork.CommitAsync();

            return newTask.Id;
        }

        public async System.Threading.Tasks.Task DeleteTaskAsync(int id)
        {
            await unitOfWork.TaskRepository.DeleteAsync(id);
            await unitOfWork.CommitAsync();
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }

        public async System.Threading.Tasks.Task UpdateAsync(TaskDTO task)
        {
            unitOfWork.TaskRepository.Update(mapper.Map<TaskDTO, Task>(task));
            await unitOfWork.CommitAsync();
        }

        public async Task<TaskDTO> GetAsync(int id)
        {
            return mapper.Map<Task, TaskDTO>(
                await unitOfWork.TaskRepository.GetAsync(id));
        }

        public async Task<IEnumerable<TaskDTO>> GetTasksAsync()
        {
            return mapper.Map<Task, TaskDTO>(
                await unitOfWork.TaskRepository.GetAllAsync());
        }

        public async Task<IEnumerable<TaskDTO>> GetByEmployeeIdAsync(int employeeId)
        {
            var tasksId = unitOfWork.TeamMemberRepository
                .Find(tm => tm.EmployeeId == employeeId && tm.TeamId != null).Select(tm => tm.TaskId);

            var tasks = new List<TaskDTO>();
            foreach (var id in tasksId)
            {
                tasks.Add(mapper.Map<Task, TaskDTO>(await unitOfWork.TaskRepository.GetAsync(id)));
            }

            return tasks;
        }

        public IEnumerable<TaskDTO> GetTasksWithoutTeams()
        {
            return mapper.Map<Task, TaskDTO>(unitOfWork.TaskRepository
                .Find(i => i.TeamsCreated == false));
        }
    }
}
