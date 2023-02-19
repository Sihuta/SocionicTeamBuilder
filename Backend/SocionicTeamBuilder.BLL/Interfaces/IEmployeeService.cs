using SocionicTeamBuilder.BLL.DTO;

namespace SocionicTeamBuilder.BLL.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDTO>> GetAllAsync();
        Task<EmployeeDTO> GetAsync(int id);
        Task<IEnumerable<EmployeeDTO>> GetByEnterpriseAsync(string enterprise);
        Task<EmployeeDTO> GetByUserIdAsync(int userId);
        Task<SocionicTypeDTO> GetSocionicTypeAsync(int id);


        Task CreateAsync(EmployeeDTO employee);
        Task UpdateAsync(EmployeeDTO employee);
        void Dispose();
    }
}
