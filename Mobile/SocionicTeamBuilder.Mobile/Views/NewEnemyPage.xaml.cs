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
    public partial class NewEnemyPage : ContentPage
    {
        private readonly NewEnemyViewModel viewModel;

        public NewEnemyPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new NewEnemyViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.OnAppearing();
        }

        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var checkbox = (CheckBox)sender;
            int id = Convert.ToInt32(checkbox.AutomationId);

            if (e.Value)
            {
                ++viewModel.EnemiesCount;
                viewModel.Enemies.Add(id);
            }
            else
            {
                --viewModel.EnemiesCount;
                viewModel.Enemies.Remove(id);
            }
        }
    }
}