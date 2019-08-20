using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Interfaces.Repositories
{
    public interface IRepoFactory
    {

        T Read<T>(string id);
        IEnumerable<T> ReadAll<T>();
        string Create<T>(T entity);
        bool Update<T>(T entity);
        bool Delete<T>(T entity);

    }
}
