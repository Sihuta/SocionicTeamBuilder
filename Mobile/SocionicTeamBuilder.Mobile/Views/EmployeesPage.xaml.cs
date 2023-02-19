using SocionicTeamBuilder.Mobile.Models;
using SocionicTeamBuilder.Mobile.ViewModels;
using SocionicTeamBuilder.Mobile.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SocionicTeamBuilder.Mobile.Views
{
    public partial class EmployeesPage : ContentPage
    {
        private readonly EmployeesViewModel viewModel;

        public EmployeesPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new EmployeesViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.OnAppearing();
        }
    }
}