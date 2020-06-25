using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shared.Interfaces.Repositories
{
    public interface ISqlBaseRepository
    {
        Task<T> QueryFoD<T>(string query);
        Task<T> QueryFoD<T>(string query, object parameters);
        Task<IEnumerable<T>> Query<T>(string query);
        Task<IEnumerable<T>> Query<T>(string query, object parameters);
        Task<int> Execute(string query, object parameters);
        Task<T> ExecuteScalar<T>(string query, object parameters);
    }
}
