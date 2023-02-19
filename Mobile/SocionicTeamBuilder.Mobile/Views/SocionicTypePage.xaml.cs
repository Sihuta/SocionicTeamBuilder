using SocionicTeamBuilder.Mobile.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SocionicTeamBuilder.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SocionicTypePage : ContentPage
    {
        SocionicTypeViewModel viewModel;

        public SocionicTypePage()
        {
            InitializeComponent();
            BindingContext = viewModel = new SocionicTypeViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.OnAppearing();
        }
    }
}