using SocionicTeamBuilder.BLL.DTO;
using SocionicTeamBuilder.BLL.Infrastructure;
using SocionicTeamBuilder.BLL.Interfaces;
using SocionicTeamBuilder.DAL.Entities;
using SocionicTeamBuilder.DAL.Interfaces;

namespace SocionicTeamBuilder.BLL.Services
{
    public class SocionicTypeService : ISocionicTypeService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly Mapper mapper;

        public SocionicTypeService(IUnitOfWork unitOfWork, Mapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }

        public async Task<SocionicTypeDTO> GetByEmployeeIdAsync(int employeeId)
        {
            int typeId;
            try
            {
                typeId = unitOfWork.TestingResultRepository
                    .Find(i => i.EmployeeId == employeeId)
                    .OrderByDescending(tr => tr.TestingDate)
                    .FirstOrDefault().SocionicTypeId;
            }
            catch
            {
                return null;
            }

            return mapper.Map<SocionicType, SocionicTypeDTO>(
                await unitOfWork.SocionicTypeRepository.GetAsync(typeId));
        }
    }
}
