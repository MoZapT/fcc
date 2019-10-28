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

        public string SetPerson(Person entity, PersonBiography biography = null)
        {
            string result = null;

            _repo.Transaction(new Task(() => 
            {
                _repo.CreatePerson(entity);
                if (biography == null)
                    return;

                _repo.CreatePersonBiography(biography);
            }));

            return result;
        }

        public bool UpdatePerson(Person entity, PersonBiography biography = null)
        {
            return _repo.Transaction(new Task(() =>
            {
                _repo.UpdatePerson(entity);
                if (biography == null)
                    return;

                bool hasBiography = string.IsNullOrWhiteSpace(biography.Id) ? false : true;

                if (hasBiography)
                {
                    _repo.UpdatePersonBiography(biography);
                }
                else
                {
                    biography.Id = Guid.NewGuid().ToString();
                    _repo.CreatePersonBiography(biography);
                }
            }));
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
            Person person = _repo.ReadPerson(userId);
            person.IsMarried = _repo.IsMarried(userId);

            return person;
        }

        public List<Person> GetPersonByRelationType(string personId, RelationType type)
        {
            return _repo.ReadAllPersonByRelation(personId, type).ToList();
        }

        public List<Person> GetListPerson()
        {
            return _repo.ReadAllPerson()
                .Select(e => 
                {
                    e.IsMarried = _repo.IsMarried(e.Id);
                    return e;
                })
                .ToList();
        }

        public IEnumerable<KeyValuePair<string, string>> PersonTypeahead(string excludePersonId, string query)
        {
            return _repo.GetPersonSelectList(excludePersonId, query);
        }

        #endregion

        #region PersonName

        public string SetPersonName(PersonName entity)
        {
            return _repo.CreatePersonName(entity);
        }

        public List<PersonName> GetAllPersonName(string personId)
        {
            return _repo.ReadAllPersonNameByPersonId(personId).ToList();
        }

        public bool DeletePersonName(string id)
        {
            return _repo.DeletePersonName(id);
        }

        #endregion

        #region PersonRelation

        public List<PersonRelation> GetAllPersonRelationsBetweenPersons(string inviter, string invited)
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

        public List<PersonRelation> GetAllPersonRelationsByInviterId(string personId)
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