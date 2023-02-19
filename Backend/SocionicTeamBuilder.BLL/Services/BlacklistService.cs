using SocionicTeamBuilder.BLL.DTO;
using SocionicTeamBuilder.BLL.Interfaces;
using SocionicTeamBuilder.DAL.Entities;
using SocionicTeamBuilder.DAL.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace SocionicTeamBuilder.BLL.Services
{
    public class BlacklistService : IBlacklistService
    {
        private readonly IUnitOfWork unitOfWork;

        public BlacklistService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task CreateAsync(BlacklistDTO blacklistDTO)
        {
            int employeeId = blacklistDTO.EmployeeId;
            foreach (var id in blacklistDTO.Enemies)
            {
                await unitOfWork.EnemyRepository.CreateAsync(new Enemy
                {
                    Employee1Id = employeeId,
                    Employee2Id = id
                });
            }

            await unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(int employeeId, int enemyId)
        {
            var enemy = unitOfWork.EnemyRepository
                .Find(e => e.Employee1Id == employeeId && e.Employee2Id == enemyId)
                .SingleOrDefault();

            await unitOfWork.EnemyRepository.DeleteAsync(enemy.Id);
            await unitOfWork.CommitAsync();
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }

        public BlacklistDTO Get(int employeeId)
        {
            var enemies = unitOfWork.EnemyRepository.Find(e => e.Employee1Id == employeeId);

            var list = new List<int>();
            foreach (var enemy in enemies)
            {
                list.Add(enemy.Employee2Id);
            }

            return new BlacklistDTO
            {
                EmployeeId = employeeId,
                Enemies = list
            };
        }
    }
}
