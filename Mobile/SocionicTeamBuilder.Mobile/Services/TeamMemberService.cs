using SocionicTeamBuilder.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SocionicTeamBuilder.Mobile.Services
{
    public class TeamMemberService
    {
        private static string Url { get => "api/teammember/"; }
        private static HttpClient HttpClient { get => App.HttpClient; }
        private static JsonSerializerOptions Options { get => App.Options; }

        public static async Task<TeamMember> GetId(int taskId, int employeeId)
        {
            string result = await HttpClient.GetStringAsync(Url + "task/" + taskId + "/employee/" + employeeId + "/");
            return JsonSerializer.Deserialize<TeamMember>(result, Options);
        }
    }
}
