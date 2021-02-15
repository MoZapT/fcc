using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAppFcc.Shared.Interfaces.Repositories
{
    public interface ISqlBaseRepository
    {
        Task<T> QueryFoD<T>(string query);
        Task<T> QueryFoD<T>(string query, object parameters, System.Data.CommandType type = System.Data.CommandType.Text);
        Task<IEnumerable<T>> Query<T>(string query);
        Task<IEnumerable<T>> Query<T>(string query, object parameters, System.Data.CommandType type = System.Data.CommandType.Text);
        Task<int> Execute(string query, object parameters, System.Data.CommandType type = System.Data.CommandType.Text);
        Task<T> ExecuteScalar<T>(string query, object parameters, System.Data.CommandType type = System.Data.CommandType.Text);
    }
}
