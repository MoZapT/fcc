﻿using System;
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
        List<PersonRelationGroup> ListPersonRelationGroup { get; set; }

        public LocalRepository()
        {
            var per1 = new Person
            {
                Id = Guid.NewGuid().ToString(),
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
            };
            var per2 = new Person
            {
                Id = Guid.NewGuid().ToString(),
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
            };
            var p1rel = new PersonRelation
            {
                Id = Guid.NewGuid().ToString(),
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                IsActive = true,
                PersonId = per1.Id,
            };
            var p2rel = new PersonRelation
            {
                Id = Guid.NewGuid().ToString(),
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                IsActive = true,
                PersonId = per2.Id,
            };
            var p2name = new PersonName
            {
                Id = Guid.NewGuid().ToString(),
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                IsActive = true,
                Name = "Margarita",
                Lastname = "Steinfeld",
                Patronym = "Sergeevna",
                PersonId = per2.Id,
            };
            var prg = new PersonRelationGroup
            {
                Id = Guid.NewGuid().ToString(),
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                IsActive = true,
                RelationTypeId = RelationType.HusbandWife,
                //Members = new List<Person>() { per1, per2 },
            };

            ListPerson = new List<Person>
            {
                per1, per2
            };
            ListPersonName = new List<PersonName>
            {
                p2name
            };
            ListPersonRelation = new List<PersonRelation>
            {
                p1rel,
                p2rel,
            };
            ListPersonRelationGroup = new List<PersonRelationGroup>
            {
                prg
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
        public IEnumerable<Person> ReadAllPersonByRelationGroupId(string id)
        {
            var relations = ListPersonRelation.Where(e => e.PersonRelationGroupId == id);

            return ListPerson
                .Where(e => relations.FirstOrDefault(r => r.PersonId == e.Id) != null);
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
        public IEnumerable<KeyValuePair<string, string>> GetPersonSelectList()
        {
            return ListPerson.Select(e => new KeyValuePair<string, string>(e.Id, e.GetFullName()));
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
            var relation = ListPersonRelation.FirstOrDefault(e => e.Id == id);
            relation.Member = ReadPerson(relation.PersonId);
            return relation;
        }
        public IEnumerable<PersonRelation> ReadAllPersonRelationByPersonId(string id)
        {
            return ListPersonRelation
                .Where(e => e.PersonId == id)
                .Select(e => {
                    e.Member = ReadPerson(e.PersonId);
                    return e;
                });
        }
        public IEnumerable<PersonRelation> ReadAllPersonRelationByGroupId(string id)
        {
            return ListPersonRelation
                .Where(e => e.PersonRelationGroupId == id)
                .Select(e => {
                    e.Member = ReadPerson(e.PersonId);
                    return e;
                });
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

        #region PersonRelation

        public PersonRelationGroup ReadPersonRelationGroup(string id)
        {
            var prg = ListPersonRelationGroup.FirstOrDefault(e => e.Id == id);
            prg.Members = ReadAllPersonRelationByGroupId(id).ToList();
            return prg;
        }
        public IEnumerable<PersonRelationGroup> ReadAllPersonRelationGroupsByPersonId(string id)
        {
            return ListPersonRelationGroup
                .Select(e => {
                    e.Members = ReadAllPersonRelationByGroupId(e.Id).ToList();
                    return e; });
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

    }
}