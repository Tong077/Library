using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    public class Borrow
    {
        public int BorrowId { get; set; }
        public bool IsHidden { get; set; }
       
        public int CustomerId { get; set; }
        
        public int LibrarianId { get; set; }
        public DateTime BorrowDate { get; set; }
        public string? BorrowCode { get; set; }
        public decimal Despositamount { get; set; }
        public DateTime DueDate { get; set; }
        public decimal FineAmount { get; set; }
        public string? Memo {  get; set; }
    }
}
