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
        List<Person> GetListPerson();
        List<Person> GetPersonByRelationType(string personId, RelationType type);
        IEnumerable<KeyValuePair<string, string>> PersonTypeahead(string excludePersonId, string query);
        IEnumerable<KeyValuePair<string, string>> PersonTypeaheadWithPossibilities(string excludePersonId, string query);
        int PersonTypeaheadWithPossibilitiesCount(string excludePersonId);
        FileContent GetMainPhotoByPersonId(string id);
        List<FileContent> GetAllPhotosByPersonId(string id);
        string SetPersonPhoto(string personId, FileContent entity);
        bool DeletePersonPhoto(string personId, string fileId);
        bool DeleteAllPersonPhotos(string personId);

        PersonDocument GetDocumentByPersonId(string id);
        List<PersonDocument> GetAllDocumentsByPersonId(string id);
        List<PersonDocument> GetAllDocumentsByPersonIdAndCategory(string id, string category);
        string SetPersonDocument(string personId, FileContent entity, string category, string activityId = null);
        bool DeletePersonDocument(string personId, string fileId);
        bool DeleteAllPersonDocuments(string personId);

        List<string> GetDocumentCategories(string query);

        #endregion

        #region PersonName

        string GetCurrentPersonName(string personId);
        List<PersonName> GetAllPersonName(string personId);
        string SetPersonName(PersonName entity);
        bool DeletePersonName(string id);

        #endregion

        #region PersonRelation

        bool CheckIfSameRelationsAvaible(string personId);
        List<KeyValuePair<string, string>> GetPersonsThatHaveRelativesWithPossibleRelations();
        List<KeyValuePair<string, string>> GetPersonsKvpWithPossibleRelations(string personId);
        List<PersonRelation> CreateRelationsMesh(string personId, string invitedId);
        List<PersonRelation> GetAllPersonRelationsBetweenPersons(string inviter, string invited);
        List<PersonRelation> GetAllPersonRelationsByInviterId(string personId);
        bool DeletePersonRelation(string inviter, string invited, RelationType type);
        bool SetPersonRelation(string inviter, string invited, RelationType type);
        List<RelationType> GetPersonsRelationTypes(string personId);

        #endregion

        #region PersonBiography

        string SetPersonBiography(PersonBiography entity);
        bool UpdatePersonBiography(PersonBiography entity);
        PersonBiography GetPersonBiographyByPersonId(string person);

        #endregion

        #region PersonActivity

        PersonActivity GetPersonActivity(string id);
        List<PersonActivity> GetAllPersonActivityByPerson(string id);
        List<PersonActivity> GetAllPersonActivityByPerson(string id, ActivityType type);
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
