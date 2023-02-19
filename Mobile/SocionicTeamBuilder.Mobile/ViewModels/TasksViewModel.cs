using SocionicTeamBuilder.Mobile.Models;
using SocionicTeamBuilder.Mobile.Services;
using SocionicTeamBuilder.Mobile.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;

namespace SocionicTeamBuilder.Mobile.ViewModels
{
    public class TasksViewModel: BaseViewModel
    {
        private Task selectedTask;

        public ObservableCollection<Task> Tasks { get; }
        public Command LoadTasksCommand { get; }
        public Command<Task> TaskTapped { get; }

        public TasksViewModel()
        {
            Tasks = new ObservableCollection<Task>();

            LoadTasksCommand = new Command(async () => await ExecuteLoadTasksCommand());
            TaskTapped = new Command<Task>(OnTaskSelected);
        }

        async System.Threading.Tasks.Task ExecuteLoadTasksCommand()
        {
            IsBusy = true;

            try
            {
                Tasks.Clear();
                var items = await TaskService.Get(App.EmployeeId);
                foreach (var item in items)
                {
                    Tasks.Add(item);
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
            SelectedTask = null;
        }

        public Task SelectedTask
        {
            get => selectedTask;
            set
            {
                SetProperty(ref selectedTask, value);
                OnTaskSelected(value);
            }
        }

        async void OnTaskSelected(Task task)
        {
            if (task == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(TeamPage)}?{nameof(TeamViewModel.TaskId)}={task.Id}");
        }
    }
}
