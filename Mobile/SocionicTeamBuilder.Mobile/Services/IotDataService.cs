using SocionicTeamBuilder.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SocionicTeamBuilder.Mobile.Services
{
    public class IotDataService
    {
        private static string Url { get => "api/iotmeasurement/"; }
        private static HttpClient HttpClient { get => App.HttpClient; }
        private static JsonSerializerOptions Options { get => App.Options; }

        public static async Task<IEnumerable<IotData>> Get(int teamMemberId)
        {
            string result = await HttpClient.GetStringAsync(Url + teamMemberId + "/");
            return JsonSerializer.Deserialize<IEnumerable<IotData>>(result, Options);
        }

        public static async Task<IEnumerable<IotData>> Get(int teamMemberId, DateTime startDate, DateTime endDate)
        {
            var url = Url + teamMemberId + "/start/" + startDate.ToString("yyyy-MM-dd") + "/end/" + endDate.ToString("yyyy-MM-dd") + "/";
            string result = await HttpClient.GetStringAsync(url);
            return JsonSerializer.Deserialize<IEnumerable<IotData>>(result, Options);
        }
    }
}
