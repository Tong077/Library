namespace Library.Models
{
    public class Librarian
    {
        public int LibrarianId { get; set; }
        public bool IsHiDden { get; set; }
        public string? LibrarainCode { get; set; }
        public string? LibrarainName { get; set; }
        public string? Sex { get; set; }
        public DateTime Dob {  get; set; }
        public string? Pob { get; set; }
        public string? Phone {  get; set; }
    }
}
