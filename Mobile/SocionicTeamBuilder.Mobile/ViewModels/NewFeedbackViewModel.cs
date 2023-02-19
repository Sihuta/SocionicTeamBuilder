using SocionicTeamBuilder.Mobile.Models;
using SocionicTeamBuilder.Mobile.Services;
using System;
using Xamarin.Forms;
using Task = System.Threading.Tasks.Task;

namespace SocionicTeamBuilder.Mobile.ViewModels
{
    [QueryProperty(nameof(TaskId), nameof(TaskId))]
    public class NewFeedbackViewModel : BaseViewModel
    {
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private int taskId;
        private string mood;
        private string details;

        public NewFeedbackViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !string.IsNullOrWhiteSpace(mood);
        }

        public int TaskId
        {
            get => taskId;
            set
            {
                taskId = value;
            }
        }

        public string Mood
        {
            get => mood;
            set => SetProperty(ref mood, value);
        }

        public string Details
        {
            get => details;
            set => SetProperty(ref details, value);
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
            var newFeedback = new Feedback()
            {
                TeamMemberId = App.TeamMemberId,
                Mood = Mood,
                Details = Details,
                DateTime = DateTime.Now,
            };

            var res = await FeedbackService.Create(newFeedback);
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
    }
}
