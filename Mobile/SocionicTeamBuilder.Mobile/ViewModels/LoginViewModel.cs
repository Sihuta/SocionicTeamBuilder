using SocionicTeamBuilder.Mobile.Models;
using SocionicTeamBuilder.Mobile.Services;
using SocionicTeamBuilder.Mobile.Views;
using System.Diagnostics;
using Xamarin.Forms;

namespace SocionicTeamBuilder.Mobile.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }

        private string login;
        private string password;

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
        }

        public string Login
        {
            get => login;
            set => SetProperty(ref login, value);
        }

        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        private async void OnLoginClicked(object obj)
        {
            var user = await UserService.LogIn(Login, Password);
            if (user == null)
            {
                string title = TranslateExtension.GetValue("LoginFailTitle");
                string msg = TranslateExtension.GetValue("LoginFailMsg");

                await Application.Current.MainPage.DisplayAlert(title, msg, "OK");
                return;
            }

            Debug.WriteLine(user.Role);
            var employee = await EmployeeService.GetByUserId(user.Id);

            App.User = user;
            App.UserPassword = Password;
            App.EmployeeId = employee.Id;

            Application.Current.MainPage = new AppShell();
        }
    }
}
