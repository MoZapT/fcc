using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Transactions;

namespace Shared.Interfaces.Repositories
{
    public interface ISqlBaseRepository
    {
        Task<bool> Transaction(Task task);
        Task<bool> Transaction(Task task, SqlConnection con);
    }
}
