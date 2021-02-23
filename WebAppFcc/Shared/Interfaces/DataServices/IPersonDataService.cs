using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WebAppFcc.Shared.Enums;
using WebAppFcc.Shared.Models;

namespace WebAppFcc.Shared.Interfaces.DataServices
{
    public interface IPersonDataService
    {
        HttpClient Http { get; }

        VmState ViewState { get; set; }
        int Skip { get; set; }
        int Take { get; set; }

        Person Person { get; set; }
        IEnumerable<Person> Persons { get; set; }

        Task LoadPersonList();
        Task LoadPersonDetails(string id);
        Task DeletePerson(string id);
        Task CreatePerson(Person person);
    }
}
