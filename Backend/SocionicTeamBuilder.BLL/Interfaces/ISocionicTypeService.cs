using SocionicTeamBuilder.BLL.DTO;

namespace SocionicTeamBuilder.BLL.Interfaces
{
    public interface ISocionicTypeService
    {
        void Dispose();
        Task<SocionicTypeDTO> GetByEmployeeIdAsync(int employeeId);
    }
}
