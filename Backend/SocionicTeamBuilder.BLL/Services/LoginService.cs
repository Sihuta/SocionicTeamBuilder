using SocionicTeamBuilder.BLL.DTO;
using SocionicTeamBuilder.BLL.Infrastructure;
using SocionicTeamBuilder.BLL.Interfaces;
using SocionicTeamBuilder.DAL.Entities;
using SocionicTeamBuilder.DAL.Interfaces;

namespace SocionicTeamBuilder.BLL.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly Mapper mapper;

        public LoginService(IUnitOfWork unitOfWork, Mapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }

        public UserDTO LogIn(string login, string password)
        {
            string passwordHash = SecurePassword.GetHashString(password);
            var user = unitOfWork.UserRepository
                .Find(u => u.Login == login && u.Password == passwordHash).SingleOrDefault();
            if (user == default)
            {
                return null;
            }

            return mapper.Map<User, UserDTO>(user);
        }
    }
}
