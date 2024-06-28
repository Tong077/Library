using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }

        [ForeignKey(nameof(AppUser))]
        public int AppUserId { get; set; }

        public AppUser? AppUser { get; set; }
    }
}
