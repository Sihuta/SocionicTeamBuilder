using SocionicTeamBuilder.Mobile.Models;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace SocionicTeamBuilder.Mobile.Services
{
    public class UserService
    {
        private static HttpClient HttpClient { get => App.HttpClient; }
        private static JsonSerializerOptions Options { get => App.Options; }

        public static async Task<User> LogIn(string login, string password)
        {
            try
            {
                var url = "api/login/";
                var response = await HttpClient.PostAsync(url, App.GetHttpContent(login + " " + password));
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    return null;
                }

                await Auth(new AuthData() { Login = login, Password = password });

                return JsonSerializer.Deserialize<User>(
                    await response.Content.ReadAsStringAsync(), Options);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return null;
        }

        public static async Task Auth(AuthData authData)
        {
            var url = "api/token/";
            var response = await HttpClient.PostAsync(url, App.GetHttpContent(authData));

            if (response.StatusCode == HttpStatusCode.Created)
            {
                var token = JsonSerializer.Deserialize<Token>(
                    await response.Content.ReadAsStringAsync(), Options);

                App.AddAuthToken(token.AccessToken);
            }
        }

        public static async Task<User> Update(User user)
        {
            var url = "api/user/";
            var response = await HttpClient.PutAsync(url, App.GetHttpContent(user));

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonSerializer.Deserialize<User>(
                await response.Content.ReadAsStringAsync(), Options);
        }

        public static async Task<string> GetEnterprise(int userId)
        {
            var url = "api/enterprise/" + userId + "/";
            string result = await HttpClient.GetStringAsync(url);
            return JsonSerializer.Deserialize<string>(result, Options);
        }
    }
}
