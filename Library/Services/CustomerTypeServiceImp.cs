using Dapper;
using Library.Data;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
    public class CustomerTypeServiceImp : ICustomerTypeService
    {
        private readonly DapperDbConnext _service;
        
        public CustomerTypeServiceImp(DapperDbConnext service)
        {
            this._service = service;
          
        }
        public bool Create(CustomerType customerType)
        {
            var sql = "INSERT INTO CustomerType(CustomerTypeName) Values(@CustomerTypeName)";
            var roweEffect = _service.Connection.Execute(sql, new
            {
                CustomerTypeName = customerType.CustomerTypeName
            });
            return roweEffect > 0;
        }

        public bool Delete(int customerTypeId)
        {
            var sql = "DELETE FROM CustomerType WHERE CustomerTypeId = @CustomerTypeId";
            var roweEffect = _service.Connection.Execute(sql, new { @CustomerTypeId = customerTypeId });
            return roweEffect > 0;
        }

        public CustomerType Get(int customerTypeId)
        {
            var sql = "SELECT * FROM CustomerType WHERE CustomerTypeId = @CustomerTypeId";
            var customer = _service.Connection.QueryFirstOrDefault<CustomerType>(sql, new  {@CustomerTypeId = customerTypeId});
            return customer!;
        }

        public IEnumerable<CustomerType> GetAll()
        {
            var sql = "SELECT * FROM CustomerType";
            var customers = _service.Connection.Query<CustomerType>(sql);
            return customers;
        }

      

        public bool Update(CustomerType customerType)
        {
            var sql = "UPDATE CustomerType SET CustomerTypeName=@CustomerTypeName Where CustomerTypeId=@CustomerTypeId";
            var roweEffect = _service.Connection.Execute(sql,customerType);
            return roweEffect > 0;
        }


    }
}
