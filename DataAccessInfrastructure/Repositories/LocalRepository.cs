using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Shared.Enums;
using Shared.Interfaces.Repositories;
using Shared.Models;

namespace DataAccessInfrastructure.Repositories
{
    public class LocalRepository : ISqlRepository
    {
        List<Person> ListPerson { get; set; }
        List<PersonName> ListPersonName { get; set; }
        List<PersonRelation> ListPersonRelation { get; set; }

        public LocalRepository()
        {
            var guid1 = Guid.NewGuid().ToString();
            var guid2 = Guid.NewGuid().ToString();
            var p1rel = new PersonRelation
            {
                Id = Guid.NewGuid().ToString(),
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                IsActive = true,
                OwnerId = guid2,
                PersonId = guid1,
                RelationTypeId = RelationType.Husband,
            };
            var p2rel = new PersonRelation
            {
                Id = Guid.NewGuid().ToString(),
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                IsActive = true,
                OwnerId = guid1,
                PersonId = guid2,
                RelationTypeId = RelationType.Wife,
            };

            ListPerson = new List<Person>
            {
                new Person {
                    Id = guid1,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    IsActive = true,
                    Name = "Thomas",
                    Lastname = "Mayer",
                    Patronym = "Sergeevich",
                    BornTimeKnown = true,
                    DeadTimeKnown = false,
                    BornTime = DateTime.Parse("25/03/1991"),
                    DeadTime = null,
                    Sex = true,
                },
                new Person {
                    Id = guid2,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    IsActive = true,
                    Name = "Margarita",
                    Lastname = "Mayer",
                    Patronym = "Sergeevna",
                    BornTimeKnown = true,
                    DeadTimeKnown = false,
                    BornTime = DateTime.Parse("16/04/1994"),
                    DeadTime = null,
                    Sex = false,
                }
            };
            ListPersonName = new List<PersonName>
            {
                new PersonName
                {
                    Id = Guid.NewGuid().ToString(),
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    IsActive = true,
                    Name = "Margarita",
                    Lastname = "Steinfeld",
                    Patronym = "Sergeevna",
                    PersonId = guid2,
                }
            };
            ListPersonRelation = new List<PersonRelation>
            {
                p1rel,
                p2rel,
            };
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
        public IEnumerable<PersonName> ReadAllPersonNameByPersonId(string id)
        {
            return ListPersonName.Where(e => e.PersonId == id);
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

        #region PersonRelation

        public PersonRelation ReadPersonRelation(string id)
        {
            return ListPersonRelation
                .FirstOrDefault(e => e.Id == id);
        }
        public IEnumerable<PersonRelation> ReadAllPersonRelationByOwnerId(string id)
        {
            return ListPersonRelation.Where(e => e.OwnerId == id);
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