using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public bool IsHidden {  get; set; }


        
        [ForeignKey(nameof(Catalog))]
        [Required(ErrorMessage = "Catalog is required.....")]
        public int? CatalogId { get; set; }

        [Required]
        public string? BookCode { get; set; }
        public string? BookDescription { get; set; }


        public ICollection<BorrowDetail>? BorrowDetail { get; set; }
        public Catalog? Catalog { get; set; }
    }
}
