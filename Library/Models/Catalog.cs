namespace Library.Models
{
    public class Catalog
    {
        public int CatalogId { get; set; }
        public bool IsHidden { get; set; }
        public string? CatalogCode {  get; set; }
        public string? CatalogName { get; set;}
        public string? Isbn { get; set; }
        public string? AuthorName { get; set; }
        public string? PubliSher { get; set; }
        public string? PublishYear { get; set; }
        public string? Publisheditin { get; set; }
    }
}
