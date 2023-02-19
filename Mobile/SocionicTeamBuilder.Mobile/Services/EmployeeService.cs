using SocionicTeamBuilder.Mobile.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SocionicTeamBuilder.Mobile.Services
{
    public class EmployeeService
    {
        private static string Url { get => "api/employee/"; }
        private static HttpClient HttpClient { get => App.HttpClient; }
        private static JsonSerializerOptions Options { get => App.Options; }

        public static async Task<Employee> Get(int id)
        {
            string result = await HttpClient.GetStringAsync(Url + "get/" + id + "/");
            return JsonSerializer.Deserialize<Employee>(result, Options);
        }

        public static async Task<Employee> GetByUserId(int userId)
        {
            string result = await HttpClient.GetStringAsync(Url + "user/" + userId + "/");
            return JsonSerializer.Deserialize<Employee>(result, Options);
        }

        public static async Task<IEnumerable<Employee>> GetByEnterprise(string enterprise)
        {
            string result = await HttpClient.GetStringAsync(Url + enterprise + "/");
            return JsonSerializer.Deserialize<IEnumerable<Employee>>(result, Options);
        }

        public static async Task<Employee> Create(Employee employee)
        {
            var response = await HttpClient.PostAsync(Url, GetHttpContent(employee));

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonSerializer.Deserialize<Employee>(
                await response.Content.ReadAsStringAsync(), Options);
        }

        public static async Task<bool> Update(Employee employee)
        {
            var response = await HttpClient.PutAsync(Url, GetHttpContent(employee));
            if (response.StatusCode != HttpStatusCode.OK)
                return false;

            return true;
        }

        public static async Task<SocionicType> GetSocionicType(int employeeId)
        {
            string result = await HttpClient.GetStringAsync(Url + employeeId + "/socionictype/");
            return JsonSerializer.Deserialize<SocionicType>(result, Options);
        }

        private static HttpContent GetHttpContent(object obj)
        {
            return App.GetHttpContent(obj);
        }
    }
}
