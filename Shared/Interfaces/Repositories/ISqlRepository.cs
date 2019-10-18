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

        PersonRelation ReadPersonRelation(string id);
        IEnumerable<PersonRelation> ReadAllPersonRelationByPersonId(string id);
        IEnumerable<PersonRelation> ReadAllPersonRelationByGroupId(string id);
        string CreatePersonRelation(PersonRelation entity);
        bool UpdatePersonRelation(PersonRelation entity);
        bool DeletePersonRelation(string id);

        PersonRelationGroup ReadPersonRelationGroup(string id);
        PersonRelationGroup ReadPersonRelationGroupByPersonAndType(string personId, int type);
        IEnumerable<PersonRelationGroup> ReadAllPersonRelationGroupsByPersonId(string id);
        string CreatePersonRelationGroup(PersonRelationGroup entity);
        bool UpdatePersonRelationGroup(PersonRelationGroup entity);
        bool DeletePersonRelationGroup(string id);
        bool MoveRelationsToOtherRelationGroupAndDelete(string fromId, string toId);

    }
}
