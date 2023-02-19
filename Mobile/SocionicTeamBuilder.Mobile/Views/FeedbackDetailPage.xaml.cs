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
    public partial class FeedbackDetailPage : ContentPage
    {
        public FeedbackDetailPage()
        {
            InitializeComponent();
            BindingContext = new FeedbackDetailViewModel();
        }
    }
}