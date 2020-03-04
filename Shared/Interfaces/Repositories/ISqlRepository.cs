using Shared.Enums;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Interfaces.Repositories
{
    public interface ISqlRepository : ISqlBaseRepository
    {
        Person ReadPerson(string id);
        IEnumerable<Person> ReadAllPerson();
        IEnumerable<Person> ReadAllPersonByRelation(string personId, RelationType type);
        string CreatePerson(Person entity);
        bool UpdatePerson(Person entity);
        bool DeletePerson(string id);
        IEnumerable<KeyValuePair<string, string>> GetPersonSelectList();
        IEnumerable<KeyValuePair<string, string>> GetPersonSelectList(string excludePersonId, string search);
        IEnumerable<KeyValuePair<string, string>> GetOnlyPossiblePersonSelectList(string excludePersonId, string search);
        int GetOnlyPossiblePersonCount(string excludePersonId);

        string ReadCurrentPersonName(string personId);
        PersonName ReadLastPersonName(string id);
        IEnumerable<PersonName> ReadAllPersonNameByPersonId(string id);
        string CreatePersonName(PersonName entity);
        bool UpdatePersonName(PersonName entity);
        bool DeletePersonName(string id);

        FileContent ReadFileContentByPersonId(string id);
        IEnumerable<FileContent> ReadAllFileContentByPersonId(string id);
        string CreatePersonFileContent(string personId, string fileId);
        bool DeletePersonFileContent(string personId, string fileId);
        bool DeleteAllPersonFileContent(string personId);

        PersonDocument ReadDocumentByPersonId(string id);
        IEnumerable<PersonDocument> ReadAllDocumentByPersonId(string id);
        IEnumerable<PersonDocument> ReadAllDocumentByPersonIdAndCategory(string id, string category);
        string CreatePersonDocument(string personId, string fileId, string category, string activityId = null);
        bool DeletePersonDocument(string personId, string fileId);
        bool DeleteAllPersonDocument(string personId);

        IEnumerable<string> ReadAllDocumentCategories(string search);

        #region PersonRelation

        PersonRelation ReadPersonRelation(string inviter, string invited, RelationType type);
        IEnumerable<PersonRelation> ReadAllPersonRelationBetweenInviterAndInvited(string inviter, string invited);
        IEnumerable<PersonRelation> ReadAllPersonRelationsByInviterId(string id);
        IEnumerable<PersonRelation> ReadAllPersonRelationsThatArePossible(string personId, string inviterId);
        bool CheckIfSameRelationsAvaible(string personId);
        IEnumerable<KeyValuePair<string, string>> GetPersonsThatHaveRelativesWithPossibleRelations();
        IEnumerable<KeyValuePair<string, string>> GetPersonsKvpWithPossibleRelations(string personId);
        string CreatePersonRelation(PersonRelation entity);
        bool UpdatePersonRelation(PersonRelation entity);
        bool DeletePersonRelation(string inviter, string invited);
        bool DeletePersonRelation(string inviter, string invited, RelationType type);
        bool IsMarried(string personId);
        bool IsInRelationship(string personId);
        IEnumerable<RelationType> GetPersonsRelationTypes(string personId);

        #endregion

        #region PersonBiography

        PersonBiography ReadPersonBiographyByPersonId(string personId);
        PersonBiography ReadPersonBiography(string biographyId);
        string CreatePersonBiography(PersonBiography entity);
        bool UpdatePersonBiography(PersonBiography entity);

        #endregion

        #region PersonActivity

        PersonActivity ReadPersonActivity(string id);
        IEnumerable<PersonActivity> ReadAllPersonActivityByPerson(string id);
        IEnumerable<PersonActivity> ReadAllPersonActivityByPerson(string id, ActivityType type);
        string CreatePersonActivity(PersonActivity entity);
        bool UpdatePersonActivity(PersonActivity entity);
        bool DeletePersonActivity(string id);
        bool DeleteAllPersonActivityByPersonId(string id);

        #endregion

        #region FileContent

        FileContent ReadFileContent(string id);
        string CreateFileContent(FileContent entity);
        bool UpdateFileContent(FileContent entity);
        bool DeleteFileContent(string id);

        #endregion

    }
}
