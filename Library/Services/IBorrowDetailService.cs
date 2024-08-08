using Library.Models;

namespace Library.Services
{
    public interface IBorrowDetailService
    {
        //bool Create(Borrow borrow, BorrowDetail borrowDetail);
        bool Create(Borrow borrow,BorrowDetail borrowDetail);

        bool Updade(Borrow borrow, BorrowDetail borrowDetail);
        bool Delete(int BorrowId, int BorrowDetailId);

        IEnumerable<BorrowDetail> GetAll(string searchTerm);

        BorrowandBorrowDetail Get(int BorrowId, int BorrowDetailId);
        BorrowDetail GetById(int BorrowDetailId, bool IsReturn);
        bool IsBorrowedAndNotReturned(int BookId);



        //bool CreateBorrow(Borrow borrow);
        //bool CreateBorrowDetail(BorrowDetail borrowDetail);

    }
}
