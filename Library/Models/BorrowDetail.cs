using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    public class BorrowDetail
    {
        public int BorrowDetailId { get; set; }
        
        public int? BorrowId { get; set; }
       
        public int BookId { get; set; }

        public string? Note { get; set; }

        public bool IsReturn { get; set; }

        [BindProperty(SupportsGet = true), DataType(DataType.Date)]
        public DateTime ReturnDate { get; set; }

        public ICollection<Book>? Book {  get; set; }





    }
}
