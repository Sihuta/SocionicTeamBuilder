using SocionicTeamBuilder.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SocionicTeamBuilder.Mobile.Services
{
    public class TaskService
    {
        private static string Url { get => "api/task/"; }
        private static HttpClient HttpClient { get => App.HttpClient; }
        private static JsonSerializerOptions Options { get => App.Options; }

        public static async Task<IEnumerable<Models.Task>> Get(int employeeId)
        {
            string result = await HttpClient.GetStringAsync(Url + "employee/" + employeeId + "/");
            return JsonSerializer.Deserialize<IEnumerable<Models.Task>>(result, Options);
        }

        public static async Task<Team> GetTeam(int taskId, int employeeId)
        {
            string result = await HttpClient.GetStringAsync(Url + taskId + "/teambuilder/teams/" + employeeId + "/");
            return JsonSerializer.Deserialize<Team>(result, Options);
        }
    }
}
