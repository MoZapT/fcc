using DataAccessInfrastructure.Repositories;
using System.Collections.Generic;
using Shared.Models;
using Shared.Enums;
using System;
using System.Linq;
using System.Web.Mvc.Html;
using Shared.Viewmodels;
using Shared.Interfaces.Managers;
using Shared.Interfaces.Repositories;
using System.Threading.Tasks;

namespace Data.Manager
{
    public class FccManager : IFccManager
    {
        private ISqlRepository _repo;

        public FccManager()
        {
            _repo = new SqlRepository();
        }

        #region Person

        public string SetPerson(Person entity)
        {
            string result = null;

            bool success = false;
            success = _repo.Transaction(new Task(() =>
            {
                result = _repo.CreatePerson(entity);
                var defaultBrotherSisterGroup = new PersonRelationGroup();
                defaultBrotherSisterGroup.Id = Guid.NewGuid().ToString();
                defaultBrotherSisterGroup.Persons.Add(entity);
                defaultBrotherSisterGroup.RelationTypeId = RelationType.BrotherSister;

                var defaultChain = new PersonRelationChain();
                defaultChain.Id = Guid.NewGuid().ToString();
                defaultChain.Children = defaultBrotherSisterGroup;
            }));

            if (!success)
            {
                return null;
            }

            return result;
        }

        public bool UpdatePerson(Person entity)
        {
            return _repo.UpdatePerson(entity);
        }

        public bool DeletePerson(string id)
        {
            return _repo.DeletePerson(id);
        }

        public bool ExistPerson(string id)
        {
            return _repo.ReadPerson(id) != null ? true : false;
        }

        public Person GetPerson(string userId)
        {
            return _repo.ReadPerson(userId);
        }

        public List<Person> GetListPerson()
        {
            return _repo.ReadAllPerson().ToList();
        }

        public IEnumerable<KeyValuePair<string, string>> PersonTypeahead(string excludePersonId, string query)
        {
            return _repo.GetPersonSelectList(excludePersonId, query);
        }

        #endregion

        #region PersonRelation

        public bool DeletePersonRelation(string personId, string groupId)
        {
            return _repo.DeletePersonRelation(id);
        }

        public string SetPersonRelations(PersonRelation entity)
        {
            entity.Id = Guid.NewGuid().ToString();
            entity.DateCreated = DateTime.Now;
            entity.DateModified = DateTime.Now;
            return _repo.CreatePersonRelation(entity);
        }

        public bool SetPersonRelation(PersonRelation from, PersonRelation to, RelationType type)
        {
            var fromRel = _repo.ReadPersonRelationGroupByPersonAndType(from.PersonId, (int)type);
            var toRel = _repo.ReadPersonRelationGroupByPersonAndType(to.PersonId, (int)type);

            if (fromRel != null && toRel != null)
            {
                var newPrl = new PersonRelationGroup() { RelationTypeId = type };
                string id = _repo.CreatePersonRelationGroup(newPrl);
                from.PersonRelationGroupId = id;
                to.PersonRelationGroupId = id;
                _repo.CreatePersonRelation(from);
                _repo.CreatePersonRelation(to);

                _repo.MoveRelationsToOtherRelationGroupAndDelete(fromRel.Id, id);
                _repo.MoveRelationsToOtherRelationGroupAndDelete(toRel.Id, id);
            }
            else if (fromRel == null)
            {
                from.PersonRelationGroupId = toRel.Id;
                to.PersonRelationGroupId = toRel.Id;
                _repo.CreatePersonRelation(from);
                _repo.CreatePersonRelation(to);
            }
            else if (toRel == null)
            {
                from.PersonRelationGroupId = fromRel.Id;
                to.PersonRelationGroupId = fromRel.Id;
                _repo.CreatePersonRelation(from);
                _repo.CreatePersonRelation(to);
            }
            else
            {
                var newPrl = new PersonRelationGroup() { RelationTypeId = type, Relations = { from, to } };
                string id = _repo.CreatePersonRelationGroup(newPrl);
                from.PersonRelationGroupId = id;
                to.PersonRelationGroupId = id;
                _repo.CreatePersonRelation(from);
                _repo.CreatePersonRelation(to);
            }

            return true;
        }

        #endregion

    }
}