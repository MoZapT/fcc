using Shared.Enums;
using Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shared.Interfaces.Repositories
{
    public interface ISqlRepository : ISqlBaseRepository
    {
        Task<Person> ReadPerson(string id);
        Task<IEnumerable<Person>> ReadAllPerson();
        Task<IEnumerable<Person>> ReadAllPersonByRelation(string personId, RelationType type);
        Task<string> CreatePerson(Person entity);
        Task<bool> UpdatePerson(Person entity);
        Task<bool> DeletePerson(string id);
        Task<IEnumerable<KeyValuePair<string, string>>> GetPersonSelectList();
        Task<IEnumerable<KeyValuePair<string, string>>> GetPersonSelectList(string excludePersonId, string search);
        Task<IEnumerable<KeyValuePair<string, string>>> GetOnlyPossiblePersonSelectList(string excludePersonId, string search);
        Task<int> GetOnlyPossiblePersonCount(string excludePersonId);

        Task<string> ReadCurrentPersonName(string personId);
        Task<PersonName> ReadLastPersonName(string id);
        Task<IEnumerable<PersonName>> ReadAllPersonNameByPersonId(string id);
        Task<string> CreatePersonName(PersonName entity);
        Task<bool> UpdatePersonName(PersonName entity);
        Task<bool> DeletePersonName(string id);

        Task<FileContent> ReadPhotoByPersonId(string id);
        Task<IEnumerable<FileContent>> ReadAllPhotoByPersonId(string id);
        Task<string> CreatePersonPhoto(string personId, FileContent entity);
        Task<bool> DeleteAllPersonPhoto(string personId);

        Task<PersonDocument> ReadDocumentByPersonId(string id);
        Task<IEnumerable<PersonDocument>> ReadAllDocumentByPersonId(string id);
        Task<IEnumerable<PersonDocument>> ReadAllDocumentByPersonIdAndCategory(string id, string category);
        Task<IEnumerable<PersonDocument>> ReadAllDocumentByPersonIdAndActivity(string id, string activity);
        Task<IEnumerable<PersonDocument>> ReadAllDocumentByActivity(string activity);
        Task<string> CreatePersonDocument(FileContent content, string personId, string activityId = null);
        Task<bool> DeletePersonDocument(string personId, string fileId);
        Task<bool> DeleteAllPersonDocument(string personId);

        Task<IEnumerable<ActivityType>> ReadAllDocumentActivities(string personId);
        Task<bool> MovePersonDocumentToAnotherCategory(string personId, string contentId, string activityId = null);

        #region PersonRelation

        Task<PersonRelation> ReadPersonRelation(string inviter, string invited, RelationType type);
        Task<IEnumerable<PersonRelation>> ReadAllPersonRelationBetweenInviterAndInvited(string inviter, string invited);
        Task<IEnumerable<PersonRelation>> ReadAllPersonRelationsByInviterId(string id);
        Task<IEnumerable<PersonRelation>> ReadAllPersonRelationsThatArePossible(string personId, string inviterId);
        Task<bool> CheckIfSameRelationsAvaible(string personId);
        Task<IEnumerable<KeyValuePair<string, string>>> GetPersonsThatHaveRelativesWithPossibleRelations();
        Task<IEnumerable<KeyValuePair<string, string>>> GetPersonsKvpWithPossibleRelations(string personId);
        Task<string> CreatePersonRelation(PersonRelation entity);
        Task<bool> UpdatePersonRelation(PersonRelation entity);
        Task<bool> DeletePersonRelation(string inviter, string invited);
        Task<bool> DeletePersonRelation(string inviter, string invited, RelationType type, RelationType ctrType);
        Task<bool> IsMarried(string personId);
        Task<bool> IsInRelationship(string personId);
        Task<IEnumerable<RelationType>> GetPersonsRelationTypes(string personId);

        #endregion

        #region PersonBiography

        Task<PersonBiography> ReadPersonBiographyByPersonId(string personId);
        Task<PersonBiography> ReadPersonBiography(string biographyId);
        Task<string> CreatePersonBiography(PersonBiography entity);
        Task<bool> UpdatePersonBiography(PersonBiography entity);

        #endregion

        #region PersonActivity

        Task<PersonActivity> ReadPersonActivity(string id);
        Task<IEnumerable<PersonActivity>> ReadCategorizedPersonActivityByPerson(string id);
        Task<IEnumerable<PersonActivity>> ReadAllPersonActivityByPerson(string id);
        Task<IEnumerable<PersonActivity>> ReadAllPersonActivityByPerson(string id, ActivityType type);
        Task<string> CreatePersonActivity(PersonActivity entity);
        Task<bool> UpdatePersonActivity(PersonActivity entity);
        Task<bool> DeletePersonActivity(string id);
        Task<bool> DeleteAllPersonActivityByPersonId(string id);

        #endregion

        #region FileContent

        Task<FileContent> ReadFileContent(string id);
        Task<string> CreateFileContent(FileContent entity);
        Task<bool> UpdateFileContent(FileContent entity);
        Task<bool> DeleteFileContent(string id);
 
        #endregion

    }
}
