using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class AppUser
    {
        public int AppUserId { get; set; }
        public bool IsHiDden { get; set; }

        [Required(ErrorMessage = "Please Enter UserName")]
        public string UserName { get; set; }


        [Required(ErrorMessage ="Please Enter Password")]
        public string Password { get; set; }
       
        public AppuserPermission? AppuserPermission { get; set; }
        public ICollection<Role>? Roles { get; set; }
    }
    

}
