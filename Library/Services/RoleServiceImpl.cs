using Dapper;
using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Data.SqlClient;

namespace Library.Services
{
    public class RoleServiceImpl : IRoleService
    {
        private readonly DapperDbConnext _service;

        public RoleServiceImpl(DapperDbConnext service)
        {
            _service = service;
        }

        public bool CreateRole(Role role)
        {
            var sql = "INSERT INTO Role(RoleName,AppUserId) VALUES(@RoleName,@AppUserId);";
            var rowEffect = _service.Connection.Execute(sql, new
            {
                RoleName = role.RoleName,
                AppUserId = role.AppUserId,
            });
            return rowEffect > 0;
        }

        public bool DeleteRole(int RoleId)
        {
            var sql = "DELETE FROM Role Where RoleId=@RoleId";
            var rowEffect = _service.Connection.Execute(sql, new { RoleId = RoleId });
            return rowEffect > 0;
        }

        public Role GetRole(int RoleId)
        {
            var sql = "SELECT * FROM Role Where RoleId=@RoleId";
            var role = _service.Connection.QueryFirstOrDefault<Role>(sql, new {RoleId});
            return role!;
        }

        public IEnumerable<Role> GetAllRoles()
        {
            var sql = @"SELECT 
                            Role.RoleId, 
                            Role.RoleName, 
                            Role.AppUserId, 
                            AppUser.UserName
                       FROM
                            Role
                        INNER JOIN 
                            AppUser 
                        ON 
                            Role.AppUserId = AppUser.AppUserId";

            var rols = _service.Connection.Query<Role, AppUser, Role>(sql,
                (role, appUser) =>
                {
                    role.AppUser = appUser;
                    return role;
                }, splitOn: "AppUserId");    
            return rols;
        }

        public bool UpdateRole(Role role)
        {
            var sql = "UPDATE Role SET AppUserId=@AppUserId,RoleName=@RoleName Where RoleId=@RoleId";
            var rowEffect = _service.Connection.Execute(sql,  role );
            return rowEffect > 0;
        }

        public IEnumerable<string> GetUserRol(string UserName)
        {
            var sql = @"
                 SELECT 
                     r.RoleName
                 FROM
                     Role r
                 INNER JOIN 
                     AppUser u 
                 ON 
                     r.AppUserId = u.AppUserId
                 WHERE
                     u.UserName = @UserName";

            return _service.Connection.Query<string>(sql, new { UserName });
        }

        
    }
}
