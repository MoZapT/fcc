using Shared.Models;
using Shared.Viewmodels.Family;
using System.Collections.Generic;

namespace Shared.Interfaces.Managers
{
    public interface IFccManager
    {
        string HandleAction(PersonViewModel vm);
        string SetPersonRelations(PersonRelation entity);
        bool DeletePersonRelation(string id);
        IEnumerable<PersonRelation> GetPersonRelationsByPersonId(string id);
    }
}
