using SocionicTeamBuilder.Mobile.Models;
using SocionicTeamBuilder.Mobile.Services;
using SocionicTeamBuilder.Mobile.Views;
using System.Diagnostics;
using Xamarin.Forms;

namespace SocionicTeamBuilder.Mobile.ViewModels
{
    public class SocionicTypeViewModel : BaseViewModel
    {
        private int employeeId;

        private string pseudonym;
        private string name;
        private string jungDichotomies;
        private string raininSigns;
        private string smallGroup;
        private string description;
        private string workingProfile;

        private bool typeUndefined = false;
        private bool typeDefined = false;

        public SocionicTypeViewModel()
        {
            PassTestingCommand = new Command(OnPassTestingClicked);
        }

        internal void OnAppearing()
        {
            LoadData();
        }

        public Command PassTestingCommand { get; }
        public bool SocionicTypeUndefined
        {
            get => typeUndefined;
            set => SetProperty(ref typeUndefined, value);
        }
        public bool SocionicTypeDefined
        {
            get => typeDefined;
            set => SetProperty(ref typeDefined, value);
        }

        public int EmployeeId
        {
            get => employeeId;
            set
            {
                employeeId = value;
            }
        }

        public string Pseudonym
        {
            get => pseudonym;
            set => SetProperty(ref pseudonym, value);
        }
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
        public string JungDichotomies
        {
            get => jungDichotomies;
            set => SetProperty(ref jungDichotomies, value);
        }
        public string RaininSigns
        {
            get => raininSigns;
            set => SetProperty(ref raininSigns, value);
        }
        public string SmallGroup
        {
            get => smallGroup;
            set => SetProperty(ref smallGroup, value);
        }
        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }
        public string WorkingProfile
        {
            get => workingProfile;
            set => SetProperty(ref workingProfile, value);
        }

        private async void OnPassTestingClicked(object obj)
        {
            await Shell.Current.GoToAsync($"{nameof(TestingPage)}");
        }

        private async void LoadData()
        {
            var employee = await EmployeeService.GetByUserId(App.User.Id);

            if (employee.SocionicType == "Undefined")
            {
                SocionicTypeUndefined = true;
                return;
            }

            EmployeeId = App.EmployeeId;
            SocionicTypeUndefined = false;
            SocionicTypeDefined = true;

            LoadSocionicType(EmployeeId);
        }

        private async void LoadSocionicType(int employeeId)
        {
            SocionicType type = await EmployeeService.GetSocionicType(employeeId);
            if (type != null)
            {
                Name = type.Name;
                Pseudonym = type.Pseudonym;
                JungDichotomies = type.JungDichotomies;
                RaininSigns = type.RaininSigns;
                SmallGroup = type.SmallGroup;
                Description = type.Description;
                WorkingProfile = type.WorkingProfile;

                return;
            }

            Debug.WriteLine("Socionic type is null");
        }
    }
}
