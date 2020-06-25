using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Shared.Interfaces.Repositories
{
    public interface ISqlBaseRepository
    {
        Task<bool> Transaction(Task task);
        Task<bool> Transaction(Task task, SqlConnection con);
    }
}
