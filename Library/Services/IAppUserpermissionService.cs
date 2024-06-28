using Library.Models;

namespace Library.Services
{
    public interface IAppUserpermissionService
    {
        bool Create(AppuserPermission appuserPermission);
        bool Update(AppuserPermission appuserPermission);
        bool Delete(int appuserPermissionId);
        IEnumerable<AppuserPermission> GetAll();
        AppuserPermission GetById(int AppUserPermissionId);


       Task<IEnumerable<string>> userpermission(string UserName);
    }
}
