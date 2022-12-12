using WAFcc.Enums;
using WAFcc.Models;
using WebAppFcc.Shared.Interfaces.DataServices;

namespace WAFcc.Interfaces.DataServices
{
    public interface IPersonDataService : IBaseDataService
    {
        VmState ViewState { get; set; }
        int Skip { get; set; }
        int Take { get; set; }
        int PersonCount { get; set; }

        Person Person { get; set; }
        IEnumerable<Person> Persons { get; set; }

        Task LoadPersonList(bool resetSkip = false);
        Task LoadPersonDetails(Guid id);

        Task DeletePerson(Guid id);
        Task AddPerson(Person person);
        Task UpdatePerson(Person person);

        void CreatePerson();

        Task<Relation> DeleteRelation(Guid id);
        Task<Relation> SetRelation(Relation relation);
        Task<PersonRelation> DeletePersonRelation(Guid id);
        Task<PersonRelation> SetPersonRelation(PersonRelation relation);
    }
}
