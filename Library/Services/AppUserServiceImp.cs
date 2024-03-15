using Dapper;
using Library.Data;
using Library.Models;

namespace Library.Services
{
    public class AppUserServiceImp :IAppUserService
    {
        private readonly DapperDbConnext _service;
        public AppUserServiceImp(DapperDbConnext service)
        {
            _service = service;
        }

        public bool Create(AppUser appUser)
        {
            var sql = "INSERT INTO APPUSER(IsHiDden, UserName, Password) Values(@IsHiDden,@Username,@Password)";
            var roweEffect = _service.Connection.Execute(sql, new
            {
                IsHiDden = appUser.IsHiDden,
                Username = appUser.UserName,
                Password = appUser.Password,
            });
            return roweEffect > 0;
        }
        public bool Delete(AppUser appUser)
        {
            var sql = "DELETE FROM AppUser Where AppUserId=@AppUserId";
            var roweEffect = _service.Connection.Execute(sql, new {
               appUser,
            });
            return roweEffect > 0;
        }

        public AppUser Get(int AppuserId)
        {
            var sql = "SELECT * FROM APPUSER WHERE APPUSERID = @APPUSERID";
            var appUser = _service.Connection.QueryFirstOrDefault<AppUser>(sql, new { AppuserId });
            return appUser;
        }
        public IEnumerable<AppUser> GetAll()
        {
            var sql = "SELECT * FROM AppUser";
            var appusers = _service.Connection.Query<AppUser>(sql);
            return appusers;
        }

        public bool Update(AppUser appUser)
        {
            var sql = "UPDATE APPUSER SET IsHiDden=@IsHiDden,Username=@Username,Password=@Password Where AppUserId=@AppUserId";
            var roweEffect = _service.Connection.Execute(sql, appUser);
            return roweEffect > 0;
        }
    }
}
