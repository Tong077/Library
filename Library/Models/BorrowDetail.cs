using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    public class BorrowDetail
    {
        public int BorrowDetailId { get; set; }

        [ForeignKey(nameof(Borrow))]
        public int? BorrowId { get; set; }

        [ForeignKey(nameof(Book))]
        public int BookId { get; set; }

        public string? Note { get; set; }

        public bool IsReturn { get; set; }

        [BindProperty(SupportsGet = true), DataType(DataType.Date)]
        public DateTime? ReturnDate { get; set; }


        public Book? Book {  get; set; }

        public Borrow? Borrow { get; set; } 

       

    }
}
