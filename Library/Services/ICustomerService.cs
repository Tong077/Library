using Library.Models;

namespace Library.Services
{
    public interface ICustomerService
    {
        bool Create (Customer customer);
        bool Update (Customer customer);
        bool Delete (Customer customer);
        IEnumerable<Customer> GetAll ();
        Customer Get(int CustomerId);
    }
}
