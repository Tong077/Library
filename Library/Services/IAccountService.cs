using Library.Models;

namespace Library.Services
{
    public interface IAccountService
    {
        Task<bool> GetUser(AppUser appUser);

        AppUser GetUsers(AppUser appUser);

        bool Create (AppUser appUser);
    }
}
