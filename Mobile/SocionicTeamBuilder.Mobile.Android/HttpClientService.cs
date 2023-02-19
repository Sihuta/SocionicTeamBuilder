using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SocionicTeamBuilder.Mobile.Droid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Xamarin.Android.Net;

[assembly: Xamarin.Forms.Dependency(typeof(HttpClientService))]
namespace SocionicTeamBuilder.Mobile.Droid
{
    public class HttpClientService : IHttpClientService
    {
        public HttpClientService()
        {
            Client = new HttpClient(new HttpClientHandler() { UseCookies = true, AllowAutoRedirect = false, CookieContainer = Cookies }); ;
        }
        public CookieContainer Cookies { get; set; } = new CookieContainer();

        public HttpClient Client { get; }
    }
}