using SocionicTeamBuilder.BLL.DTO;

namespace SocionicTeamBuilder.BLL.Interfaces
{
    public interface ILoginService
    {
        UserDTO LogIn(string login, string password);
        void Dispose();
    }
}
