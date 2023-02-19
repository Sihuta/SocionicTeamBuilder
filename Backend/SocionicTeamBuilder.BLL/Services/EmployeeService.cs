using SocionicTeamBuilder.BLL.DTO;
using SocionicTeamBuilder.BLL.Interfaces;
using SocionicTeamBuilder.DAL.Entities;
using SocionicTeamBuilder.DAL.Interfaces;
using SocionicTeamBuilder.BLL.Infrastructure;
using Task = System.Threading.Tasks.Task;

namespace SocionicTeamBuilder.BLL.Services
{
    public class EmployeeService : IEmployeeService
    {
        private IUnitOfWork unitOfWork;
        private IUserService userService;
        private Mapper mapper;

        public EmployeeService(IUnitOfWork unitOfWork, IUserService userService, Mapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.userService = userService;
            this.mapper = mapper;
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }

        public async Task CreateAsync(EmployeeDTO employee)
        {
            await CreateUserAsync(employee);
            await CreateEmployeeAsync(employee);
        }

        private async Task CreateEmployeeAsync(EmployeeDTO employee)
        {
            int userId = unitOfWork.UserRepository.Find(u => u.Login == employee.Login).SingleOrDefault().Id;
            int enterpriseId = unitOfWork.EnterpriseRepository.Find(e => e.Name == employee.Enterprise).SingleOrDefault().Id;
            var emp = new Employee()
            {
                EnterpriseId = enterpriseId,
                FullName = employee.FullName,
                UserId = userId
            };

            await unitOfWork.EmployeeRepository.CreateAsync(emp);
            await unitOfWork.CommitAsync();
        }

        private async Task CreateUserAsync(EmployeeDTO employee)
        {
            var user = new User()
            {
                Email = employee.Email,
                Login = employee.Login,
                Password = employee.Password,
                Role = "employee"
            };
            await userService.CreateAsync(user);
        }

        public async Task<IEnumerable<EmployeeDTO>> GetAllAsync()
        {
            var list = await unitOfWork.EmployeeRepository.GetAllAsync();
            var res = mapper.Map<IEnumerable<Employee>, List<EmployeeDTO>>(
                list);
            return res;
        }

        public async Task<IEnumerable<EmployeeDTO>> GetByEnterpriseAsync(string enterprise)
        {
            var employees = await unitOfWork.EmployeeRepository.GetAllAsync();
            var enterprises = unitOfWork.EnterpriseRepository.Find(e => e.Name == enterprise);
            var users = unitOfWork.UserRepository.Find(u => u.Role == "employee");

            var testingResults = await unitOfWork.TestingResultRepository.GetAllAsync();
            var socionicTypes = await unitOfWork.SocionicTypeRepository.GetAllAsync();

            var employeesTypes = socionicTypes.Join(testingResults,
                s => s.Id,
                t => t.SocionicTypeId,
                (s, t) => new
                {
                    Type = s.Name,
                    EmployeeId = t.EmployeeId,
                    WorkingProfile = s.WorkingProfile
                }
                );

            var employeesOfTheEnterprise = employees.Join(enterprises,
                em => em.EnterpriseId,
                en => en.Id,
                (em, en) => new
                {
                    Enterprise = en.Name,
                    FullName = em.FullName,
                    Id = em.Id,
                    UserId = em.UserId,
                });

            var usersOfTheEnterprise = employeesOfTheEnterprise.Join(users,
                e => e.UserId,
                u => u.Id,
                (e, u) => new
                {
                    Email = u.Email,
                    Enterprise = e.Enterprise,
                    FullName = e.FullName,
                    Id = e.Id,
                    Login = u.Login,
                });

            return usersOfTheEnterprise.GroupJoin(employeesTypes,
                u => u.Id,
                s => s.EmployeeId,
                (u, s) => new EmployeeDTO
                {
                    Email = u.Email,
                    Enterprise = u.Enterprise,
                    FullName = u.FullName,
                    Id = u.Id,
                    Login = u.Login,
                    SocionicType = s.LastOrDefault() == null ? "Undefined" : s.LastOrDefault().Type,
                    WorkingProfile = s.LastOrDefault() == null ? "Undefined" : s.LastOrDefault().WorkingProfile,
                });
        }

        public async Task<EmployeeDTO> GetByUserIdAsync(int userId)
        {
            var user = await unitOfWork.UserRepository.GetAsync(userId);
            var employee = unitOfWork.EmployeeRepository.Find(e => e.UserId == userId).SingleOrDefault();
            var testingResult = unitOfWork.TestingResultRepository
                .Find(tr => tr.EmployeeId == employee.Id)
                .OrderByDescending(tr => tr.TestingDate)
                .FirstOrDefault();

            string socionicType = testingResult == null ?
                "Undefined" :
                unitOfWork.SocionicTypeRepository
                    .Find(s => s.Id == testingResult.SocionicTypeId)
                    .SingleOrDefault().Name;

            return new EmployeeDTO
            {
                Email = user.Email,
                FullName = employee.FullName,
                Id = employee.Id,
                Login = user.Login,
                SocionicType = socionicType,
                DateOfBirth = (DateTime)employee.YearOfBirth,
                Password = user.Password
            };
        }

        public async Task UpdateAsync(EmployeeDTO employee)
        {
            var empl = await unitOfWork.EmployeeRepository.GetAsync(employee.Id);
            var user = await unitOfWork.UserRepository.GetAsync((int)empl.UserId);

            if (unitOfWork.UserRepository
                .Find(u => u.Login == employee.Login && u.Id != user.Id).Any())
            {
                throw new ArgumentException("User with such login is already exists.");
            }

            empl.FullName = employee.FullName;
            empl.YearOfBirth = employee.DateOfBirth;
            user.Email = employee.Email;
            user.Login = employee.Login;

            unitOfWork.UserRepository.Update(user);
            unitOfWork.EmployeeRepository.Update(empl);
            await unitOfWork.CommitAsync();
        }

        public async Task<SocionicTypeDTO> GetSocionicTypeAsync(int id)
        {
            int socionicTypeId = unitOfWork.TestingResultRepository
                .Find(tr => tr.EmployeeId == id)
                .OrderByDescending(tr => tr.TestingDate)
                .FirstOrDefault()
                .SocionicTypeId;

            return mapper.Map<SocionicType, SocionicTypeDTO>(
                await unitOfWork.SocionicTypeRepository.GetAsync(socionicTypeId));
        }

        public async Task<EmployeeDTO> GetAsync(int id)
        {
            var employee = await unitOfWork.EmployeeRepository.GetAsync(id);
            var user = await unitOfWork.UserRepository.GetAsync((int)employee.UserId);
            var testingResult = unitOfWork.TestingResultRepository
                .Find(tr => tr.EmployeeId == employee.Id)
                .OrderByDescending(tr => tr.TestingDate)
                .FirstOrDefault();

            string socionicType = testingResult == null ?
                "Undefined" :
                unitOfWork.SocionicTypeRepository
                    .Find(s => s.Id == testingResult.SocionicTypeId)
                    .SingleOrDefault().Name;

            var empl =  new EmployeeDTO
            {
                Email = user.Email,
                FullName = employee.FullName,
                Id = employee.Id,
                Login = user.Login,
                SocionicType = socionicType,
                DateOfBirth = employee.YearOfBirth == null ? DateTime.Now : (DateTime)employee.YearOfBirth,
                Password = user.Password
            };

            return empl;
        }
    }
}
