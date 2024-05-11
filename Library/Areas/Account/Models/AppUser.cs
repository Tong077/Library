namespace Library.Areas.Account.Models
{
    public class AppUser
    {
        public int AppUserId { get; set; }
        public bool IsHiDden { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public AppUserPermission? AppuserPermission { get; set; }
    }
}
