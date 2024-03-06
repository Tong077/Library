using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    public class AppuserPermission
    {
        public int AppUserPermissionId { get; set; }
        public int AppUserId {  get; set; }
        public string? UserPermission { get; set; }
    }
}
