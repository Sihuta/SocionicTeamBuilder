using SocionicTeamBuilder.Mobile.Models;
using SocionicTeamBuilder.Mobile.Services;
using SocionicTeamBuilder.Mobile.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;
using Task = System.Threading.Tasks.Task;

namespace SocionicTeamBuilder.Mobile.ViewModels
{
    [QueryProperty(nameof(TaskId), nameof(TaskId))]
    public class TeamViewModel: BaseViewModel
    {
        public ObservableCollection<Employee> Employees { get; }
        public Command LoadTeamCommand { get; }

        public Command TapFeedbackCommand { get; }
        public Command TapIotDataCommand { get; }

        private int taskId;
        private string wayOfBuilding;

        public TeamViewModel()
        {
            Employees = new ObservableCollection<Employee>();
            LoadTeamCommand = new Command(async () => await ExecuteLoadTeamCommand());

            TapFeedbackCommand = new Command(OnFeedbackTap);
            TapIotDataCommand = new Command(OnIotDataTap);
        }

        public int TaskId
        {
            get => taskId;
            set
            {
                taskId = value;
            }
        }

        public string WayOfBuilding
        {
            get => wayOfBuilding;
            set => SetProperty(ref wayOfBuilding, value);
        }

        async void OnFeedbackTap()
        {
            await Shell.Current.GoToAsync($"{nameof(FeedbackPage)}?{nameof(FeedbackViewModel.TaskId)}={TaskId}");
        }

        async void OnIotDataTap()
        {
            await Shell.Current.GoToAsync($"{nameof(IotDataPage)}?{nameof(IotDataPage.TaskId)}={TaskId}");
        }

        async Task ExecuteLoadTeamCommand()
        {
            IsBusy = true;

            try
            {
                Employees.Clear();

                var team = await TaskService.GetTeam(TaskId, App.EmployeeId);
                foreach (var id in team.EmployeeIdList)
                {
                    Employees.Add(await EmployeeService.Get(id));
                }

                WayOfBuilding = team.WayOfBuilding;
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
    }
}
