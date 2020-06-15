using Shared.Viewmodels;
using Shared.Models;
using System.Collections.Generic;
using Shared.Enums;

namespace Shared.Interfaces.Managers
{
    public interface IFccManager
    {

        #region Person

        string SetPerson(Person entity);
        bool UpdatePerson(Person entity);
        bool DeletePerson(string id);
        bool ExistPerson(string id);
        Person GetPerson(string userId);
        IEnumerable<Person> GetListPerson();
        IEnumerable<Person> GetPersonByRelationType(string personId, RelationType type);
        IEnumerable<KeyValuePair<string, string>> PersonTypeahead(string excludePersonId, string query);
        IEnumerable<KeyValuePair<string, string>> PersonTypeaheadWithPossibilities(string excludePersonId, string query);
        int PersonTypeaheadWithPossibilitiesCount(string excludePersonId);
        FileContent GetMainPhotoByPersonId(string id);
        IEnumerable<FileContent> GetAllPhotosByPersonId(string id);
        string SetPersonPhoto(string personId, FileContent entity);
        bool DeletePersonPhoto(string personId, string fileId);
        bool DeleteAllPersonPhotos(string personId);

        PersonDocument GetDocumentByPersonId(string id);
        IEnumerable<PersonDocument> GetAllDocumentsByPersonId(string id);
        IEnumerable<PersonDocument> GetAllDocumentsByPersonIdAndCategory(string id, string category);
        string SetPersonDocument(string personId, FileContent entity, string category, string activityId = null);
        bool DeletePersonDocument(string personId, string fileId);
        bool DeleteAllPersonDocuments(string personId);

        IEnumerable<string> GetDocumentCategories(string query);

        #endregion

        #region PersonName

        string GetCurrentPersonName(string personId);
        IEnumerable<PersonName> GetAllPersonName(string personId);
        string SetPersonName(PersonName entity);
        bool DeletePersonName(string id);

        #endregion

        #region PersonRelation

        bool CheckIfSameRelationsAvaible(string personId);
        IEnumerable<KeyValuePair<string, string>> GetPersonsThatHaveRelativesWithPossibleRelations();
        IEnumerable<KeyValuePair<string, string>> GetPersonsKvpWithPossibleRelations(string personId);
        IEnumerable<PersonRelation> CreateRelationsMesh(string personId, string invitedId);
        IEnumerable<PersonRelation> GetAllPersonRelationsBetweenPersons(string inviter, string invited);
        IEnumerable<PersonRelation> GetAllPersonRelationsByInviterId(string personId);
        bool DeletePersonRelation(string inviter, string invited, RelationType type);
        bool SetPersonRelation(string inviter, string invited, RelationType type);
        IEnumerable<RelationType> GetPersonsRelationTypes(string personId);

        #endregion

        #region PersonBiography

        string SetPersonBiography(PersonBiography entity);
        bool UpdatePersonBiography(PersonBiography entity);
        PersonBiography GetPersonBiographyByPersonId(string person);

        #endregion

        #region PersonActivity

        PersonActivity GetPersonActivity(string id);
        IEnumerable<PersonActivity> GetAllPersonActivityByPerson(string id);
        IEnumerable<PersonActivity> GetAllPersonActivityByPerson(string id, ActivityType type);
        string SetPersonActivity(PersonActivity entity);
        bool UpdatePersonActivity(PersonActivity entity);
        bool DeletePersonActivity(string id);

        #endregion

        #region FileContent

        FileContent GetFileContent(string id);
        string SetFileContent(FileContent entity);
        bool UpdateFileContent(FileContent entity);
        bool DeleteFileContent(string id);

        #endregion

    }
}
