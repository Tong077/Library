namespace Library.Models
{
    public class BorrowandBorrowDetail
    {
        public Borrow Borrow { get; set; }
        public BorrowDetail BorrowDetail { get; set; }
        public IEnumerable<Borrow> Borrows { get; set; }
        public IEnumerable<BorrowDetail> BorrowDetails { get; set;}
        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<Catalog> Catalog { get; set; }

        public List<int> BookId { get; set; }
        public int BorrowId { get; set; }
        public List<int> SelectedBookCodes { get; set; }
        // BorrowDetail properties
        public int BorrowDetailId { get; set; }
       
    }
}
