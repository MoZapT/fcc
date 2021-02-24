using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WebAppFcc.Shared.Enums;
using WebAppFcc.Shared.Models;

namespace WebAppFcc.Shared.Interfaces.DataServices
{
    public interface IPersonDataService : IBaseDataService
    {
        VmState ViewState { get; set; }
        int Skip { get; set; }
        int Take { get; set; }
        int PersonCount { get; set; }

        Person Person { get; set; }
        IEnumerable<Person> Persons { get; set; }

        Task LoadPersonList();
        Task LoadPersonDetails(Guid id);

        Task DeletePerson(Guid id);
        Task AddPerson(Person person);
        Task UpdatePerson(Person person);

        void CreatePerson();
    }
}
