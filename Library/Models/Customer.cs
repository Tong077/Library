using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        public bool IsHidden { get; set; }

        public string? CustomerCode {  get; set; }
       
        public int CustomerTypeId { get; set; }

        [Required(ErrorMessage ="Please Enter Your Name")]
        public string? CustomerName { get; set; }

        public string? Sex { get; set; }

        [BindProperty(SupportsGet = true), DataType(DataType.Date)]
        public DateTime Dob { get; set; }

        public string? Pob {  get; set; }

        public string? Phone { get; set; }

        public string? Address { get; set; }

        public ICollection<Borrow>? Borrow { get; set; }

        public CustomerType? CustomerType { get; set; }

      
       
    }
}
