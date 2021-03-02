using WebAppFcc.Shared.Enums;
using WebAppFcc.Shared.Models;
using WebAppFcc.Shared.Viewmodels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace WebAppFcc.Shared.Interfaces.Managers
{
    public interface IFccManager
    {
        Task<Person> GetPerson(Guid id);
        Task<Person> CreatePerson(Person entity);
        Task<Person> UpdatePerson(Person entity);
        Task<Person> DeletePerson(Guid id);
        Task<IEnumerable<Person>> GetPersonList(int skip, int take);
        Task<int> PersonCount();

        Task<PersonPhoto> CreatePersonPhoto(PersonPhoto photo);
        Task<PersonDocument> CreatePersonDocument(PersonDocument document);
    }
}
