using SocionicTeamBuilder.BLL.DTO;
using SocionicTeamBuilder.BLL.Infrastructure;
using SocionicTeamBuilder.BLL.Interfaces;
using SocionicTeamBuilder.DAL.Entities;
using SocionicTeamBuilder.DAL.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace SocionicTeamBuilder.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly Mapper mapper;

        public UserService(IUnitOfWork unitOfWork, Mapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<int> CreateAsync(UserDTO user)
        {
            if (unitOfWork.UserRepository.Find(u => u.Login == user.Login).Any())
            {
                throw new ArgumentException("User with such login already exists.");
            }

            user.Password = SecurePassword.GetHashString(user.Password);
            
            await unitOfWork.UserRepository.CreateAsync(mapper.Map<UserDTO, User>(user));
            await unitOfWork.CommitAsync();

            int id = unitOfWork.UserRepository.Find(u => u.Login == user.Login).SingleOrDefault().Id;
            if (user.EnterpriseId != 0)
            {
                await CreateEmployeeAsync(id, user.EnterpriseId);
            }

            return id;
        }

        private async Task CreateEmployeeAsync(int userId, int enterpriseId)
        {
            var employee = new Employee()
            {
                UserId = userId,
                EnterpriseId = enterpriseId
            };

            await unitOfWork.EmployeeRepository.CreateAsync(employee);
            await unitOfWork.CommitAsync();
        }

        public async Task CreateAsync(User user)
        {
            if (unitOfWork.UserRepository.Find(u => u.Login == user.Login).Any())
            {
                throw new ArgumentException("User with such login already exists.");
            }

            user.Password = SecurePassword.GetHashString(user.Password);
            
            await unitOfWork.UserRepository.CreateAsync(user);
            await unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await unitOfWork.UserRepository.DeleteAsync(id);
            await unitOfWork.CommitAsync();
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }

        public async Task UpdateAsync(UserDTO user)
        {
            user.Password = SecurePassword.GetHashString(user.Password);
            unitOfWork.UserRepository.Update(mapper.Map<UserDTO, User>(user));
            
            await unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            return mapper.Map<User, UserDTO>(
                await unitOfWork.UserRepository.GetAllAsync());
        }
    }
}