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
        Person ReadPerson(string id);
        IEnumerable<Person> ReadAllPerson();
        string CreatePerson(Person entity);
        bool UpdatePerson(Person entity);
        bool DeletePerson(string id);

        PersonName ReadPersonName(string id);
        IEnumerable<PersonName> ReadAllPersonName();
        string CreatePersonName(PersonName entity);
        bool UpdatePersonName(PersonName entity);
        bool DeletePersonName(string id);

        PersonRelation ReadPersonRelation(string id);
        IEnumerable<PersonRelation> ReadAllPersonRelation();
        string CreatePersonRelation(PersonRelation entity);
        bool UpdatePersonRelation(PersonRelation entity);
        bool DeletePersonRelation(string id);

        PersonRelationGroup ReadPersonRelationGroup(string id);
        IEnumerable<PersonRelationGroup> ReadAllPersonRelationGroup();
        string CreatePersonRelationGroup(PersonRelationGroup entity);
        bool UpdatePersonRelationGroup(PersonRelationGroup entity);
        bool DeletePersonRelationGroup(string id);

    }
}
