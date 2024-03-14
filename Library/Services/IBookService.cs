using Library.Models;

namespace Library.Services
{
    public interface IBookService
    {
        bool Create(Book book);
        bool Update(Book book);
        bool Delete(Book book);
        IEnumerable<Book> GetAll();
        Book Get(int BookId);
    }
}
