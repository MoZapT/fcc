using Dapper;
using Shared.Interfaces.Repositories;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DataAccessInfrastructure.Repositories
{
    public class SqlBaseRepository : ISqlBaseRepository
    {
        private readonly string _fccConStr = ConfigurationManager.ConnectionStrings["FccConStr"].ConnectionString;
        private readonly Dictionary<int, SqlConnection> _conSet;

        public string FccConStr
        {
            get { return _fccConStr; }
        }

        public SqlBaseRepository()
        {
            _conSet = new Dictionary<int, SqlConnection>();
        }

        public async Task<T> QueryFoD<T>(string query)
        {
            return await (new SqlConnection(_fccConStr)).QueryFirstOrDefaultAsync<T>(query);
        }

        public async Task<T> QueryFoD<T>(string query, object parameters)
        {
            return await (new SqlConnection(_fccConStr)).QueryFirstOrDefaultAsync<T>(query, parameters);
        }

        public async Task<IEnumerable<T>> Query<T>(string query)
        {
            return await (new SqlConnection(_fccConStr)).QueryAsync<T>(query);
        }

        public async Task<IEnumerable<T>> Query<T>(string query, object parameters)
        {
            return await (new SqlConnection(_fccConStr)).QueryAsync<T>(query, parameters);
        }

        public async Task<int> Execute(string query, object parameters)
        {
            return await (new SqlConnection(_fccConStr)).ExecuteAsync(query, parameters);
        }

        public async Task<T> ExecuteScalar<T>(string query, object parameters)
        {
            return await (new SqlConnection(_fccConStr)).ExecuteScalarAsync<T>(query, parameters);
        }
    }
}