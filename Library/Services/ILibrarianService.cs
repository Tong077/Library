using Library.Models;

namespace Library.Services
{
    public interface ILibrarianService
    {
        bool Create(Librarian librarian);
        bool Update(Librarian librarian);
        bool Delete(Librarian librarian); 
        IEnumerable<Librarian> GetAll();
        Librarian Get(int LibrarianId);
    }
}
