using System.Collections.Generic;
using System.Threading.Tasks;

namespace web_app.Models
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<bool> AssignPrivilegeAsync(int userId, int privilegeId);
        Task<bool> RemovePrivilegeAsync(int userId, int privilegeId);
    }
}