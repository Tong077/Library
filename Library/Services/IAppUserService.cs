using Library.Models;

namespace Library.Services
{
    public interface IAppUserService
    {
        bool Create(AppUser appUser);

        bool Update(AppUser appUser);

        bool Delete(AppUser appUser);

        IEnumerable<AppUser> GetAll();

        AppUser Get(int AppuserId);
    }
}
