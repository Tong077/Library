using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.Models
{
    public class BorrowCreateViewModel
    {

        public SelectList Customers { get; set; }
        public SelectList Librarians { get; set; }
        public List<SelectListItem> AvailableBooks { get; set; }
        public Borrow Borrow { get; set; }
        public BorrowDetail BorrowDetail { get; set; }

        public List<int> SelectedBookIds { get; set; }
      
      

        public BorrowCreateViewModel()
        {
            Borrow = new Borrow();
            SelectedBookIds = new List<int>();
        }
    }
}
