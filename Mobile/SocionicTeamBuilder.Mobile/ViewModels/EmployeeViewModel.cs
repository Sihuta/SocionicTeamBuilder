using SocionicTeamBuilder.Mobile.Models;
using SocionicTeamBuilder.Mobile.Services;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Task = System.Threading.Tasks.Task;

namespace SocionicTeamBuilder.Mobile.ViewModels
{
    [QueryProperty(nameof(UserId), nameof(UserId))]
    public class EmployeeViewModel : BaseViewModel
    {
        private int userId;
        private string login;
        private string password;
        private string email;
        private string fullName;
        private string socionicType;
        private DateTime dateOfBirth;

        public Command SaveCommand { get; }
        private Employee Employee { get; }

        public EmployeeViewModel(int id)
        {
            UserId = id;
            Employee = new Employee();
            SaveCommand = new Command(OnSaveClicked);
        }

        internal void OnAppearing()
        {
            LoadEmployeeAsync(UserId);
        }

        public int Id { get; set; }

        public string Login
        {
            get => login;
            set
            {
                SetProperty(ref login, value);
                Employee.Login = value;
            }
        }

        public string Password
        {
            get => password;
            set
            {
                SetProperty(ref password, value);
                Employee.Password = value;
            }
        }

        public string Email
        {
            get => email;
            set
            {
                SetProperty(ref email, value);
                Employee.Email = value;
            }
        }

        public string FullName
        {
            get => fullName;
            set
            {
                SetProperty(ref fullName, value);
                Employee.FullName = value;
            }
        }

        public string SocionicType
        {
            get => socionicType;
            set => SetProperty(ref socionicType, value);
        }

        public DateTime DateOfBirth
        {
            get => dateOfBirth;
            set
            {
                SetProperty(ref dateOfBirth, value);
                Employee.DateOfBirth = value;
            }
        }

        public int UserId
        {
            get
            {
                return userId;
            }
            set
            {
                userId = value;
                LoadEmployeeAsync(value);
            }
        }

        public async void LoadEmployeeAsync(int userId)
        {
            try
            {
                var employee = await EmployeeService.GetByUserId(userId);

                Login = employee.Login;
                Password = employee.Password;
                Email = employee.Email;
                FullName = employee.FullName;
                SocionicType = employee.SocionicType;
                DateOfBirth = employee.DateOfBirth;

                Employee.Id = employee.Id;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failed to Load Employee: " + ex.Message);
            }
        }

        private async void OnSaveClicked(object obj)
        {
            bool result = await EmployeeService.Update(Employee);
            if (result)
            {
                await DisplayAlert("SaveSuccessTitle", "SaveSuccessMsg");
            }
            else
            {
                await DisplayAlert("SaveFailTitle", "SaveFailMsg");
            }
        }

        private async Task DisplayAlert(string titleKey, string msgKey)
        {
            string title = TranslateExtension.GetValue(titleKey);
            string msg = TranslateExtension.GetValue(msgKey);

            await Application.Current.MainPage.DisplayAlert(title, msg, "OK");
        }
    }
}
