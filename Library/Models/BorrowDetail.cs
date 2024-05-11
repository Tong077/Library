using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    public class BorrowDetail
    {
        public int BorrowDetailId { get; set; }
        
        public int? BorrowId { get; set; }
       
        public int? BookId { get; set; }

        public string? Note { get; set; }

        public bool IsReturn { get; set; }

        public DateTime ReturnDate { get; set; }

        public ICollection<Book>? Book {  get; set; }





    }
}
