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
        List<PersonActivity> GetAllPersonActivityByPerson(string id, string type);
        PersonActivity SetPersonActivity(PersonActivity entity);
        PersonActivity UpdatePersonActivity(PersonActivity entity);
        PersonActivity DeletePersonActivity(string id);

        #endregion

        #region FileContent

        FileContent GetFileContent(string id);
        FileContent SetFileContent(FileContent entity);
        FileContent UpdateFileContent(FileContent entity);
        FileContent DeleteFileContent(string id);

        #endregion

    }
}
