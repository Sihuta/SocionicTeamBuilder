using SocionicTeamBuilder.BLL.DTO;

namespace SocionicTeamBuilder.BLL.Interfaces
{
    public interface IIotMeasurementService
    {
        void Dispose();
        Task<int> CreateAsync(IotMeasurementDTO measurementDTO);
        Task UpdateAsync(IotMeasurementDTO measurementDTO);
        Task DeleteAsync(int id);
        IEnumerable<IotMeasurementDTO> Get(int teamMemberId);
        IEnumerable<IotMeasurementDTO> GetByDateRange(int teamMemberId, DateTime startDate, DateTime endDate);
        IotMeasurementDTO GetFixedOne(int teamMemberId);
    }
}
