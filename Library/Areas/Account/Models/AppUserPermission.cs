using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Areas.Account.Models
{
    public class AppUserPermission
    {
        public int AppUserPermissionId { get; set; }

        [ForeignKey(nameof(AppUser))]
        public int AppUserId { get; set; }
        public string? UserPermission { get; set; }

        public AppUser? AppUser { get; set; }
    }
}
