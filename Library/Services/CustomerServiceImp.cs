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
            var sql = "INSERT INTO Customer (IsHidden,CustomerCode,CustomerTypeId,CustomerName,Sex,Date,Pob,Phone,Address) Values(@IsHiDden,@CustomerCode,@CustomerType,@CustomerName,@Sex,@Date,@Pob,@Phone,@Address)";
            var roweEffect = _service.Connection.Execute(sql, new  {
                IsHiDden = customer.IsHiDden,
                CustomserCode = customer.CustomerCode,
                CustomerName = customer.CustomerName,
                Sex = customer.Sex,
                Date = customer.Date,
                Pob = customer.Pob,
                Phone = customer.Phone,
                Address = customer.Address,

            });
            return roweEffect > 0;
        }

        public bool Delete(Customer customer)
        {
            var sql = "DELETE FROM Customer WHERE CustomerCode = @CustomerCode";
            var roweEffect = _service.Connection.Execute(sql, new { customer});
            return roweEffect > 0;
            
        }

        public Customer Get(int CustomerId)
        {
            var sql = "SELECT * FROM Customer Wher CustomerId=@CustomerId";
            var cusomter = _service.Connection.QueryFirstOrDefault<Customer>(sql, new {CustomerId = CustomerId });
            return cusomter;
        }

        public IEnumerable<Customer> GetAll()
        {
            var sql = "SELECT * FROM Customer ";
            var customers = _service.Connection.Query<Customer>(sql);
            return customers;

        }

        public bool Update(Customer customer)
        {
            var sql = "UPDATE Customer SET IsHiDden=@IsHiDden,CustomerCode=@CustomerCode,CustomerTypeId=@CustomerTypeId,CustomerName=@CustomerName,Sex=@Sex,Date=@Date,Pob=@Pob,Phone=@Phone,Address=@Address Where CustomerId=@CustomerId";
            var roweEffect = _service.Connection.Execute(sql,new { customer });
            return roweEffect > 0;
        }
    }
}
