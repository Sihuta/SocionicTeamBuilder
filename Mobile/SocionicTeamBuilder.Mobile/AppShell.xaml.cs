using SocionicTeamBuilder.Mobile.Models;
using SocionicTeamBuilder.Mobile.ViewModels;
using SocionicTeamBuilder.Mobile.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace SocionicTeamBuilder.Mobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
            Routing.RegisterRoute(nameof(SocionicTypePage), typeof(SocionicTypePage));
            Routing.RegisterRoute(nameof(TestingPage), typeof(TestingPage));
            Routing.RegisterRoute(nameof(TestPassingPage), typeof(TestPassingPage));

            Routing.RegisterRoute(nameof(EmployeesPage), typeof(EmployeesPage));
            Routing.RegisterRoute(nameof(EmployeeDetailPage), typeof(EmployeeDetailPage));
            Routing.RegisterRoute(nameof(NewEmployeePage), typeof(NewEmployeePage));

            Routing.RegisterRoute(nameof(TasksPage), typeof(TasksPage));
            Routing.RegisterRoute(nameof(TeamPage), typeof(TeamPage));
            Routing.RegisterRoute(nameof(IotDataPage), typeof(IotDataPage));

            Routing.RegisterRoute(nameof(FeedbackPage), typeof(FeedbackPage));
            Routing.RegisterRoute(nameof(FeedbackDetailPage), typeof(FeedbackDetailPage));
            Routing.RegisterRoute(nameof(NewFeedbackPage), typeof(NewFeedbackPage));

            Routing.RegisterRoute(nameof(BlacklistPage), typeof(BlacklistPage));
            Routing.RegisterRoute(nameof(NewEnemyPage), typeof(NewEnemyPage));


        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Current.GoToAsync("//LoginPage");
        }
    }
}
