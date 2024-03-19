using Library.Models;

namespace Library.Services
{
    public interface ICustomerService
    {
        bool Create (Customer customer);
        bool Update (Customer customer);
        bool Delete (int customerId);
        IEnumerable<Customer> GetAll ();
        Customer Get(int CustomerId);
    }
}
