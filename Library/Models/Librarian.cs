using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class Librarian
    {
        public int LibrarianId { get; set; }
        public bool IsHidden { get; set; }
        public string? LibrarainCode { get; set; }
        [Required(ErrorMessage ="Please Enter Your Name")]
        public string? LibrarianName { get; set; }
        public string? Sex { get; set; }

        [BindProperty(SupportsGet = true), DataType(DataType.Date)]
        public DateTime Dob {  get; set; }
        public string? Pob { get; set; }

        [Required(ErrorMessage ="Phone Number Is Required")]
        public string? Phone {  get; set; }

        public ICollection<Borrow>? Borrow { get; set; }
    }
}
