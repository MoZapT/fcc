using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Shared.Interfaces.Repositories;
using Shared.Models;

namespace DataAccessInfrastructure.Repositories
{
    public class LocalRepository : ISqlRepository
    {
        List<Person> ListPerson { get; set; }
        List<PersonName> ListPersonName { get; set; }
        List<PersonRelationGroup> ListPersonRelationGroup { get; set; }
        List<PersonRelation> ListPersonRelation { get; set; }

        public LocalRepository()
        {
            ListPerson = new List<Person>();
            ListPersonName = new List<PersonName>();
            ListPersonRelationGroup = new List<PersonRelationGroup>();
            ListPersonRelation = new List<PersonRelation>();
        }

        #region Person

        public Person ReadPerson(string id)
        {
            return ListPerson
                .FirstOrDefault(e => e.Id == id);
        }
        public IEnumerable<Person> ReadAllPerson()
        {
            return ListPerson as IEnumerable<Person>;
        }
        public string CreatePerson(Person entity)
        {
            ListPerson.Add(entity);

            return entity.Id;
        }
        public bool UpdatePerson(Person entity)
        {
            var model = ListPerson.FirstOrDefault(e => e.Id == entity.Id);
            model = entity;

            return true;
        }
        public bool DeletePerson(string id)
        {
            var model = ListPerson.FirstOrDefault(e => e.Id == id);
            return ListPerson.Remove(model);
        }

        #endregion

        #region PersonName

        public PersonName ReadPersonName(string id)
        {
            return ListPersonName
                .FirstOrDefault(e => e.Id == id);
        }
        public IEnumerable<PersonName> ReadAllPersonName()
        {
            return ListPersonName as IEnumerable<PersonName>;
            throw new System.NotImplementedException();
        }
        public string CreatePersonName(PersonName entity)
        {
            ListPersonName.Add(entity);

            return entity.Id;
        }
        public bool UpdatePersonName(PersonName entity)
        {
            var model = ListPersonName.FirstOrDefault(e => e.Id == entity.Id);
            model = entity;

            return true;
        }
        public bool DeletePersonName(string id)
        {
            var model = ListPersonName.FirstOrDefault(e => e.Id == id);
            return ListPersonName.Remove(model);
        }

        #endregion

        #region PersonRelationGroup

        public PersonRelationGroup ReadPersonRelationGroup(string id)
        {
            return ListPersonRelationGroup
                .FirstOrDefault(e => e.Id == id);
        }
        public IEnumerable<PersonRelationGroup> ReadAllPersonRelationGroup()
        {
            return ListPersonRelationGroup as IEnumerable<PersonRelationGroup>;
        }
        public string CreatePersonRelationGroup(PersonRelationGroup entity)
        {
            ListPersonRelationGroup.Add(entity);

            return entity.Id;
        }
        public bool UpdatePersonRelationGroup(PersonRelationGroup entity)
        {
            var model = ListPersonRelationGroup.FirstOrDefault(e => e.Id == entity.Id);
            model = entity;

            return true;
        }
        public bool DeletePersonRelationGroup(string id)
        {
            var model = ListPersonRelationGroup.FirstOrDefault(e => e.Id == id);
            return ListPersonRelationGroup.Remove(model);
        }

        #endregion

        #region PersonRelation

        public PersonRelation ReadPersonRelation(string id)
        {
            return ListPersonRelation
                .FirstOrDefault(e => e.Id == id);
        }
        public IEnumerable<PersonRelation> ReadAllPersonRelation()
        {
            return ListPersonRelation as IEnumerable<PersonRelation>;
        }
        public string CreatePersonRelation(PersonRelation entity)
        {
            ListPersonRelation.Add(entity);

            return entity.Id;
        }
        public bool UpdatePersonRelation(PersonRelation entity)
        {
            var model = ListPersonRelation.FirstOrDefault(e => e.Id == entity.Id);
            model = entity;

            return true;
        }
        public bool DeletePersonRelation(string id)
        {
            var model = ListPersonRelation.FirstOrDefault(e => e.Id == id);
            return ListPersonRelation.Remove(model);
        }

        #endregion

    }
}