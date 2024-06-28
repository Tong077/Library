using Library.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.Services
{
    public interface IRoleService
    {
        IEnumerable<Role> GetAllRoles();
        bool CreateRole(Role role);
        bool DeleteRole(int RoleId);
        bool UpdateRole(Role role);
        Role GetRole(int RoleId);

        IEnumerable<string> GetUserRol(string UserName);




        
    }
}
