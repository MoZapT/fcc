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
using Shared.Helpers;

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
            return _repo.CreatePerson(entity);
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

        public List<PersonRelation> ReadAllPersonRelationsBetweenPersons(string inviter, string invited)
        {
            return _repo.ReadAllPersonRelationBetweenInviterAndInvited(inviter, invited)
                .Select(e =>
                {
                    e.Inviter = _repo.ReadPerson(e.InviterId);
                    e.Invited = _repo.ReadPerson(e.InvitedId);
                    return e;
                })
                .ToList();
        }

        public List<PersonRelation> ReadAllPersonRelationsByInviterId(string personId)
        {
            return _repo.ReadAllPersonRelationsByInviterId(personId)
                .Select(e => 
                {
                    e.Inviter = _repo.ReadPerson(e.InviterId);
                    e.Invited = _repo.ReadPerson(e.InvitedId);
                    return e;
                })
                .ToList();
        }

        public bool DeletePersonRelation(string inviter, string invited, RelationType type)
        {
            bool success = false;

            RelationType counterType = FccRelationTypeHelper.GetCounterRelationType(type);

            success = _repo.Transaction(new Task(() => 
            {
                _repo.DeletePersonRelation(inviter, invited, type);
                _repo.DeletePersonRelation(invited, inviter, counterType);
            }));

            return success;
        }

        public bool SetPersonRelation(string inviter, string invited, RelationType type)
        {
            PersonRelation inviterRelation = new PersonRelation();
            inviterRelation.Id = Guid.NewGuid().ToString();
            inviterRelation.InviterId = inviter;
            inviterRelation.InvitedId = invited;
            inviterRelation.RelationType = type;

            PersonRelation invitedRelation = new PersonRelation();
            invitedRelation.Id = Guid.NewGuid().ToString();
            invitedRelation.InviterId = invited;
            invitedRelation.InvitedId = inviter;
            invitedRelation.RelationType = FccRelationTypeHelper.GetCounterRelationType(type);

            bool success = _repo.Transaction(new Task(() => 
            {
                _repo.CreatePersonRelation(inviterRelation);
                _repo.CreatePersonRelation(invitedRelation);
            }));

            return success;
        }

        #endregion

        #region PersonBiography

        public PersonBiography GetPersonBiographyByPersonId(string person)
        {
            return _repo.ReadPersonBiographyByPersonId(person);
        }

        #endregion

    }
}