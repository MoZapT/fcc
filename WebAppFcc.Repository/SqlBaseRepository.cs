using WebAppFcc.Shared.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace WebAppFcc.Repository
{
    public class SqlBaseRepository : ISqlBaseRepository
    {
        //    private readonly string _fccConStr = ConfigurationManager.ConnectionStrings["FccConStr"].ConnectionString;
        //    private readonly Dictionary<int, SqlConnection> _conSet;

        //    public string FccConStr
        //    {
        //        get { return _fccConStr; }
        //    }

        //    public SqlBaseRepository()
        //    {
        //        _conSet = new Dictionary<int, SqlConnection>();
        //    }

        //    public async Task<T> QueryFoD<T>(string query)
        //    {
        //        return await (new SqlConnection(_fccConStr)).QueryFirstOrDefaultAsync<T>(query);
        //    }

        //    public async Task<T> QueryFoD<T>(string query, object parameters, System.Data.CommandType type = System.Data.CommandType.Text)
        //    {
        //        return await (new SqlConnection(_fccConStr)).QueryFirstOrDefaultAsync<T>(query, parameters, commandType: type);
        //    }

        //    public async Task<IEnumerable<T>> Query<T>(string query)
        //    {
        //        return await (new SqlConnection(_fccConStr)).QueryAsync<T>(query);
        //    }

        //    public async Task<IEnumerable<T>> Query<T>(string query, object parameters, System.Data.CommandType type = System.Data.CommandType.Text)
        //    {
        //        return await (new SqlConnection(_fccConStr)).QueryAsync<T>(query, parameters, commandType: type);
        //    }

        //    public async Task<int> Execute(string query, object parameters, System.Data.CommandType type = System.Data.CommandType.Text)
        //    {
        //        return await (new SqlConnection(_fccConStr)).ExecuteAsync(query, parameters, commandType: type);
        //    }

        //    public async Task<T> ExecuteScalar<T>(string query, object parameters, System.Data.CommandType type = System.Data.CommandType.Text)
        //    {
        //        return await (new SqlConnection(_fccConStr)).ExecuteScalarAsync<T>(query, parameters, commandType: type);
        //    }

        //    public async Task<IEnumerable<T>> SelectAll<T>(object parameters)
        //    {
        //        return await (new SqlConnection(_fccConStr)).QueryAsync<T>(GenerateSelectQuery<T>(parameters));
        //    }

        //    public async Task<T> Insert<T>(object parameters, bool output = false)
        //    {
        //        return await (new SqlConnection(_fccConStr))
        //            .QueryFirstOrDefaultAsync<T>(GenerateInsertQuery<T>(output), parameters);
        //    }

        //    public async Task<bool> Update<T>(object parameters, object keys)
        //    {
        //        return await (new SqlConnection(_fccConStr)).ExecuteAsync(GenerateUpdateQuery<T>(parameters, keys), parameters) > 0;
        //    }

        //    public async Task<bool> Delete<T>(object parameters)
        //    {
        //        return await (new SqlConnection(_fccConStr)).ExecuteAsync(GenerateDeleteQuery<T>(parameters), parameters) > 0;
        //    }

        //    private string GenerateSelectQuery<T>(object parameters)
        //    {
        //        StringBuilder sb = new StringBuilder();

        //        Type type = typeof(T);
        //        sb.AppendLine($"SELECT * FROM [{type.Name}]");

        //        if (parameters == null)
        //        {
        //            return sb.ToString();
        //        }

        //        sb.AppendLine("WHERE");
        //        foreach (var prop in parameters.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
        //        {
        //            sb.AppendLine($"[{prop.Name}] = @{prop.Name}");
        //        }

        //        return sb.ToString();
        //    }

        //    private string GenerateInsertQuery<T>(bool output = false)
        //    {
        //        StringBuilder sb = new StringBuilder();

        //        Type type = typeof(T);
        //        PropertyInfo[] properties = type.GetProperties();

        //        sb.AppendLine($"INSERT INTO [{type.Name}]");

        //        sb.AppendLine(@"(");
        //        foreach (var property in properties)
        //        {
        //            sb.AppendLine($"[{property.Name}]");
        //        }
        //        sb.AppendLine(@")");

        //        if (output)
        //            sb.AppendLine(@"OUTPUT INSERTED.Id");

        //        sb.AppendLine(@"VALUES");

        //        sb.AppendLine(@"(");
        //        foreach (var property in properties)
        //        {
        //            sb.AppendLine($"@{property.Name}");
        //        }
        //        sb.AppendLine(@")");

        //        return sb.ToString();
        //    }

        //    private string GenerateUpdateQuery<T>(object parameters, object keys)
        //    {
        //        StringBuilder sb = new StringBuilder();

        //        Type type = typeof(T);

        //        sb.AppendLine($"UPDATE [{type.Name}]");

        //        sb.AppendLine("SET");
        //        string comma = string.Empty;
        //        foreach (var prop in parameters.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
        //        {
        //            sb.AppendLine($"{comma}[{prop.Name}] = @{prop.Name}");

        //            if (string.IsNullOrWhiteSpace(comma))
        //                comma = ",";
        //        }

        //        string and = string.Empty;
        //        sb.AppendLine("WHERE");
        //        foreach (var prop in keys.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
        //        {
        //            sb.AppendLine($"{and} [{prop.Name}] = @{prop.Name}");

        //            if (string.IsNullOrWhiteSpace(and))
        //                and = "AND";
        //        }

        //        return sb.ToString();
        //    }

        //    private string GenerateDeleteQuery<T>(object parameters)
        //    {
        //        StringBuilder sb = new StringBuilder();

        //        Type type = typeof(T);

        //        sb.AppendLine($"DELETE FROM [{type.Name}]");
        //        if (parameters != null)
        //        {
        //            sb.AppendLine("WHERE");
        //            foreach (var prop in parameters.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
        //            {
        //                sb.AppendLine($"[{prop.Name}] = @{prop.Name}");
        //            }
        //        }

        //        return sb.ToString();
        //    }
        //}
        public Task<int> Execute(string query, object parameters, CommandType type = CommandType.Text)
        {
            throw new NotImplementedException();
        }

        public Task<T> ExecuteScalar<T>(string query, object parameters, CommandType type = CommandType.Text)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> Query<T>(string query)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> Query<T>(string query, object parameters, CommandType type = CommandType.Text)
        {
            throw new NotImplementedException();
        }

        public Task<T> QueryFoD<T>(string query)
        {
            throw new NotImplementedException();
        }

        public Task<T> QueryFoD<T>(string query, object parameters, CommandType type = CommandType.Text)
        {
            throw new NotImplementedException();
        }
    }
}