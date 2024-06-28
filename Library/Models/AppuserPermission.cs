using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    public class AppuserPermission
    {
        public int AppUserPermissionId { get; set; }

        [ForeignKey(nameof(AppUser))]
        public int AppUserId { get; set; }
        public string? UserPermission { get; set; }

        public AppUser? AppUser { get; set; }
    }
}
