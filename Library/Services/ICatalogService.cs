using Library.Models;

namespace Library.Services
{
    public interface ICatalogService
    {
        bool Create(Catalog catalog);
        bool Update(Catalog catalog);
        bool Delete(Catalog catalog);
        IEnumerable<Catalog> GetAll();
        Catalog Get(int CatalogId);
    }
}
