using SocionicTeamBuilder.Mobile.Models;
using SocionicTeamBuilder.Mobile.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;
using Task = System.Threading.Tasks.Task;

namespace SocionicTeamBuilder.Mobile.ViewModels
{
    public class NewEnemyViewModel : BaseViewModel
    {
        public ObservableCollection<Employee> Employees { get; }

        public Command LoadEmployeesCommand { get; }
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        public List<int> Enemies;
        private int enemiesCount;

        public NewEnemyViewModel()
        {
            Enemies = new List<int>();
            Employees = new ObservableCollection<Employee>();

            LoadEmployeesCommand = new Command(async () => await ExecuteLoadEmployeesCommand());

            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        public int EnemiesCount
        {
            get => enemiesCount;
            set => SetProperty(ref enemiesCount, value);
        }

        private bool ValidateSave()
        {
            return EnemiesCount > 0;
        }

        private async void OnCancel()
        {
            await Return();
        }

        private async Task Return()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            var blacklist = new Blacklist()
            {
                EmployeeId = App.EmployeeId,
                Enemies = Enemies
            };

            var res = await BlacklistService.CreateAsync(blacklist);
            if (res)
            {
                await Return();
            }
            else
            {
                await DisplayAlert("SaveFailTitle", "SendFeedbackFailMsg");
            }
        }

        private async Task DisplayAlert(string titleKey, string msgKey)
        {
            string title = TranslateExtension.GetValue(titleKey);
            string msg = TranslateExtension.GetValue(msgKey);

            await Application.Current.MainPage.DisplayAlert(title, msg, "OK");
        }

        async Task ExecuteLoadEmployeesCommand()
        {
            IsBusy = true;

            try
            {
                var blacklist = await BlacklistService.Get(App.EmployeeId);
                var enterprise = await UserService.GetEnterprise(App.User.Id);
                var employees = await EmployeeService.GetByEnterprise(enterprise);

                Employees.Clear();
                foreach (var emp in employees)
                {
                    if (!blacklist.Enemies.Contains(emp.Id) && emp.Id != App.EmployeeId)
                    {
                        Employees.Add(emp);
                    }
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

        public async void OnAppearing()
        {
            IsBusy = true;
            await ExecuteLoadEmployeesCommand();
        }
    }
}
