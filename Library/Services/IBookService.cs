using Library.Models;

namespace Library.Services
{
    public interface IBookService
    {
        bool Create(Book book);
        bool Update(Book book);
        bool Delete(int bookId);
        IEnumerable<Book> GetAll();
        Book Get(int BookId);

       bool IsBorrowed(int BookId);
    }
}
