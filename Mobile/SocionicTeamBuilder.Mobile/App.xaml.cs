using SocionicTeamBuilder.Mobile.Models;
using SocionicTeamBuilder.Mobile.Services;
using SocionicTeamBuilder.Mobile.Views;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Xamarin.Android.Net;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace SocionicTeamBuilder.Mobile
{
    public partial class App : Application
    {
        public static string BaseWebApiUrl { get => "https://192.168.31.244:45455/"; }
        public static HttpClient HttpClient { get; private set; }
        public static JsonSerializerOptions Options
        {
            get => new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
        }

        public static string UserPassword { get; set; }
        public static User User { get; set; }

        public static int EmployeeId { get; set; }
        public static int TeamMemberId { get; set; }

        public static bool IsTeamCreator { get => User.Role == "teamCreator"; }
        public static bool IsEmployee { get => User.Role == "employee"; }

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            DependencyService.Register<Localize>();

            //Locale = "en-US";
            MainPage = new LoginPage();
            HttpClient = GetClient();
        }

        public static void AddAuthToken(string token)
        {
            HttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }

        public static HttpContent GetHttpContent(object obj)
        {
            return new StringContent(
                    JsonSerializer.Serialize(obj),
                    Encoding.UTF8, "application/json");
        }

        private HttpClient GetClient()
        {
            //var androidClientHandler = new AndroidClientHandler();
            //var httpClient = new HttpClient(androidClientHandler);
            //httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            //httpClient.Timeout = TimeSpan.FromSeconds(60);
            //return httpClient;

            //var httpClientHandler = new HttpClientHandler();
            //var httpClient = new HttpClient(httpClientHandler);
            //httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            //httpClient.Timeout = TimeSpan.FromSeconds(60);
            //return httpClient;

            //HttpClient client = new HttpClient
            //{
            //    Timeout = TimeSpan.FromSeconds(60)
            //};
            //client.DefaultRequestHeaders.Add("Accept", "application/json");
            //return client;

            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

            var httpClient = new HttpClient(httpClientHandler);
            var uri = new Uri(BaseWebApiUrl);

            httpClient.BaseAddress = uri;
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            return httpClient;

            //var clientService = DependencyService.Get<IHttpClientService>();
            //HttpClient httpClient = clientService.Client;
            //httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            //return httpClient;

            //var httpClient = new HttpClient();
            //switch (Device.RuntimePlatform)
            //{
            //    case Device.Android:
            //        httpClient = new HttpClient(DependencyService.Get<IHttpClientHandlerCreationService>().GetInsecureHandler());
            //        break;
            //    default:
            //        var httpClientHandler = new HttpClientHandler();
            //        httpClientHandler.ServerCertificateCustomValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

            //        httpClient = new HttpClient(httpClientHandler);
            //        break;
            //}
            //httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            //return httpClient;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
