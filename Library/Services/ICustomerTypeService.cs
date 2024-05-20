using Library.Models;

namespace Library.Services
{
    public interface ICustomerTypeService
    {
        bool Create(CustomerType customerType);
        bool Update(CustomerType customerType);
        bool Delete(int customerTypeId);
        IEnumerable<CustomerType> GetAll();
        CustomerType Get(int CustomerTypeId);

       

    }
}
