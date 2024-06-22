using System.Collections.Generic;

namespace web_app.Models
{
    public class EditUserPrivilegesViewModel
    {
        public User User { get; set; }
        public List<Privilege> AllPrivileges { get; set; }
        public List<int> AssignedPrivileges { get; set; }
    }
}