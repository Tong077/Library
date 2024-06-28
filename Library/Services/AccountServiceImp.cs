using Dapper;
using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Identity;

namespace Library.Services
{
    public class AccountServiceImp : IAccountService
    {
        private readonly DapperDbConnext _dapper;
        //private readonly IHttpContextAccessor _context;
        public AccountServiceImp(DapperDbConnext dapper)
        {
            _dapper = dapper;
            
        }

        public bool Create(AppUser appUser)
        {
            var sql = "INSERT INTO AppUser (UserName,Password) Values(@UserName,@Password)";
            var rolEffect = _dapper.Connection.Execute(sql, new
            {
               UserName = appUser.UserName,
               Password = appUser.Password,
            });
            return rolEffect > 0;
        }

        public async Task<bool> GetUser(AppUser appUser)
        {
            string sql = "SELECT * FROM AppUser WHERE Username = @Username AND Password=@Password";
            return await _dapper.Connection.QueryFirstOrDefaultAsync(sql, new { UserName = appUser.UserName, Password = appUser.Password});
          
        }

       

        public AppUser GetUsers(AppUser appUser)
        {
            var user = "SELECT * FROM AppUser WHERE UserName=@UserName AND Password=@Password;";
            var result = _dapper.Connection.QueryFirstOrDefault<AppUser>(user, new
            {
                appUser.Password,
                appUser.UserName
            });
          
            return result!;
        }

       
    }
}
