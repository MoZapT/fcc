using Shared.Enums;
using Shared.Models;
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
        Task<bool> DeletePersonPhoto(string personId, string fileId);
        Task<bool> DeleteAllPersonPhotos(string personId);

        Task<PersonDocument> GetDocumentByPersonId(string id);
        Task<IEnumerable<PersonDocument>> GetAllDocumentsByPersonId(string id);
        Task<IEnumerable<PersonDocument>> GetAllDocumentsByPersonIdAndCategory(string id, string category);
        Task<string> SetPersonDocument(string personId, FileContent entity, string category, string activityId = null);
        Task<bool> DeletePersonDocument(string personId, string fileId);
        Task<bool> DeleteAllPersonDocuments(string personId);

        Task<IEnumerable<string>> GetDocumentCategories(string query);

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
        Task<bool> RenameCategory(string personId, string oldCategory, string newCategory);
        Task<bool> MoveContentToAnotherCategory(string personId, string contentId, string category);

        #endregion

    }
}
