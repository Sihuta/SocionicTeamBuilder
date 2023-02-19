using SocionicTeamBuilder.Mobile.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace SocionicTeamBuilder.Mobile.Views
{
    public partial class EmployeeDetailPage : ContentPage
    {
        public EmployeeDetailPage()
        {
            InitializeComponent();
            BindingContext = new EmployeeDetailViewModel();
        }
    }
}