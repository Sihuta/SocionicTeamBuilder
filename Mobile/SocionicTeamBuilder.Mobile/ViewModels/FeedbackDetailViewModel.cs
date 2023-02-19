using SocionicTeamBuilder.Mobile.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace SocionicTeamBuilder.Mobile.ViewModels
{
    [QueryProperty(nameof(FeedbackId), nameof(FeedbackId))]
    public class FeedbackDetailViewModel : BaseViewModel
    {
        private int feedbackId;

        private string mood;
        private DateTime dateTime;
        private string details;

        public string Mood
        {
            get => mood;
            set => SetProperty(ref mood, value);
        }

        public DateTime DateTime
        {
            get => dateTime;
            set => SetProperty(ref dateTime, value);
        }

        public string Details
        {
            get => details;
            set => SetProperty(ref details, value);
        }

        public int FeedbackId
        {
            get
            {
                return feedbackId;
            }
            set
            {
                feedbackId = value;
                LoadFeedback(value);
            }
        }

        public async void LoadFeedback(int id)
        {
            try
            {
                var feedback = await FeedbackService.GetAsync(id);

                Mood = feedback.Mood;
                DateTime = feedback.DateTime;
                Details = (feedback.Details == null || feedback.Details.Length == 0) ? "---" : feedback.Details;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Feedback");
            }
        }
    }
}
