using SocionicTeamBuilder.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace SocionicTeamBuilder.Mobile.Services
{
    public class FeedbackService
    {
        private static string Url { get => "api/feedback/"; }
        private static HttpClient HttpClient { get => App.HttpClient; }
        private static JsonSerializerOptions Options { get => App.Options; }

        public static async Task<IEnumerable<Feedback>> Get(int teamMemberId)
        {
            string result = await HttpClient.GetStringAsync(Url + teamMemberId + "/");
            return JsonSerializer.Deserialize<IEnumerable<Feedback>>(result, Options);
        }

        public static async Task<Feedback> GetAsync(int id)
        {
            string result = await HttpClient.GetStringAsync(Url + "get/" + id + "/");
            return JsonSerializer.Deserialize<Feedback>(result, Options);
        }

        public static async Task<IEnumerable<Feedback>> Get(int teamMemberId, DateTime startDate, DateTime endDate)
        {
            var url = Url + teamMemberId + "/start/" + startDate.ToString("yyyy-MM-dd") + "/end/" + endDate.ToString("yyyy-MM-dd") + "/";
            string result = await HttpClient.GetStringAsync(url);
            return JsonSerializer.Deserialize<IEnumerable<Feedback>>(result, Options);
        }

        public static async Task<bool> Create(Feedback feedback)
        {
            var response = await HttpClient.PostAsync(Url, App.GetHttpContent(feedback));
            return response.StatusCode == HttpStatusCode.OK;
        }

        public static async Task<bool> DeleteAsync(int id)
        {
            var response = await HttpClient.DeleteAsync(Url + id + "/");
            return response.StatusCode == HttpStatusCode.OK;
        }
    }
}
