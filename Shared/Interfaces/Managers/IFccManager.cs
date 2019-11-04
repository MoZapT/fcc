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
        FileContent GetMainPhotoByPersonId(string id);
        List<FileContent> GetAllPhotosByPersonId(string id);
        string SetPersonPhoto(string personId, FileContent entity);
        bool DeletePersonPhoto(string personId, string fileId);
        bool DeleteAllPersonPhotos(string personId);

        FileContent GetDocumentByPersonId(string id);
        List<FileContent> GetAllDocumentsByPersonId(string id);
        string SetPersonDocument(string personId, FileContent entity);
        bool DeletePersonDocument(string personId, string fileId);
        bool DeleteAllPersonDocuments(string personId);

        #endregion

        #region PersonName

        List<PersonName> GetAllPersonName(string personId);
        string SetPersonName(PersonName entity);
        bool DeletePersonName(string id);

        #endregion

        #region PersonRelation

        List<PersonRelation> GetAllPersonRelationsBetweenPersons(string inviter, string invited);
        List<PersonRelation> GetAllPersonRelationsByInviterId(string personId);
        bool DeletePersonRelation(string inviter, string invited, RelationType type);
        bool SetPersonRelation(string inviter, string invited, RelationType type);

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
