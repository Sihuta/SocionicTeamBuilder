using SocionicTeamBuilder.BLL.DTO;
using SocionicTeamBuilder.DAL.Entities;
using Task = System.Threading.Tasks.Task;

namespace SocionicTeamBuilder.BLL.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAllAsync();

        Task DeleteAsync(int id);
        Task UpdateAsync(UserDTO user);
        Task<int> CreateAsync(UserDTO user);
        Task CreateAsync(User user);
        void Dispose();
    }
}
