using SocionicTeamBuilder.Mobile.Models;
using SocionicTeamBuilder.Mobile.Services;
using SocionicTeamBuilder.Mobile.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;
using Task = System.Threading.Tasks.Task;

namespace SocionicTeamBuilder.Mobile.ViewModels
{
    public class BlacklistViewModel : BaseViewModel
    {
        public ObservableCollection<Employee> Employees { get; }

        public Command LoadBlacklistCommand { get; }
        public Command AddEnemiesCommand { get; }
        public Command<int> EnemyRemoved { get; }

        public BlacklistViewModel()
        {
            Employees = new ObservableCollection<Employee>();

            LoadBlacklistCommand = new Command(async () => await ExecuteLoadBlacklistCommand());
            AddEnemiesCommand = new Command(OnAddEnemies);
            EnemyRemoved = new Command<int>(OnEnemyRemoved);
        }

        async Task ExecuteLoadBlacklistCommand()
        {
            IsBusy = true;

            try
            {
                var blacklist = await BlacklistService.Get(App.EmployeeId);

                Employees.Clear();
                foreach (var id in blacklist.Enemies)
                {
                    Employees.Add(await EmployeeService.Get(id));
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
        }

        private async void OnAddEnemies(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewEnemyPage));
        }

        private async void OnEnemyRemoved(int enemyId)
        {
            await BlacklistService.DeleteAsync(App.EmployeeId, enemyId);
            IsBusy = true;
        }
    }
}
