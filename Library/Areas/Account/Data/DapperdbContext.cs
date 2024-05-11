using Microsoft.Data.SqlClient;
using System.Data;

namespace Library.Areas.Account.Data
{
    public class DapperdbContext
    {
        private readonly IConfiguration _conConfig;

        public DapperdbContext(IConfiguration conConfig)
        {
            _conConfig = conConfig;
        }
        public IDbConnection Connection =>new SqlConnection(_conConfig.GetConnectionString("MyConnection"));
    }
}
