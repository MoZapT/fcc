﻿using Shared.Enums;
using Shared.Models;
using Shared.Viewmodels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shared.Interfaces.Managers
{
    public interface IFccManager
    {

        #region Person

        Task<string> SetPerson(Person entity);
        Task<bool> UpdatePerson(Person entity);
        Task<bool> DeletePerson(string id);
        Task<bool> ExistPerson(string id);
        Task<Person> GetPerson(string userId);
        Task<IEnumerable<Person>> GetListPerson();
        Task<IEnumerable<Person>> GetPersonByRelationType(string personId, RelationType type);
        Task<IEnumerable<KeyValuePair<string, string>>> PersonTypeahead(string excludePersonId, string query);
        Task<IEnumerable<KeyValuePair<string, string>>> PersonTypeaheadWithPossibilities(string excludePersonId, string query);
        Task<int> PersonTypeaheadWithPossibilitiesCount(string excludePersonId);
        Task<FileContent> GetMainPhotoByPersonId(string id);
        Task<IEnumerable<FileContent>> GetAllPhotosByPersonId(string id);
        Task<string> SetPersonPhoto(string personId, FileContent entity);
        Task<bool> DeletePersonPhoto(string fileId);
        Task<bool> DeleteAllPersonPhotos(string personId);

        Task<PersonDocument> GetDocumentByPersonId(string id);
        Task<IEnumerable<PersonDocument>> GetAllDocumentsByPersonId(string id);
        Task<IEnumerable<PersonDocument>> GetAllDocumentsByPersonIdAndActivity(string id, string activity);
        Task<string> SetPersonDocument(string personId, FileContent entity, string activityId = null);
        Task<bool> DeletePersonDocument(string fileId);
        Task<bool> DeleteAllPersonDocuments(string personId);

        Task<IEnumerable<ActivityType>> GetDocumentActivities(string personId);

        #endregion

        #region PersonName

        Task<string> GetCurrentPersonName(string personId);
        Task<IEnumerable<PersonName>> GetAllPersonName(string personId);
        Task<string> SetPersonName(PersonName entity);
        Task<bool> DeletePersonName(string id);

        #endregion

        #region PersonRelation

        Task<bool> CheckIfSameRelationsAvaible(string personId);
        Task<IEnumerable<KeyValuePair<string, string>>> GetPersonsThatHaveRelativesWithPossibleRelations();
        Task<IEnumerable<KeyValuePair<string, string>>> GetPersonsKvpWithPossibleRelations(string personId);
        Task<IEnumerable<PersonRelation>> CreateRelationsMesh(string personId, string invitedId);
        Task<IEnumerable<PersonRelation>> GetAllPersonRelationsBetweenPersons(string inviter, string invited);
        Task<IEnumerable<PersonRelation>> GetAllPersonRelationsByInviterId(string personId);
        Task<bool> DeletePersonRelation(string inviter, string invited, RelationType type);
        Task<bool> SetPersonRelation(string inviter, string invited, RelationType type);
        Task<IEnumerable<RelationType>> GetPersonsRelationTypes(string personId);

        #endregion

        #region PersonBiography

        Task<string> SetPersonBiography(PersonBiography entity);
        Task<bool> UpdatePersonBiography(PersonBiography entity);
        Task<PersonBiography> GetPersonBiographyByPersonId(string person);

        #endregion

        #region PersonActivity

        Task<PersonActivity> GetPersonActivity(string id);
        Task<IEnumerable<PersonActivity>> GetCategorizedPersonActivityByPerson(string id);
        Task<IEnumerable<PersonActivity>> GetAllPersonActivityByPerson(string id);
        Task<IEnumerable<PersonActivity>> GetAllPersonActivityByPerson(string id, ActivityType type);
        Task<string> SetPersonActivity(PersonActivity entity);
        Task<bool> UpdatePersonActivity(PersonActivity entity);
        Task<bool> DeletePersonActivity(string id);

        #endregion

        #region FileContent

        Task<FileContent> GetFileContent(string id);
        Task<string> SetFileContent(FileContent entity);
        Task<bool> UpdateFileContent(FileContent entity);
        Task<bool> DeleteFileContent(string id);

        #endregion

        Task<IEnumerable<ActivityDocumentsViewModel>> GetPersonDocuments(string id);
        Task<bool> DeletePersonDocuments(IEnumerable<string> docs);
        Task<bool> MovePersonDocumentsToAnotherCategory(IEnumerable<string> docs, string activity);

    }
}
