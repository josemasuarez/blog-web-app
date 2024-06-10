using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace web_app.Models
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string?> AuthenticateUserAsync(string username, string password)
        {
            var authRequest = JsonConvert.SerializeObject(new { username = username, password = password });
            var content = new StringContent(authRequest, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("http://localhost:8080/authenticate", content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var authResult = JsonConvert.DeserializeObject<AuthResult>(responseContent);
                return authResult?.Jwt;
            }

            return null;
        }

        public async Task<bool> RegisterUserAsync(User user)
        {
            var userJson = JsonConvert.SerializeObject(new { username = user.Username, password = user.Password });
            var content = new StringContent(userJson, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("http://localhost:8080/register", content);

            return response.IsSuccessStatusCode;
        }
    }

    public class AuthResult
    {
        [JsonProperty("jwt")]
        public string Jwt { get; set; }

        [JsonProperty("refreshToken")]
        public string RefreshToken { get; set; }
    }
}
