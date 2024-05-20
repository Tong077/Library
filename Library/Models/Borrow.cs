using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    public class Borrow
    {
        public int BorrowId { get; set; }

        public bool IsHidden { get; set; }
       
        public int CustomerId { get; set; }
        
        public int LibrarianId { get; set; }

        [BindProperty(SupportsGet = true), DataType(DataType.Date)]
        public DateTime? BorrowDate { get; set; }
        public string? BorrowCode { get; set; }
        public decimal? Depositamount { get; set; }

        [BindProperty(SupportsGet = true), DataType(DataType.Date)]
        public DateTime? Duedate { get; set; }
        public decimal? FineAmount { get; set; }
        public string? Memo {  get; set; }

        public Customer? Customer { get; set; }

        public Librarian? Librarian { get; set; }
    }
}
