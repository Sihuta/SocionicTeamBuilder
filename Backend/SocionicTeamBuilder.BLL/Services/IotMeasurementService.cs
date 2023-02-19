using SocionicTeamBuilder.BLL.DTO;
using SocionicTeamBuilder.BLL.Infrastructure;
using SocionicTeamBuilder.BLL.Interfaces;
using SocionicTeamBuilder.DAL.Entities;
using SocionicTeamBuilder.DAL.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace SocionicTeamBuilder.BLL.Services
{
    public class IotMeasurementService : IIotMeasurementService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly Mapper mapper;

        public IotMeasurementService(IUnitOfWork unitOfWork, Mapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<int> CreateAsync(IotMeasurementDTO measurementDTO)
        {
            var measurement = mapper.Map<IotMeasurementDTO, IotMeasurement>(measurementDTO);

            await unitOfWork.IotMeasurementRepository.CreateAsync(measurement);
            await unitOfWork.CommitAsync();

            return measurement.Id;
        }

        public async Task DeleteAsync(int id)
        {
            await unitOfWork.IotMeasurementRepository.DeleteAsync(id);
            await unitOfWork.CommitAsync();
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }

        public IEnumerable<IotMeasurementDTO> Get(int teamMemberId)
        {
            return mapper.Map<IotMeasurement, IotMeasurementDTO>(unitOfWork.IotMeasurementRepository
                .Find(m => m.TeamMemberId == teamMemberId));
        }

        public IEnumerable<IotMeasurementDTO> GetByDateRange(int teamMemberId, DateTime startDate, DateTime endDate)
        {
            return mapper.Map<IotMeasurement, IotMeasurementDTO>(unitOfWork.IotMeasurementRepository
                .Find(m => m.TeamMemberId == teamMemberId && m.DateTime.Date >= startDate.Date && m.DateTime.Date <= endDate.Date)
                .OrderBy(m => m.DateTime));
        }

        public IotMeasurementDTO GetFixedOne(int teamMemberId)
        {
            return mapper.Map<IotMeasurement, IotMeasurementDTO>(unitOfWork.IotMeasurementRepository
                .Find(m => m.TeamMemberId == teamMemberId && m.IsFixedOne)
                .FirstOrDefault());
        }

        public async Task UpdateAsync(IotMeasurementDTO measurementDTO)
        {
            unitOfWork.IotMeasurementRepository.Update(mapper.Map<IotMeasurementDTO, IotMeasurement>(measurementDTO));
            await unitOfWork.CommitAsync();
        }
    }
}
