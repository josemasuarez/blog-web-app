using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace web_app.Models
{
    public class PrivilegeService: IPrivilegeService
    {
        private readonly HttpClient _httpClient;

        public PrivilegeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Privilege>> GetPrivilegesAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Privilege>>("privileges");
        }
    }
}
