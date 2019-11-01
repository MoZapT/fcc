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
            string result = null;

            result = _repo.CreatePerson(entity);

            return result;
        }

        public bool UpdatePerson(Person entity)
        {
            return _repo.Transaction(new Task(() =>
            {
                _repo.UpdatePerson(entity);
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

        public IEnumerable<KeyValuePair<string, string>> PersonTypeaheadWithPossibilities(string excludePersonId, string query)
        {
            return _repo.GetOnlyPossiblePersonSelectList(excludePersonId, query);
        }

        public FileContent GetMainPhotoByPersonId(string id)
        {
            return _repo.ReadFileContentByPersonId(id);
        }
        public List<FileContent> GetAllPhotosByPersonId(string id)
        {
            return _repo.ReadAllFileContentByPersonId(id).ToList();
        }
        public string SetPersonFileContent(string personId, FileContent entity)
        {
            string result = "";

            bool success = _repo.Transaction(new Task(() => 
            {
                result = _repo.CreateFileContent(entity);
                _repo.CreatePersonFileContent(personId, result);
            }));
            if (!success)
                return null;

            return result;
        }
        public bool DeletePersonFileContent(string personId, string fileId)
        {
            var person = _repo.ReadPerson(personId);

            return _repo.Transaction(new Task(() => 
            {
                //if deleting main photo, try to set random avaible photo as main
                if (person.FileContentId == fileId)
                {
                    person.FileContentId = null;
                    List<FileContent> files = _repo.ReadAllFileContentByPersonId(personId).ToList() ?? 
                        new List<FileContent>();
                    if (files.Count > 0)
                    {
                        person.FileContentId = files.FirstOrDefault().Id;
                    }
                    _repo.UpdatePerson(person);
                }
                //continue delete photo content
                _repo.DeletePersonFileContent(personId, fileId);
                _repo.DeleteFileContent(fileId);
            }));
        }
        public bool DeleteAllPersonFileContent(string personId)
        {
            return _repo.DeleteAllPersonFileContent(personId);
        }

        public FileContent GetDocumentByPersonId(string id)
        {
            return _repo.ReadDocumentByPersonId(id);
        }
        public List<FileContent> GetAllDocumentByPersonId(string id)
        {
            return _repo.ReadAllDocumentByPersonId(id).ToList();
        }
        public string SetPersonDocument(string personId, FileContent entity)
        {
            string result = "";

            bool success = _repo.Transaction(new Task(() =>
            {
                result = _repo.CreateFileContent(entity);
                _repo.CreatePersonDocument(personId, result);
            }));
            if (!success)
                return null;

            return result;
        }
        public bool DeletePersonDocument(string personId, string fileId)
        {
            return _repo.Transaction(new Task(() =>
            {
                _repo.DeletePersonDocument(personId, fileId);
                _repo.DeleteFileContent(fileId);
            }));
        }
        public bool DeleteAllPersonDocument(string personId)
        {
            return _repo.DeleteAllPersonDocument(personId);
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

        public string SetPersonBiography(PersonBiography entity)
        {
            return _repo.CreatePersonBiography(entity);
        }
        public bool UpdatePersonBiography(PersonBiography entity)
        {
            return _repo.UpdatePersonBiography(entity);
        }
        public PersonBiography GetPersonBiographyByPersonId(string person)
        {
            return _repo.ReadPersonBiographyByPersonId(person);
        }

        #endregion

        #region PersonActivity

        public PersonActivity GetPersonActivity(string id)
        {
            return _repo.ReadPersonActivity(id);
        }

        public List<PersonActivity> GetAllPersonActivityByPerson(string id)
        {
            return _repo.ReadAllPersonActivityByPerson(id).ToList();
        }

        public List<PersonActivity> GetAllPersonActivityByPerson(string id, string type)
        {
            return _repo.ReadAllPersonActivityByPerson(id, type).ToList();
        }

        public string SetPersonActivity(PersonActivity entity)
        {
            return _repo.CreatePersonActivity(entity);
        }

        public bool UpdatePersonActivity(PersonActivity entity)
        {
            return _repo.UpdatePersonActivity(entity);
        }

        public bool DeletePersonActivity(string id)
        {
            return _repo.DeletePersonActivity(id);
        }

        #endregion

        #region FileContent

        public FileContent GetFileContent(string id)
        {
            return _repo.ReadFileContent(id);
        }

        public string SetFileContent(FileContent entity)
        {
            return _repo.CreateFileContent(entity);
        }

        public bool UpdateFileContent(FileContent entity)
        {
            return _repo.UpdateFileContent(entity);
        }

        public bool DeleteFileContent(string id)
        {
            return _repo.DeleteFileContent(id);
        }

        #endregion

    }
}