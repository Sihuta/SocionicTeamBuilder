using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SocionicTeamBuilder.Mobile.ViewModels
{
    public class WelcomeViewModel : BaseViewModel
    {
        public WelcomeViewModel()
        {
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("http://192.168.0.111:45455"));
        }

        public ICommand OpenWebCommand { get; }
    }
}