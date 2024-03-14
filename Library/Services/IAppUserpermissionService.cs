using Library.Models;

namespace Library.Services
{
    public interface IAppUserpermissionService
    {
        bool Create(AppuserPermission appuserPermission);
        bool Update(AppuserPermission appuserPermission);
        bool Delete(AppuserPermission appuserPermission);
        IEnumerable<AppuserPermission> GetAll();
        AppuserPermission GetById(int AppUserPermissionId);
    }
}
