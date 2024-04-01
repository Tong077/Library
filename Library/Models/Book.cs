using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public bool IsHidden {  get; set; }
       
        public int? CatalogId { get; set; }
        public string? BookCode { get; set; }
        public string? BookDescription { get; set; }
    }
}
