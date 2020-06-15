﻿using DataAccessInfrastructure.Repositories;
using System.Collections.Generic;
using Shared.Models;
using Shared.Enums;
using System;
using System.Linq;
using Shared.Interfaces.Managers;
using Shared.Interfaces.Repositories;
using System.Threading.Tasks;
using Shared.Helpers;

namespace Data.Manager
{
    public class FccManager : IFccManager
    {
        private readonly ISqlRepository _repo;

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
            return _repo.ReadPerson(id) != null;
        }

        public Person GetPerson(string userId)
        {
            Person person = _repo.ReadPerson(userId);
            if (person == null)
                return person;

            person.IsMarried = _repo.IsMarried(userId);
            person.IsInPartnership = _repo.IsInRelationship(userId);

            return person;
        }

        public IEnumerable<Person> GetPersonByRelationType(string personId, RelationType type)
        {
            return _repo.ReadAllPersonByRelation(personId, type).ToList();
        }

        public IEnumerable<Person> GetListPerson()
        {
            return _repo.ReadAllPerson()
                .Select(e => 
                {
                    e.IsMarried = _repo.IsMarried(e.Id);
                    e.IsInPartnership = _repo.IsInRelationship(e.Id);
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

        public int PersonTypeaheadWithPossibilitiesCount(string excludePersonId)
        {
            return _repo.GetOnlyPossiblePersonCount(excludePersonId);
        }

        public FileContent GetMainPhotoByPersonId(string id)
        {
            return _repo.ReadFileContentByPersonId(id);
        }
        public IEnumerable<FileContent> GetAllPhotosByPersonId(string id)
        {
            return _repo.ReadAllFileContentByPersonId(id).ToList();
        }
        public string SetPersonPhoto(string personId, FileContent entity)
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
        public bool DeletePersonPhoto(string personId, string fileId)
        {
            var person = _repo.ReadPerson(personId);

            return _repo.Transaction(new Task(() => 
            {
                //if deleting main photo, try to set random avaible photo as main
                if (person.FileContentId == fileId)
                {
                    var files = _repo.ReadAllFileContentByPersonId(personId)
                        .Where(e => e.Id != fileId);

                    person.FileContentId = files.Any() ? files.FirstOrDefault().Id : null;
                    _repo.UpdatePerson(person);
                }
                //continue delete photo content
                _repo.DeletePersonFileContent(personId, fileId);
                _repo.DeleteFileContent(fileId);
            }));
        }
        public bool DeleteAllPersonPhotos(string personId)
        {
            return _repo.DeleteAllPersonFileContent(personId);
        }

        public PersonDocument GetDocumentByPersonId(string id)
        {
            return _repo.ReadDocumentByPersonId(id);
        }
        public IEnumerable<PersonDocument> GetAllDocumentsByPersonId(string id)
        {
            return _repo.ReadAllDocumentByPersonId(id).ToList();
        }
        public IEnumerable<PersonDocument> GetAllDocumentsByPersonIdAndCategory(string id, string category)
        {
            return _repo.ReadAllDocumentByPersonIdAndCategory(id, category).ToList();
        }
        public string SetPersonDocument(string personId, FileContent entity, string category, string activityId = null)
        {
            string result = "";

            bool success = _repo.Transaction(new Task(() =>
            {
                result = _repo.CreateFileContent(entity);
                _repo.CreatePersonDocument(personId, result, category, activityId);
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
        public bool DeleteAllPersonDocuments(string personId)
        {
            return _repo.DeleteAllPersonDocument(personId);
        }

        public IEnumerable<string> GetDocumentCategories(string query)
        {
            return _repo.ReadAllDocumentCategories(query).ToList();
        }

        #endregion

        #region PersonName

        public string GetCurrentPersonName(string personId)
        {
            return _repo.ReadCurrentPersonName(personId);
        }
        public string SetPersonName(PersonName entity)
        {
            return _repo.CreatePersonName(entity);
        }

        public IEnumerable<PersonName> GetAllPersonName(string personId)
        {
            return _repo.ReadAllPersonNameByPersonId(personId).ToList();
        }

        public bool DeletePersonName(string id)
        {
            return _repo.DeletePersonName(id);
        }

        #endregion

        #region PersonRelation

        public IEnumerable<PersonRelation> GetAllPersonRelationsBetweenPersons(string inviter, string invited)
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

        public IEnumerable<PersonRelation> GetAllPersonRelationsByInviterId(string personId)
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
            return _repo.Transaction(new Task(() =>
            {
                foreach (var relation in GetUpdateRelationsStack(inviter, invited, type))
                {
                    _repo.CreatePersonRelation(relation);
                }
            }));
        }
        
        private IEnumerable<PersonRelation> GetUpdateRelationsStack(string inviter, string invited, RelationType type)
        {
            return new List<PersonRelation>
            {
                CreateRelation(inviter, invited, type),
                CreateRelation(invited, inviter, FccRelationTypeHelper.GetCounterRelationType(type))
            };
        }
        
        public IEnumerable<PersonRelation> CreateRelationsMesh(string personId, string invitedId)
        {
            return _repo.ReadAllPersonRelationsThatArePossible(personId, invitedId)
                .Select(e =>
                {
                    e.Inviter = _repo.ReadPerson(e.InviterId);
                    e.Invited = _repo.ReadPerson(e.InvitedId);
                    return e;
                })
                .ToList();
        }

        public IEnumerable<KeyValuePair<string, string>> GetPersonsThatHaveRelativesWithPossibleRelations()
        {
            return _repo.GetPersonsThatHaveRelativesWithPossibleRelations().ToList();
        }

        public bool CheckIfSameRelationsAvaible(string personId)
        {
            return _repo.CheckIfSameRelationsAvaible(personId);
        }

        public IEnumerable<KeyValuePair<string, string>> GetPersonsKvpWithPossibleRelations(string personId)
        {
            return _repo.GetPersonsKvpWithPossibleRelations(personId).ToList();
        }

        public IEnumerable<RelationType> GetPersonsRelationTypes(string personId)
        {
            return _repo.GetPersonsRelationTypes(personId).ToList();
        }

        private PersonRelation CreateRelation(string inviter, string invited, RelationType type)
        {
            //if (type == RelationType.HusbandWife)
            //{
            //    type = RelationType.InLawSiblings;
            //}

            return new PersonRelation
            {
                Id = Guid.NewGuid().ToString(),
                InviterId = inviter,
                InvitedId = invited,
                RelationType = type
            };
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

        public IEnumerable<PersonActivity> GetAllPersonActivityByPerson(string id)
        {
            return _repo.ReadAllPersonActivityByPerson(id).ToList();
        }

        public IEnumerable<PersonActivity> GetAllPersonActivityByPerson(string id, ActivityType type)
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