using SocionicTeamBuilder.BLL.Interfaces;
using SocionicTeamBuilder.DAL.Entities;
using SocionicTeamBuilder.BLL.DTO;
using SocionicTeamBuilder.BLL.Infrastructure;
using SocionicTeamBuilder.DAL.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace SocionicTeamBuilder.BLL.Services
{
    public class EnterpriseService : IEnterpriseService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly Mapper mapper;

        public EnterpriseService(IUnitOfWork unitOfWork, Mapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task CreateAsync(EnterpriseDTO enterprise)
        {
            await unitOfWork.EnterpriseRepository.CreateAsync(mapper.Map<EnterpriseDTO, Enterprise>(enterprise));
            await unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await unitOfWork.EnterpriseRepository.DeleteAsync(id);
            await unitOfWork.CommitAsync();
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }

        public async Task<IEnumerable<EnterpriseDTO>> GetAllAsync()
        {
            return mapper.Map<Enterprise, EnterpriseDTO>(
                await unitOfWork.EnterpriseRepository.GetAllAsync());
        }

        public async Task<string> GetByUserIdAsync(int userId)
        {
            int id = (int)unitOfWork.EmployeeRepository.Find(e => e.UserId == userId).SingleOrDefault().EnterpriseId;
            var enterprise = await unitOfWork.EnterpriseRepository.GetAsync(id);
            return enterprise.Name;
        }

        public async Task UpdateAsync(EnterpriseDTO enterprise)
        {
            unitOfWork.EnterpriseRepository.Update(mapper.Map<EnterpriseDTO, Enterprise>(enterprise));
            await unitOfWork.CommitAsync();
        }
    }
}