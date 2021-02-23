using WebAppFcc.Shared.Enums;
using WebAppFcc.Shared.Models;
using WebAppFcc.Shared.Viewmodels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAppFcc.Shared.Interfaces.Managers
{
    public interface IFccManager
    {
        Task<Person> GetPerson(string id);
        Task<Person> UpdatePerson(Person entity);
        Task<Person> DeletePerson(string id);
        Task<IEnumerable<Person>> GetPersonList();
    }
}
