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
    public partial class TestPassingPage : ContentPage
    {
        TestPassingViewModel viewModel;
        public TestPassingPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new TestPassingViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.OnAppearing();
        }
    }
}