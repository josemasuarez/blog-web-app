using System.Collections.Generic;
using System.Threading.Tasks;
using web_app.Models;

namespace web_app.Models
{
    public interface IPrivilegeService
    {
        Task<IEnumerable<Privilege>> GetPrivilegesAsync();
    }
}
