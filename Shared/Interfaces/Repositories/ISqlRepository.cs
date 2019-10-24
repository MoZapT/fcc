using Shared.Enums;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Interfaces.Repositories
{
    public interface ISqlRepository : ISqlBaseRepository
    {
        Person ReadPerson(string id);
        IEnumerable<Person> ReadAllPerson();
        IEnumerable<Person> ReadAllPersonByRelationGroupId(string id);
        string CreatePerson(Person entity);
        bool UpdatePerson(Person entity);
        bool DeletePerson(string id);
        IEnumerable<KeyValuePair<string, string>> GetPersonSelectList();
        IEnumerable<KeyValuePair<string, string>> GetPersonSelectList(string excludePersonId, string query);

        PersonName ReadLastPersonName(string id);
        IEnumerable<PersonName> ReadAllPersonNameByPersonId(string id);
        string CreatePersonName(PersonName entity);
        bool UpdatePersonName(PersonName entity);
        bool DeletePersonName(string id);

        #region PersonRelation

        PersonRelation ReadPersonRelation(string inviter, string invited, RelationType type);
        IEnumerable<PersonRelation> ReadAllPersonRelationBetweenInviterAndInvited(string inviter, string invited);
        IEnumerable<PersonRelation> ReadAllPersonRelationsByInviterId(string id);
        string CreatePersonRelation(PersonRelation entity);
        bool UpdatePersonRelation(PersonRelation entity);
        bool DeletePersonRelation(string inviter, string invited);
        bool DeletePersonRelation(string inviter, string invited, RelationType type);
        bool IsMarried(string personId);

        #endregion

        #region PersonBiography

        PersonBiography ReadPersonBiographyByPersonId(string personId);
        string CreatePersonBiography(PersonBiography entity);
        bool UpdatePersonBiography(PersonBiography entity);

        #endregion

    }
}
