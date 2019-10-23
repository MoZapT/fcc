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
        bool DeletePerson(string id);
        bool ExistPerson(string id);
        Person GetPerson(string userId);
        List<Person> GetListPerson();
        IEnumerable<KeyValuePair<string, string>> PersonTypeahead(string excludePersonId, string query);

        #endregion

        #region PersonRelation

        List<PersonRelation> ReadAllPersonRelationsBetweenPersons(string inviter, string invited);
        List<PersonRelation> ReadAllPersonRelationsByInviterId(string personId);
        bool DeletePersonRelation(string inviter, string invited, RelationType type);
        bool SetPersonRelation(string inviter, string invited, RelationType type);

        #endregion

        #region PersonBiography

        PersonBiography GetPersonBiographyByPersonId(string person);

        #endregion

    }
}
