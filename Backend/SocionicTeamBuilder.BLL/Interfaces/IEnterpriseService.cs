using SocionicTeamBuilder.BLL.DTO;

namespace SocionicTeamBuilder.BLL.Interfaces
{
    public interface IEnterpriseService
    {
        Task<IEnumerable<EnterpriseDTO>> GetAllAsync();
        Task<string> GetByUserIdAsync(int userId);

        Task DeleteAsync(int id);
        Task UpdateAsync(EnterpriseDTO enterprise);
        Task CreateAsync(EnterpriseDTO enterprise);
        void Dispose();
    }
}
