using Library.Models;

namespace Library.Services
{
    public interface IBorrowDetailService
    {
        bool Create (BorrowDetail borrowDetail);
        bool Update (BorrowDetail borrowDetail);
        bool Delete (BorrowDetail borrowDetail);
        IEnumerable<BorrowDetail> GetAll ();
        BorrowDetail Get (int BorrowDetailId);
    }
}
