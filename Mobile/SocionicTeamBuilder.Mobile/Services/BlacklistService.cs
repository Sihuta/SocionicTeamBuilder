using SocionicTeamBuilder.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SocionicTeamBuilder.Mobile.Services
{
    public class BlacklistService
    {
        private static string Url { get => "api/blacklist/"; }
        private static HttpClient HttpClient { get => App.HttpClient; }
        private static JsonSerializerOptions Options { get => App.Options; }

        public static async Task<Blacklist> Get(int employeeId)
        {
            string result = await HttpClient.GetStringAsync(Url + employeeId + "/");
            return JsonSerializer.Deserialize<Blacklist>(result, Options);
        }

        public static async Task<bool> CreateAsync(Blacklist blacklist)
        {
            var response = await HttpClient.PostAsync(Url, App.GetHttpContent(blacklist));
            return response.IsSuccessStatusCode;
        }

        public static async Task<bool> DeleteAsync(int employeeId, int enemyId)
        {
            var response = await HttpClient.DeleteAsync(Url + employeeId + "/enemy/" + enemyId + "/");
            return response.IsSuccessStatusCode;
        }
    }
}
