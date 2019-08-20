using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Interfaces.Repositories
{
    public interface ILocalCashRepository
    {

        IEnumerable<Person> Persons { get; set; }
        IEnumerable<PersonName> PersonNames { get; set; }
        IEnumerable<PersonRelation> PersonRelations { get; set; }
        IEnumerable<PersonRelationGroup> PersonRelationGroups { get; set; }

        void InitializeCash();

        Person ReadPerson(string id);
        IEnumerable<Person> ReadAllPerson();
        string CreatePerson(Person entity);
        bool UpdatePerson(Person entity);
        bool DeletePerson(Person entity);

        PersonName ReadPersonName(string id);
        IEnumerable<PersonName> ReadAllPersonName();
        string CreatePersonName(PersonName entity);
        bool UpdatePersonName(PersonName entity);
        bool DeletePersonName(PersonName entity);

        PersonRelation ReadPersonRelation(string id);
        IEnumerable<PersonRelation> ReadAllPersonRelation();
        string CreatePersonRelation(PersonRelation entity);
        bool UpdatePersonRelation(PersonRelation entity);
        bool DeletePersonRelation(PersonRelation entity);

        PersonRelationGroup ReadPersonRelationGroup(string id);
        IEnumerable<PersonRelationGroup> ReadAllPersonRelationGroup();
        string CreatePersonRelationGroup(PersonRelationGroup entity);
        bool UpdatePersonRelationGroup(PersonRelationGroup entity);
        bool DeletePersonRelationGroup(PersonRelationGroup entity);

    }
}
