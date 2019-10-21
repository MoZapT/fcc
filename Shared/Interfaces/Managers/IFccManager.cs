using Shared.Viewmodels;
using Shared.Models;
using System.Collections.Generic;
using Shared.Enums;

namespace Shared.Interfaces.Managers
{
    public interface IFccManager
    {

        #region Person

        string SetPerson(Person entity);
        bool UpdatePerson(Person entity);
        bool ExistPerson(string id);
        Person GetPerson(string userId);
        List<Person> GetListPerson();
        IEnumerable<KeyValuePair<string, string>> PersonTypeahead(string excludePersonId, string query);

        #endregion

        #region PersonRelation

        string SetPersonRelations(PersonRelation entity);
        bool DeletePersonRelation(string id);
        bool SetPersonRelation(PersonRelation from, PersonRelation to, RelationType type);

        #endregion

    }
}
