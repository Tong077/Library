using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    public class Borrow
    {
        public int BorrowId { get; set; }

        public bool IsHidden { get; set; }
       
        public int CustomerId { get; set; }
        
        public int LibrarianId { get; set; }

        public DateTime? BorrowDate { get; set; }
        public string? BorrowCode { get; set; }
        public decimal? Depositamount { get; set; }
        public DateTime? Duedate { get; set; }
        public decimal? FineAmount { get; set; }
        public string? Memo {  get; set; }

        public Customer? Customer { get; set; }

        public Librarian? Librarian { get; set; }
    }
}
