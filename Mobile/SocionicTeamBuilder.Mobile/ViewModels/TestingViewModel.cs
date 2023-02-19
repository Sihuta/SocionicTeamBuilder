using SocionicTeamBuilder.Mobile.Models;
using SocionicTeamBuilder.Mobile.Services;
using SocionicTeamBuilder.Mobile.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Task = System.Threading.Tasks.Task;

namespace SocionicTeamBuilder.Mobile.ViewModels
{
    public class TestingViewModel: BaseViewModel
    {
        private Testing selectedItem;

        public TestingViewModel()
        {
            Items = new ObservableCollection<Testing>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ItemTapped = new Command<Testing>(OnItemSelected);
        }

        public ObservableCollection<Testing> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command<Testing> ItemTapped { get; }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await TestingService.Get();
                foreach (var item in items)
                {
                    Items.Add(item);
                }
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
            SelectedItem = null;
        }

        public Testing SelectedItem
        {
            get => selectedItem;
            set
            {
                SetProperty(ref selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnItemSelected(Testing item)
        {
            if (item == null)
                return;

            await Shell.Current
                .GoToAsync($"{nameof(TestPassingPage)}?{nameof(TestPassingViewModel.TestingId)}={item.Id}");
        }
    }
}
