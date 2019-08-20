using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Interfaces.Repositories
{
    public interface ISqlRepository
    {
        Task<Person> ReadPerson(string id);
        Task<IEnumerable<Person>> ReadAllPerson();
        Task<string> CreatePerson(Person entity);
        Task<bool> UpdatePerson(Person entity);
        Task<bool> DeletePerson(string id);

        Task<PersonName> ReadPersonName(string id);
        Task<IEnumerable<PersonName>> ReadAllPersonName();
        Task<int> CreatePersonName(PersonName entity);
        Task<bool> UpdatePersonName(PersonName entity);
        Task<bool> DeletePersonName(string id);

        Task<PersonRelation> ReadPersonRelation(string id);
        Task<IEnumerable<PersonRelation>> ReadAllPersonRelation();
        Task<int> CreatePersonRelation(PersonRelation entity);
        Task<bool> UpdatePersonRelation(PersonRelation entity);
        Task<bool> DeletePersonRelation(string id);

        Task<PersonRelationGroup> ReadPersonRelationGroup(string id);
        Task<IEnumerable<PersonRelationGroup>> ReadAllPersonRelationGroup();
        Task<int> CreatePersonRelationGroup(PersonRelationGroup entity);
        Task<bool> UpdatePersonRelationGroup(PersonRelationGroup entity);
        Task<bool> DeletePersonRelationGroup(string id);

    }
}
