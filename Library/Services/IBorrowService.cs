using Library.Models;

namespace Library.Services
{
    public interface IBorrowService
    {
        bool Create(Borrow borrow, BorrowDetail borrowDetail);

        bool Updade(Borrow borrow, BorrowDetail borrowDetail);
        bool Delete(int BorrowId, int BorrowDetailId);

        IEnumerable<Borrow> GetAll();

        BorrowandBorrowDetail Get(int BorrowId, int BorrowDetailId);

    }
}
