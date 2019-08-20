using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Shared.Models;
using Shared.Interfaces.Repositories;

namespace DataAccessInfrastructure.Repositories
{
    public class RepoFactory : IRepoFactory
    {
        private ILocalCashRepository _xmlRepo;
        private ISqlRepository _sqlRepo;

        public RepoFactory(Type repo, Type mgr)
        {
            Initialize(repo, mgr);
        }
        private void Initialize(Type repo, Type mgr)
        {
            if      (typeof(ILocalCashRepository) == repo) _xmlRepo = new LocalCashRepository();
            else if (typeof(ISqlRepository) == repo) _sqlRepo = new SqlRepository();
        }

        public T Read<T>(string id)
        {
            var type = typeof(T);
            var converter = System.ComponentModel.TypeDescriptor.GetConverter(type);

            if (type == typeof(Person))
            {
                var result = _xmlRepo.ReadPerson(id);
                return (T)converter.ConvertFrom(result);
            }
            else if (type == typeof(PersonName))
            {
                var result = _xmlRepo.ReadPersonName(id);
                return (T)converter.ConvertFrom(result);
            }
            else if (type == typeof(PersonRelation))
            {
                var result = _xmlRepo.ReadPersonRelation(id);
                return (T)converter.ConvertFrom(result);
            }
            else if (type == typeof(PersonRelationGroup))
            {
                var result = _xmlRepo.ReadPersonRelationGroup(id);
                return (T)converter.ConvertFrom(result);
            }
            else
            {
                return default(T);
            }
        }
        public IEnumerable<T> ReadAll<T>()
        {
            var type = typeof(T);
            var converter = System.ComponentModel.TypeDescriptor.GetConverter(type);

            if (type == typeof(Person))
            {
                var result = _xmlRepo.ReadAllPerson();
                return (IEnumerable<T>)converter.ConvertFrom(result);
            }
            else if (type == typeof(PersonName))
            {
                var result = _xmlRepo.ReadAllPersonName();
                return (IEnumerable<T>)converter.ConvertFrom(result);
            }
            else if (type == typeof(PersonRelation))
            {
                var result = _xmlRepo.ReadAllPersonRelation();
                return (IEnumerable<T>)converter.ConvertFrom(result);
            }
            else if (type == typeof(PersonRelationGroup))
            {
                var result = _xmlRepo.ReadAllPersonRelationGroup();
                return (IEnumerable<T>)converter.ConvertFrom(result);
            }
            else
            {
                return default(IEnumerable<T>);
            }
        }
        public string Create<T>(T entity)
        {
            var type = typeof(T);
            var converter = System.ComponentModel.TypeDescriptor.GetConverter(type);

            if (type == typeof(Person))
            {
                string result = _xmlRepo.CreatePerson((Person)converter.ConvertFrom(entity));
                return result;
            }
            else if (type == typeof(PersonName))
            {
                string result = _xmlRepo.CreatePersonName((PersonName)converter.ConvertFrom(entity));
                return result;
            }
            else if (type == typeof(PersonRelation))
            {
                string result = _xmlRepo.CreatePersonRelation((PersonRelation)converter.ConvertFrom(entity));
                return result;
            }
            else if (type == typeof(PersonRelationGroup))
            {
                string result = _xmlRepo.CreatePersonRelationGroup((PersonRelationGroup)converter.ConvertFrom(entity));
                return result;
            }
            else
            {
                return null;
            }
        }
        public bool Update<T>(T entity)
        {
            var type = typeof(T);
            var converter = System.ComponentModel.TypeDescriptor.GetConverter(type);

            if (type == typeof(Person))
            {
                bool result = _xmlRepo.UpdatePerson((Person)converter.ConvertFrom(entity));
                return result;
            }
            else if (type == typeof(PersonName))
            {
                bool result = _xmlRepo.UpdatePersonName((PersonName)converter.ConvertFrom(entity));
                return result;
            }
            else if (type == typeof(PersonRelation))
            {
                bool result = _xmlRepo.UpdatePersonRelation((PersonRelation)converter.ConvertFrom(entity));
                return result;
            }
            else if (type == typeof(PersonRelationGroup))
            {
                bool result = _xmlRepo.UpdatePersonRelationGroup((PersonRelationGroup)converter.ConvertFrom(entity));
                return result;
            }
            else
            {
                return false;
            }
        }
        public bool Delete<T>(T entity)
        {
            var type = typeof(T);
            var converter = System.ComponentModel.TypeDescriptor.GetConverter(type);

            if (type == typeof(Person))
            {
                bool result = _xmlRepo.DeletePerson((Person)converter.ConvertFrom(entity));
                return result;
            }
            else if (type == typeof(PersonName))
            {
                bool result = _xmlRepo.DeletePersonName((PersonName)converter.ConvertFrom(entity));
                return result;
            }
            else if (type == typeof(PersonRelation))
            {
                bool result = _xmlRepo.DeletePersonRelation((PersonRelation)converter.ConvertFrom(entity));
                return result;
            }
            else if (type == typeof(PersonRelationGroup))
            {
                bool result = _xmlRepo.DeletePersonRelationGroup((PersonRelationGroup)converter.ConvertFrom(entity));
                return result;
            }
            else
            {
                return false;
            }
        }
    }
}