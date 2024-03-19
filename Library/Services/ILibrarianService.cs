using Library.Models;

namespace Library.Services
{
    public interface ILibrarianService
    {
        bool Create(Librarian librarian);
        bool Update(Librarian librarian);
        bool Delete(int librarianId); 
        IEnumerable<Librarian> GetAll();
        Librarian Get(int LibrarianId);
    }
}
