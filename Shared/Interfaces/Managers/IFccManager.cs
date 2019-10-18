using Shared.Viewmodels;
using Shared.Models;
using System.Collections.Generic;

namespace Shared.Interfaces.Managers
{
    public interface IFccManager
    {
        void HandleAction(PersonViewModel vm);

        string SetPersonRelations(PersonRelation entity);
        bool DeletePersonRelation(string id);
        IEnumerable<PersonRelationGroup> GetPersonRelationGroupsByPersonId(string id);
        IEnumerable<KeyValuePair<string, string>> PersonTypeahead(string excludePersonId, string query);

        Person GetPerson(string userId);
    }
}
