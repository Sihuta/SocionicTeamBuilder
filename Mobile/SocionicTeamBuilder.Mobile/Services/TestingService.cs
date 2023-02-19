using SocionicTeamBuilder.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SocionicTeamBuilder.Mobile.Services
{
    public class TestingService
    {
        private static string Url { get => "api/testing/"; }
        private static HttpClient HttpClient { get => App.HttpClient; }
        private static JsonSerializerOptions Options { get => App.Options; }

        public static async Task<IEnumerable<Testing>> Get()
        {
            string result = await HttpClient.GetStringAsync(Url);
            return JsonSerializer.Deserialize<IEnumerable<Testing>>(result, Options);
        }

        public static async Task<IEnumerable<Question>> GetQuestions(int testingId)
        {
            string result = await HttpClient.GetStringAsync(Url + testingId + "/question/");
            return JsonSerializer.Deserialize<IEnumerable<Question>>(result, Options);
        }

        public static async Task<IEnumerable<Answer>> GetAnswers(int testingId, int questionNumber)
        {
            string result = await HttpClient
                .GetStringAsync(Url + testingId + "/question/" + questionNumber + "/answer/");
            return JsonSerializer.Deserialize<IEnumerable<Answer>>(result, Options);
        }

        public static async Task<string> GetResult(int testingId, int employeeId, Dictionary<string, int> scores)
        {
            var response = await HttpClient.PostAsync(
                Url + testingId + "/" + employeeId + "/score/",
                GetHttpContent(scores)
            );

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonSerializer.Deserialize<string>(
                await response.Content.ReadAsStringAsync(), Options);
        }

        private static HttpContent GetHttpContent(object obj)
        {
            return App.GetHttpContent(obj);
        }
    }
}
