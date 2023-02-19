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
    public partial class NewFeedbackPage : ContentPage
    {
        private readonly NewFeedbackViewModel viewModel;

        public NewFeedbackPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new NewFeedbackViewModel();
        }

        private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            viewModel.Mood = rb.Content.ToString();
        }
    }
}