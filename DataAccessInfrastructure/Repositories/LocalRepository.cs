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

        public Task<Person> ReadPerson(string id)
        {
            return Task.FromResult(ListPerson
                .FirstOrDefault(e => e.Id == id));
        }
        public Task<IEnumerable<Person>> ReadAllPerson()
        {
            return Task.FromResult(ListPerson as IEnumerable<Person>);
        }
        public Task<string> CreatePerson(Person entity)
        {
            ListPerson.Add(entity);

            return Task.FromResult(entity.Id);
        }
        public Task<bool> UpdatePerson(Person entity)
        {
            var model = ListPerson.FirstOrDefault(e => e.Id == entity.Id);
            model = entity;

            return Task.FromResult(true);
        }
        public Task<bool> DeletePerson(string id)
        {
            var model = ListPerson.FirstOrDefault(e => e.Id == id);
            return Task.FromResult(ListPerson.Remove(model));
        }

        #endregion

        #region PersonName

        public Task<PersonName> ReadPersonName(string id)
        {
            return Task.FromResult(ListPersonName
                .FirstOrDefault(e => e.Id == id));
        }
        public Task<IEnumerable<PersonName>> ReadAllPersonName()
        {
            return Task.FromResult(ListPersonName as IEnumerable<PersonName>);
            throw new System.NotImplementedException();
        }
        public Task<string> CreatePersonName(PersonName entity)
        {
            ListPersonName.Add(entity);

            return Task.FromResult(entity.Id);
        }
        public Task<bool> UpdatePersonName(PersonName entity)
        {
            var model = ListPersonName.FirstOrDefault(e => e.Id == entity.Id);
            model = entity;

            return Task.FromResult(true);
        }
        public Task<bool> DeletePersonName(string id)
        {
            var model = ListPersonName.FirstOrDefault(e => e.Id == id);
            return Task.FromResult(ListPersonName.Remove(model));
        }

        #endregion

        #region PersonRelationGroup

        public Task<PersonRelationGroup> ReadPersonRelationGroup(string id)
        {
            return Task.FromResult(ListPersonRelationGroup
                .FirstOrDefault(e => e.Id == id));
        }
        public Task<IEnumerable<PersonRelationGroup>> ReadAllPersonRelationGroup()
        {
            return Task.FromResult(ListPersonRelationGroup as IEnumerable<PersonRelationGroup>);
        }
        public Task<string> CreatePersonRelationGroup(PersonRelationGroup entity)
        {
            ListPersonRelationGroup.Add(entity);

            return Task.FromResult(entity.Id);
        }
        public Task<bool> UpdatePersonRelationGroup(PersonRelationGroup entity)
        {
            var model = ListPersonRelationGroup.FirstOrDefault(e => e.Id == entity.Id);
            model = entity;

            return Task.FromResult(true);
        }
        public Task<bool> DeletePersonRelationGroup(string id)
        {
            var model = ListPersonRelationGroup.FirstOrDefault(e => e.Id == id);
            return Task.FromResult(ListPersonRelationGroup.Remove(model));
        }

        #endregion

        #region PersonRelation

        public Task<PersonRelation> ReadPersonRelation(string id)
        {
            return Task.FromResult(ListPersonRelation
                .FirstOrDefault(e => e.Id == id));
        }
        public Task<IEnumerable<PersonRelation>> ReadAllPersonRelation()
        {
            return Task.FromResult(ListPersonRelation as IEnumerable<PersonRelation>);
        }
        public Task<string> CreatePersonRelation(PersonRelation entity)
        {
            ListPersonRelation.Add(entity);

            return Task.FromResult(entity.Id);
        }
        public Task<bool> UpdatePersonRelation(PersonRelation entity)
        {
            var model = ListPersonRelation.FirstOrDefault(e => e.Id == entity.Id);
            model = entity;

            return Task.FromResult(true);
        }
        public Task<bool> DeletePersonRelation(string id)
        {
            var model = ListPersonRelation.FirstOrDefault(e => e.Id == id);
            return Task.FromResult(ListPersonRelation.Remove(model));
        }

        #endregion

    }
}