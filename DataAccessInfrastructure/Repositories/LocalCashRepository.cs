using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using Dapper;
using Shared.Models;
using Shared.Interfaces.Repositories;

namespace DataAccessInfrastructure.Repositories
{
    public class LocalCashRepository : ILocalCashRepository
    {

        public IEnumerable<Person> Persons { get; set; }
        public IEnumerable<PersonName> PersonNames { get; set; }
        public IEnumerable<PersonRelation> PersonRelations { get; set; }
        public IEnumerable<PersonRelationGroup> PersonRelationGroups { get; set; }

        public LocalCashRepository()
        {
            InitializeCash();
        }

        public void InitializeCash()
        {

        }

        #region Person

        public Person ReadPerson(string id)
        {
            return Persons.FirstOrDefault(e => e.Id == id);
        }
        public IEnumerable<Person> ReadAllPerson()
        {
            return Persons;
        }
        public string CreatePerson(Person entity)
        {
            entity.Id = Guid.NewGuid().ToString();

            var list = Persons.ToList();
            list.Add(entity);
            Persons = list;

            return entity.Id;
        }
        public bool UpdatePerson(Person entity)
        {
            try
            {
                DeletePerson(entity);

                var list = Persons.ToList();
                list.Add(entity);
                Persons = list;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool DeletePerson(Person entity)
        {
            try
            {
                var list = Persons.ToList();
                var removeObj = list.FirstOrDefault(e => e.Id == entity.Id);
                if (removeObj != null)
                {
                    list.Remove(removeObj);
                    Persons = list;
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        public PersonName ReadPersonName(string id)
        {
            return PersonNames.FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<PersonName> ReadAllPersonName()
        {
            return PersonNames;
        }

        public string CreatePersonName(PersonName entity)
        {
            entity.Id = Guid.NewGuid().ToString();

            var list = PersonNames.ToList();
            list.Add(entity);
            PersonNames = list;

            return entity.Id;
        }

        public bool UpdatePersonName(PersonName entity)
        {
            try
            {
                DeletePersonName(entity);

                var list = PersonNames.ToList();
                list.Add(entity);
                PersonNames = list;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeletePersonName(PersonName entity)
        {
            try
            {
                var list = PersonNames.ToList();
                var removeObj = list.FirstOrDefault(e => e.Id == entity.Id);
                if (removeObj != null)
                {
                    list.Remove(removeObj);
                    PersonNames = list;
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public PersonRelation ReadPersonRelation(string id)
        {
            return PersonRelations.FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<PersonRelation> ReadAllPersonRelation()
        {
            return PersonRelations;
        }

        public string CreatePersonRelation(PersonRelation entity)
        {
            entity.Id = Guid.NewGuid().ToString();

            var list = PersonRelations.ToList();
            list.Add(entity);
            PersonRelations = list;

            return entity.Id;
        }

        public bool UpdatePersonRelation(PersonRelation entity)
        {
            try
            {
                DeletePersonRelation(entity);

                var list = PersonRelations.ToList();
                list.Add(entity);
                PersonRelations = list;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeletePersonRelation(PersonRelation entity)
        {
            try
            {
                var list = PersonRelations.ToList();
                var removeObj = list.FirstOrDefault(e => e.Id == entity.Id);
                if (removeObj != null)
                {
                    list.Remove(removeObj);
                    PersonRelations = list;
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public PersonRelationGroup ReadPersonRelationGroup(string id)
        {
            return PersonRelationGroups.FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<PersonRelationGroup> ReadAllPersonRelationGroup()
        {
            return PersonRelationGroups;
        }

        public string CreatePersonRelationGroup(PersonRelationGroup entity)
        {
            entity.Id = Guid.NewGuid().ToString();

            var list = PersonRelationGroups.ToList();
            list.Add(entity);
            PersonRelationGroups = list;

            return entity.Id;
        }

        public bool UpdatePersonRelationGroup(PersonRelationGroup entity)
        {
            try
            {
                DeletePersonRelationGroup(entity);

                var list = PersonRelationGroups.ToList();
                list.Add(entity);
                PersonRelationGroups = list;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeletePersonRelationGroup(PersonRelationGroup entity)
        {
            try
            {
                var list = PersonRelationGroups.ToList();
                var removeObj = list.FirstOrDefault(e => e.Id == entity.Id);
                if (removeObj != null)
                {
                    list.Remove(removeObj);
                    PersonRelationGroups = list;
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}