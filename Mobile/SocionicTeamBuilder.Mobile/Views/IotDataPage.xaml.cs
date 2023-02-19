using Microcharts;
using SkiaSharp;
using SocionicTeamBuilder.Mobile.Models;
using SocionicTeamBuilder.Mobile.Services;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Entry = Microcharts.ChartEntry;
using Task = System.Threading.Tasks.Task;

namespace SocionicTeamBuilder.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(TaskId), nameof(TaskId))]
    public partial class IotDataPage : ContentPage
    {
        private int taskId;
        private TeamMember teamMember;
        private List<IotData> iotData;

        private readonly List<Entry> heartBeatEntries;
        private readonly List<Entry> temperatureEntries;
        private readonly List<Entry> pulseEntries;

        public IotDataPage()
        {
            InitializeComponent();

            heartBeatEntries = new List<Entry>();
            temperatureEntries = new List<Entry>();
            pulseEntries = new List<Entry>();
        }

        public int TaskId
        {
            get => taskId;
            set => taskId = value;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await LoadData();
            DrawCharts();
        }

        private async void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            await LoadData(e.NewDate);
            DrawCharts();
        }

        private async Task LoadData()
        {
            teamMember = await TeamMemberService.GetId(TaskId, App.EmployeeId);
            iotData = (List<IotData>)await IotDataService.Get(teamMember.Id);

            if (iotData.Count > 0)
            {
                datePicker.MinimumDate = iotData[0].DateTime.Date;
                datePicker.MaximumDate = iotData[iotData.Count - 1].DateTime.Date;
            }
        }

        private async Task LoadData(DateTime date)
        {
            teamMember = await TeamMemberService.GetId(TaskId, App.EmployeeId);
            iotData = (List<IotData>)await IotDataService.Get(teamMember.Id, date, date);
        }

        private void DrawCharts()
        {
            ClearEntries();

            foreach (var data in iotData)
            {
                heartBeatEntries.Add(GetEntry(data.HeartBeat.Value, "#FF1943", data.DateTime.ToString("HH:mm")));
                temperatureEntries.Add(GetEntry((float)data.BodyTemperature, "#00BFFF", data.DateTime.ToString("HH:mm")));
                pulseEntries.Add(GetEntry(data.Pulse, "#00CED1", data.DateTime.ToString("HH:mm")));
            }

            heartBeatChart.Chart = GetLineChart(heartBeatEntries);
            temperatureChart.Chart = GetLineChart(temperatureEntries);
            pulseChart.Chart = GetLineChart(pulseEntries);
        }

        private void ClearEntries()
        {
            heartBeatEntries.Clear();
            temperatureEntries.Clear();
            pulseEntries.Clear();
        }

        private Entry GetEntry(float value, string hexString, string label)
        {
            return new Entry(value)
            {
                Color = SKColor.Parse(hexString),
                Label = label,
                ValueLabel = value.ToString()
            };
        }

        private LineChart GetLineChart(IEnumerable<Entry> entries)
        {
            return new LineChart()
            {
                Entries = entries,

                LineSize = 5,
                LineMode = LineMode.Straight,

                LabelTextSize = 40,
                ValueLabelOrientation = Orientation.Horizontal,
                PointMode = PointMode.Circle,
                PointSize = 18,
                Margin = 25
            };
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            string title = "IoT ID";
            string msg = TranslateExtension.GetValue("IotIdMsg");

            await Application.Current.MainPage.DisplayAlert(title, msg + teamMember.Id, "OK");
        }
    }
}