using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class BorrowDetailView
    {
        public int BorrowDetailId { get; set; }
        public int BorrowId { get; set; }
        public int BookId { get; set; }
        public string Note { get; set; }
        public bool IsReturn { get; set; }
        [BindProperty(SupportsGet = true), DataType(DataType.Date)]
        public DateTime? ReturnDate { get; set; }
        public string BookCode { get; set; }
        public string BookDescription { get; set; }
        public string CatalogName { get; set; }

        public string CustomerName { get; set; }
        public string LibrarianName { get; set; }

        public bool IsHidden { get; set; }

        [BindProperty(SupportsGet = true), DataType(DataType.Date)]
        public DateTime? BorrowDate { get; set; }
        public string? BorrowCode { get; set; }

        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal? Depositamount { get; set; }

        [BindProperty(SupportsGet = true), DataType(DataType.Date)]
        public DateTime? Duedate { get; set; }

        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal? FineAmount { get; set; }
        public string? Memo { get; set; }
    }
}
