using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class Catalog
    {
        public int CatalogId { get; set; }
        public bool IsHidden { get; set; }
        public string? CatalogCode {  get; set; }

        [Required(ErrorMessage ="Please Enter Catalog Name")]
        public string? CatalogName { get; set;}
        public string? Isbn { get; set; }

        [Required(ErrorMessage ="AuthorName Is Required ...!")]
        public string? AuthorName { get; set; }
        public string? PubliSher { get; set; }
        public string? PublishYear { get; set; }
        public string? PublisheDition { get; set; }

        public ICollection<Book>? Book {  get; set; }
    }
}
