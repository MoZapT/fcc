using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WebAppFcc.Shared.Enums;
using WebAppFcc.Shared.Models;

namespace WebAppFcc.Shared.Interfaces.DataServices
{
    public interface IPersonDataService 
    {
        event Action OnChange;
        HttpClient Http { get; }

        VmState ViewState { get; set; }
        int Skip { get; set; }
        int Take { get; set; }

        Person Person { get; set; }
        IEnumerable<Person> Persons { get; set; }

        void Init(Action action);

        Task LoadPersonList();
        Task LoadPersonDetails(Guid id);

        Task DeletePerson(Guid id);
        Task AddPerson(Person person);
        Task UpdatePerson(Person person);

        void CreatePerson();
    }
}
