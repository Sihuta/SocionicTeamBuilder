using SocionicTeamBuilder.Mobile.Models;
using SocionicTeamBuilder.Mobile.Services;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SocionicTeamBuilder.Mobile.ViewModels
{
    [QueryProperty(nameof(EmployeeId), nameof(EmployeeId))]
    public class EmployeeDetailViewModel : BaseViewModel
    {
        private int employeeId;

        private string login;
        private string email;
        private string fullName;
        private string socionicType;

        public string Login
        {
            get => login;
            set => SetProperty(ref login, value);
        }

        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }

        public string FullName
        {
            get => fullName;
            set => SetProperty(ref fullName, value);
        }

        public string SocionicType
        {
            get => socionicType;
            set => SetProperty(ref socionicType, value);
        }

        public int EmployeeId
        {
            get
            {
                return employeeId;
            }
            set
            {
                employeeId = value;
                LoadItemId(value);
            }
        }

        public async void LoadItemId(int employeeId)
        {
            try
            {
                var employee = await EmployeeService.Get(employeeId);

                Login = employee.Login;
                Email = employee.Email;
                FullName = employee.FullName;
                SocionicType = employee.SocionicType;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Employee");
            }
        }
    }
}
