using SocionicTeamBuilder.Mobile.Models;
using SocionicTeamBuilder.Mobile.Popups;
using SocionicTeamBuilder.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SocionicTeamBuilder.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        private readonly EmployeeViewModel viewModel;

        public ProfilePage()
        {
            InitializeComponent();
            BindingContext = viewModel = new EmployeeViewModel(App.User.Id);
        }

        private async void changePassword_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value)
            {
                await ChangePasswordPopup.ChangePassword(Navigation);
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.OnAppearing();
        }
    }
}