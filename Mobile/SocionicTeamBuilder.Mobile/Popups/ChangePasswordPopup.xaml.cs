using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using SocionicTeamBuilder.Mobile.Services;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SocionicTeamBuilder.Mobile.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangePasswordPopup : PopupPage
    {
        private readonly Action<bool> setResultAction;

        public ChangePasswordPopup(Action<bool> setResultAction)
        {
            InitializeComponent();
            this.setResultAction = setResultAction;
        }

        public static async Task ChangePassword(INavigation navigation)
        {
            TaskCompletionSource<bool> completionSource = new TaskCompletionSource<bool>();

            void callback(bool didConfirm)
            {
                completionSource.TrySetResult(didConfirm);
            }

            var popup = new ChangePasswordPopup(callback);
            await navigation.PushPopupAsync(popup);
            await completionSource.Task;
        }

        public void btnOk_Clicked(object sender, EventArgs e)
        {
            ChangePassword();
            setResultAction?.Invoke(true);
            Navigation.PopPopupAsync().ConfigureAwait(false);
        }

        private async void ChangePassword()
        {
            if (await CheckInput())
            {
                App.User.Password = newPassword1.Text;
                var user = await UserService.Update(App.User);

                if (user != null)
                {
                    App.UserPassword = App.User.Password;
                    App.User = user;

                    Debug.WriteLine(user.Password);
                    return;
                }

                Debug.WriteLine("Updating user failed");
            }
        }

        private async Task<bool> CheckInput()
        {
            var oldPass = oldPassword.Text;
            var newPass1 = newPassword1.Text;
            var newPass2 = newPassword2.Text;

            string title = TranslateExtension.GetValue("InvalidInputTitle");

            if (oldPass == "" || newPass1 == "" || newPass2 == "")
            {
                await DisplayAlert(title, "InvalidInputFillMsg");
                return false;
            }

            if (oldPass != App.UserPassword)
            {
                await DisplayAlert(title, "InvalidInputOldPassMsg");
                return false;
            }

            if (newPass1 != newPass2)
            {
                await DisplayAlert(title, "InvalidInputNewPassMsg");
                return false;
            }

            if (oldPass == newPass1)
            {
                await DisplayAlert(title, "InvalidInputSamePassMsg");
                return false;
            }

            return true;
        }

        private async Task DisplayAlert(string title, string msgKey)
        {
            string msg = TranslateExtension.GetValue(msgKey);
            await Application.Current.MainPage.DisplayAlert(title, msg, "OK");
        }

        public void btnCancel_Clicked(object sender, EventArgs e)
        {
            setResultAction?.Invoke(false);
            Navigation.PopPopupAsync().ConfigureAwait(false);
        }
    }
}