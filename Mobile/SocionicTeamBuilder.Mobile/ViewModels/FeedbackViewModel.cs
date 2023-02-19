using SocionicTeamBuilder.Mobile.Models;
using SocionicTeamBuilder.Mobile.Services;
using SocionicTeamBuilder.Mobile.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;
using Task = System.Threading.Tasks.Task;

namespace SocionicTeamBuilder.Mobile.ViewModels
{
    [QueryProperty(nameof(TaskId), nameof(TaskId))]
    public class FeedbackViewModel: BaseViewModel
    {
        public ObservableCollection<Feedback> Feedback { get; }
        public Command LoadFeedbackCommand { get; }
        public Command AddFeedbackCommand { get; }
        public Command<int> FeedbackRemoved { get; }
        public Command<Feedback> FeedbackTapped { get; }

        private Feedback selectedFeedback;
        private int taskId;
        //private DateTime date;

        public FeedbackViewModel()
        {
            Feedback = new ObservableCollection<Feedback>();

            LoadFeedbackCommand = new Command(async () => await ExecuteLoadFeedbackCommand());
            FeedbackTapped = new Command<Feedback>(OnFeedbackSelected);
            FeedbackRemoved = new Command<int>(OnFeedbackRemoved);
            AddFeedbackCommand = new Command(OnAddFeedback);
        }

        public int TaskId
        {
            get => taskId;
            set => taskId = value;
        }

        //public DateTime Date
        //{
        //    get => date;
        //    set => date = value;
        //}

        //public async Task LoadFeedbackByDate(int taskId, DateTime date)
        //{
        //    Date = date;
        //    IsBusy = true;

        //    var teamMember = await TeamMemberService.GetId(taskId, App.EmployeeId);
        //    var feedback = await FeedbackService.Get(teamMember.Id, date, date);
        //    App.TeamMemberId = teamMember.Id;

        //    FillInFeedback(feedback);
        //}

        private async Task ExecuteLoadFeedbackCommand()
        {
            IsBusy = true;

            try
            {
                //if (Date != null)
                //{
                //    await LoadFeedbackByDate(TaskId, Date);
                //    return;
                //}

                var teamMember = await TeamMemberService.GetId(TaskId, App.EmployeeId);
                var feedback = await FeedbackService.Get(teamMember.Id);
                App.TeamMemberId = teamMember.Id;

                FillInFeedback(feedback);
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
            SelectedFeedback = null;
        }

        public Feedback SelectedFeedback
        {
            get => selectedFeedback;
            set
            {
                SetProperty(ref selectedFeedback, value);
                OnFeedbackSelected(value);
            }
        }

        private async void OnAddFeedback(object obj)
        {
            await Shell.Current.GoToAsync($"{nameof(NewFeedbackPage)}?{nameof(NewFeedbackViewModel.TaskId)}={TaskId}");
        }

        private async void OnFeedbackSelected(Feedback feedback)
        {
            if (feedback == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(FeedbackDetailPage)}?{nameof(FeedbackDetailViewModel.FeedbackId)}={feedback.Id}");
        }

        private async void OnFeedbackRemoved(int id)
        {
            await FeedbackService.DeleteAsync(id);
            //await ExecuteLoadFeedbackCommand();
            IsBusy = true;
        }

        private void FillInFeedback(IEnumerable<Feedback> feedback)
        {
            Feedback.Clear();
            foreach (var f in feedback)
            {
                const string badMood = "Поганий";
                const string soSoMood = "Такий собі";
                const string goodMood = "Гарний";
                const string greatMood = "Чудовий";

                switch (f.Mood)
                {
                    case "Bad":
                        f.MoodIconPath = "bad_mood.png";
                        break;
                    case badMood:
                        f.MoodIconPath = "bad_mood.png";
                        break;

                    case "So-so":
                        f.MoodIconPath = "soso_mood.png";
                        break;
                    case soSoMood:
                        f.MoodIconPath = "soso_mood.png";
                        break;

                    case "Good":
                        f.MoodIconPath = "good_mood.png";
                        break;
                    case goodMood:
                        f.MoodIconPath = "good_mood.png";
                        break;

                    case "Great":
                        f.MoodIconPath = "great_mood.png";
                        break;
                    case greatMood:
                        f.MoodIconPath = "great_mood.png";
                        break;
                }

                Feedback.Add(f);
            }
        }
    }
}
