using Microsoft.Data.SqlClient;
using System.Data;

namespace Library.Data
{
    public class DapperDbConnext
    {
        private readonly IConfiguration _configuration;
        public DapperDbConnext (IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection Connection => new SqlConnection(_configuration.GetConnectionString("MyConnection"));
    }
}
