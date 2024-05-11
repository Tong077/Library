using Library.Models;

namespace Library.Services
{
    public interface IBorrowService
    {
        bool Create(Borrow borrow, BorrowDetail borrowDetail);

        bool Updade(Borrow borrow);
        bool Delete(int borrowId);
        IEnumerable<Borrow> GetAll();
        Borrow Get(int BorrowId);


        


    }
}
