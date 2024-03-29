using Dapper;
using Library.Data;
using Library.Models;

namespace Library.Services
{
    public class CustomerServiceImp : ICustomerService
    {
        private DapperDbConnext _service;
        public CustomerServiceImp(DapperDbConnext service)
        {
            this._service = service;
        }
        public bool Create(Customer customer)
        {
            var sql = "INSERT INTO Customer (IsHidden,CustomerCode,CustomerTypeId,CustomerName,Sex,Dob,Pob,Phone,Address) Values(@IsHiDdden,@CustomerCode,@CustomerTypeId,@CustomerName,@Sex,@Dob,@Pob,@Phone,@Address)";
            var roweEffect = _service.Connection.Execute(sql, new  {
                IsHidden = customer.IsHidden,
				CustomerCode = customer.CustomerCode,
                CustomerTypeId = customer.CustomerTypeId,
				CustomerName = customer.CustomerName,
                Sex = customer.Sex,
				Dob = customer.Dob,
                Pob = customer.Pob,
                Phone = customer.Phone,
                Address = customer.Address,

            });
            return roweEffect > 0;
        }

        public bool Delete(int customerId)
        {
            var sql = "DELETE FROM Customer WHERE CustomerCode = @CustomerCode";
            var roweEffect = _service.Connection.Execute(sql, new { @CustomerId = customerId});
            return roweEffect > 0;
            
        }

        public Customer Get(int CustomerId)
        {
            var sql = "SELECT * FROM Customer Where CustomerId=@CustomerId";
            var cusomter = _service.Connection.QueryFirstOrDefault<Customer>(sql, new {@CustomerId = CustomerId });
            return cusomter!;
        }

        public IEnumerable<Customer> GetAll()
        {
            var sql = "SELECT * FROM Customer";
            var type = "SELECT Customer.CustomerId, CustomerType.CustomerTypeName FROM Customer " +
                "INNER JOIN CustomerType ON Customer.CustomerTypeId = CustomerType.CustomerTypeId;";

			var customers = _service.Connection.Query<Customer>(sql);
            return customers;

        }

        public bool Update(Customer customer)
        {
            var sql = "UPDATE CUSTOMER SET IsHidden=@IsHidden,CustomerCode=@CustomerCode,CustomerTypeId=@CustomerTypeId,CustomerName=@CustomerName,Sex=@Sex,Dob=@Dob,Pob=@Pob,Phone=@Phone,Address=@Address Where CustomerId=@CustomerId";
            var roweEffect = _service.Connection.Execute(sql, customer);
            return roweEffect > 0;
        }
    }
}
