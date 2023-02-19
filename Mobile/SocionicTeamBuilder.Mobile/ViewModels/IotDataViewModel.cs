using SocionicTeamBuilder.Mobile.Models;
using SocionicTeamBuilder.Mobile.Services;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Task = System.Threading.Tasks.Task;

namespace SocionicTeamBuilder.Mobile.ViewModels
{
    [QueryProperty(nameof(TaskId), nameof(TaskId))]
    public class IotDataViewModel: BaseViewModel
    {
        public ObservableCollection<IotData> IotData { get; }

        private int taskId;

        public IotDataViewModel()
        {
            IotData = new ObservableCollection<IotData>();
        }

        public int TaskId
        {
            get => taskId;
            set => taskId = value;
        }

        public async void OnAppearing()
        {
            await LoadIotData();
        }

        private async Task LoadIotData()
        {
            var teamMember = await TeamMemberService.GetId(TaskId, App.EmployeeId);
            var iotData = await IotDataService.Get(teamMember.Id);
            App.TeamMemberId = teamMember.Id;

            IotData.Clear();
            foreach (var data in iotData)
            {
                IotData.Add(data);
            }
        }
    }
}
