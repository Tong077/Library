using Dapper;
using Library.Data;
using Library.Migrations;
using Library.Models;

namespace Library.Services
{
    public class AppUserpermissionServiceImp : IAppUserpermissionService
    {
        private readonly DapperDbConnext _service;
        public AppUserpermissionServiceImp(DapperDbConnext service)
        {
           this._service = service;
        }

        public bool Create(AppuserPermission appuserPermission)
        {
       
            var sql = "INSERT INTO AppUserPermission (AppUserId, UserPermission) Values(@AppUserId,@UserPermission)";

            var roweEffect = _service.Connection.Execute(sql, new {
               AppUserId = appuserPermission.AppUserId,
                UserPermission = appuserPermission.UserPermission,
            });
            return roweEffect > 0;
        }

        public bool Delete(int appuserPermissionId)
        {
            var sql = "DELETE FROM AppUserPermission WHERE AppUserpermissionId = @AppUserpermissionId";
            var roweEffect = _service.Connection.Execute(sql, new {@AppuserPermissionId = appuserPermissionId});
            
            return roweEffect > 0;
        }

        public IEnumerable<AppuserPermission> GetAll()
        {
            var sql = @"SELECT  
                                AppUserPermission.AppUserPermissionId,
                                AppUserPermission.UserPermission,
                                AppUserPermission.AppUserId,
                                AppUser.UserName
                            FROM
                                AppUserPermission
                            INNER JOIN
                                AppUser ON AppUserPermission.AppUserId = AppUser.AppUserId;";


            var appuserPermission = _service.Connection.Query<AppuserPermission, AppUser, AppuserPermission>(sql,
                (appuserPermission, appuser) =>
                {
                    appuserPermission.AppUser = appuser;
                    return appuserPermission;
                }, splitOn: "AppUserId");
                
            return appuserPermission;
        }

        public AppuserPermission GetById(int AppUserPermissionId)
        {
            var sql = "SELECT * FROM AppUserpermission Where AppUserpermissionId = @AppUserpermissionId";
            var appuserpermission = _service.Connection.QueryFirstOrDefault<AppuserPermission>(sql, new {AppUserPermissionId});
            return appuserpermission;
        }

        public bool Update(AppuserPermission appuserPermission)
        {
            var sql = "UPDATE APPUSERPERMISSION SET AppUserId=@AppUserId,UserPermission=@UserPermission Where AppUserpermissionId=@AppUserpermissionId";
            var roweEffect = _service.Connection.Execute(sql, appuserPermission);
            return roweEffect > 0;
        }

    }
}
