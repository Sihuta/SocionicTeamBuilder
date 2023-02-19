using SocionicTeamBuilder.Mobile.Models;
using SocionicTeamBuilder.Mobile.Services;
using SocionicTeamBuilder.Mobile.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Task = System.Threading.Tasks.Task;

namespace SocionicTeamBuilder.Mobile.ViewModels
{
    public class EmployeesViewModel : BaseViewModel
    {
        private Employee selectedEmployee;

        public ObservableCollection<Employee> Employees { get; }
        public Command LoadEmployeesCommand { get; }
        public Command AddEmployeeCommand { get; }
        public Command<Employee> EmployeeTapped { get; }

        public EmployeesViewModel()
        {
            Employees = new ObservableCollection<Employee>();

            LoadEmployeesCommand = new Command(async () => await ExecuteLoadEmployeesCommand());
            EmployeeTapped = new Command<Employee>(OnEmployeeSelected);
            AddEmployeeCommand = new Command(OnAddEmployee);
        }

        async Task ExecuteLoadEmployeesCommand()
        {
            IsBusy = true;

            try
            {
                Employees.Clear();
                var enterprise = await UserService.GetEnterprise(App.User.Id);
                var items = await EmployeeService.GetByEnterprise(enterprise);
                foreach (var item in items)
                {
                    Employees.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedEmployee = null;
        }

        public Employee SelectedEmployee
        {
            get => selectedEmployee;
            set
            {
                SetProperty(ref selectedEmployee, value);
                OnEmployeeSelected(value);
            }
        }

        private async void OnAddEmployee(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewEmployeePage));
        }

        async void OnEmployeeSelected(Employee employee)
        {
            if (employee == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(EmployeeDetailPage)}?{nameof(EmployeeDetailViewModel.EmployeeId)}={employee.Id}");
        }
    }
}