using SocionicTeamBuilder.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SocionicTeamBuilder.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FeedbackPage : ContentPage
    {
        private readonly FeedbackViewModel viewModel;

        public FeedbackPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new FeedbackViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.OnAppearing();

            //if (viewModel.Feedback.Count != 0)
            //{
            //    datePicker.MinimumDate = viewModel.Feedback[viewModel.Feedback.Count - 1].DateTime.Date;
            //    datePicker.MaximumDate = viewModel.Feedback[0].DateTime.Date;
            //}
        }

        //private async void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
        //{
        //    if (viewModel.TaskId != 0)
        //    {
        //        await viewModel.LoadFeedbackByDate(viewModel.TaskId, e.NewDate);
        //    }
        //}
    }
}