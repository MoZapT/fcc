using Shared.Viewmodels;
using Shared.Models;
using System.Collections.Generic;
using Shared.Enums;

namespace Shared.Interfaces.Managers
{
    public interface IFccManager
    {
        void HandleAction(PersonViewModel vm);

        string SetPersonRelations(PersonRelation entity);
        bool DeletePersonRelation(string id);
        IEnumerable<KeyValuePair<string, string>> PersonTypeahead(string excludePersonId, string query);
        IEnumerable<PersonRelationGroup> GetPersonRelationGroupsByPersonId(string personId);

        bool SetPersonRelation(PersonRelation from, PersonRelation to, RelationType type);
        Person GetPerson(string userId);
    }
}
