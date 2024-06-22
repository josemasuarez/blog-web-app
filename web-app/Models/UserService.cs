using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace web_app.Models
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<User>>("users");
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<User>($"users/{id}");
        }

        public async Task<bool> AssignPrivilegeAsync(int userId, int privilegeId)
        {
            var response = await _httpClient.PostAsJsonAsync<object>($"users/{userId}/privileges/{privilegeId}", null);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RemovePrivilegeAsync(int userId, int privilegeId)
        {
            var response = await _httpClient.DeleteAsync($"users/{userId}/privileges/{privilegeId}");
            return response.IsSuccessStatusCode;
        }
    }
}
