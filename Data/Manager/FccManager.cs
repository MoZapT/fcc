using DataAccessInfrastructure.Repositories;
using System.Collections.Generic;
using Shared.Models;
using Shared.Enums;
using System;
using System.Threading.Tasks;
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

        public async Task<string> SetPerson(Person entity)
        {
            return await _repo.CreatePerson(entity);
        }

        public async Task<bool> UpdatePerson(Person entity)
        {
            return _repo.Transaction(new Task(async () =>
            {
                await _repo.UpdatePerson(entity);
            }));
        }

        public async Task<bool> DeletePerson(string id)
        {
            return await _repo.DeletePerson(id);
        }

        public async Task<bool> ExistPerson(string id)
        {
            return await _repo.ReadPerson(id) != null;
        }

        public async Task<Person> GetPerson(string userId)
        {
            Person person = await _repo.ReadPerson(userId);
            if (person == null)
                return person;

            person.IsMarried = await _repo.IsMarried(userId);
            person.IsInPartnership = await _repo.IsInRelationship(userId);

            return person;
        }

        public async Task<IEnumerable<Person>> GetPersonByRelationType(string personId, RelationType type)
        {
            return await _repo.ReadAllPersonByRelation(personId, type);
        }

        public async Task<IEnumerable<Person>> GetListPerson()
        {
            var tasks =  (await _repo.ReadAllPerson())
                .Select(async e => 
                {
                    e.IsMarried = await _repo.IsMarried(e.Id);
                    e.IsInPartnership = await _repo.IsInRelationship(e.Id);
                    return e;
                });

            return await Task.WhenAll(tasks);
        }

        public async Task<IEnumerable<KeyValuePair<string, string>>> PersonTypeahead(string excludePersonId, string query)
        {
            return await _repo.GetPersonSelectList(excludePersonId, query);
        }

        public async Task<IEnumerable<KeyValuePair<string, string>>> PersonTypeaheadWithPossibilities(string excludePersonId, string query)
        {
            return await _repo.GetOnlyPossiblePersonSelectList(excludePersonId, query);
        }

        public async Task<int> PersonTypeaheadWithPossibilitiesCount(string excludePersonId)
        {
            return await _repo.GetOnlyPossiblePersonCount(excludePersonId);
        }

        public async Task<FileContent> GetMainPhotoByPersonId(string id)
        {
            return await _repo.ReadFileContentByPersonId(id);
        }
        public async Task<IEnumerable<FileContent>> GetAllPhotosByPersonId(string id)
        {
            return await _repo.ReadAllFileContentByPersonId(id);
        }
        public async Task<string> SetPersonPhoto(string personId, FileContent entity)
        {
            string result = "";

            bool success = _repo.Transaction(new Task(async () => 
            {
                result = await _repo.CreateFileContent(entity);
                await _repo.CreatePersonFileContent(personId, result);
            }));
            if (!success)
                return null;

            return result;
        }
        public async Task<bool> DeletePersonPhoto(string personId, string fileId)
        {
            var person = await _repo.ReadPerson(personId);

            return _repo.Transaction(new Task(async () => 
            {
                //if deleting main photo, try to set random avaible photo as main
                if (person.FileContentId == fileId)
                {
                    var files = (await _repo.ReadAllFileContentByPersonId(personId))
                        .Where(e => e.Id != fileId);

                    person.FileContentId = files.Any() ? files.FirstOrDefault().Id : null;
                    await _repo.UpdatePerson(person);
                }
                //continue delete photo content
                await _repo.DeletePersonFileContent(personId, fileId);
                await _repo.DeleteFileContent(fileId);
            }));
        }
        public async Task<bool> DeleteAllPersonPhotos(string personId)
        {
            return await _repo.DeleteAllPersonFileContent(personId);
        }

        public async Task<PersonDocument> GetDocumentByPersonId(string id)
        {
            return await _repo.ReadDocumentByPersonId(id);
        }
        public async Task<IEnumerable<PersonDocument>> GetAllDocumentsByPersonId(string id)
        {
            return await _repo.ReadAllDocumentByPersonId(id);
        }
        public async Task<IEnumerable<PersonDocument>> GetAllDocumentsByPersonIdAndCategory(string id, string category)
        {
            return await _repo.ReadAllDocumentByPersonIdAndCategory(id, category);
        }
        public async Task<string> SetPersonDocument(string personId, FileContent entity, string category, string activityId = null)
        {
            string result = "";

            bool success = _repo.Transaction(new Task(async () =>
            {
                result = await _repo.CreateFileContent(entity);
                await _repo.CreatePersonDocument(personId, result, category, activityId);
            }));
            if (!success)
                return null;

            return result;
        }
        public async Task<bool> DeletePersonDocument(string personId, string fileId)
        {
            return _repo.Transaction(new Task(async () =>
            {
                await _repo.DeletePersonDocument(personId, fileId);
                await _repo.DeleteFileContent(fileId);
            }));
        }
        public async Task<bool> DeleteAllPersonDocuments(string personId)
        {
            return await _repo.DeleteAllPersonDocument(personId);
        }

        public async Task<IEnumerable<string>> GetDocumentCategories(string query)
        {
            return await _repo.ReadAllDocumentCategories(query);
        }

        #endregion

        #region PersonName

        public async Task<string> GetCurrentPersonName(string personId)
        {
            return await _repo.ReadCurrentPersonName(personId);
        }
        public async Task<string> SetPersonName(PersonName entity)
        {
            return await _repo.CreatePersonName(entity);
        }

        public async Task<IEnumerable<PersonName>> GetAllPersonName(string personId)
        {
            return await _repo.ReadAllPersonNameByPersonId(personId);
        }

        public async Task<bool> DeletePersonName(string id)
        {
            return await _repo.DeletePersonName(id);
        }

        #endregion

        #region PersonRelation

        public async Task<IEnumerable<PersonRelation>> GetAllPersonRelationsBetweenPersons(string inviter, string invited)
        {
            var tasks = (await _repo.ReadAllPersonRelationBetweenInviterAndInvited(inviter, invited))
                .Select(async e =>
                {
                    e.Inviter = await _repo.ReadPerson(e.InviterId);
                    e.Invited = await _repo.ReadPerson(e.InvitedId);
                    return e;
                });

            return await Task.WhenAll(tasks);
        }

        public async Task<IEnumerable<PersonRelation>> GetAllPersonRelationsByInviterId(string personId)
        {
            var tasks =  (await _repo.ReadAllPersonRelationsByInviterId(personId))
                .Select(async e => 
                {
                    e.Inviter = await _repo.ReadPerson(e.InviterId);
                    e.Invited = await _repo.ReadPerson(e.InvitedId);
                    return e;
                });

            return await Task.WhenAll(tasks);
        }

        public async Task<bool> DeletePersonRelation(string inviter, string invited, RelationType type)
        {
            bool success = false;

            RelationType counterType = FccRelationTypeHelper.GetCounterRelationType(type);

            success = _repo.Transaction(new Task(async () => 
            {
                await _repo.DeletePersonRelation(inviter, invited, type);
                await _repo.DeletePersonRelation(invited, inviter, counterType);
            }));

            return success;
        }

        public async Task<bool> SetPersonRelation(string inviter, string invited, RelationType type)
        {
            return _repo.Transaction(new Task(async () =>
            {
                foreach (var relation in await GetUpdateRelationsStack(inviter, invited, type))
                {
                    await _repo.CreatePersonRelation(relation);
                }
            }));
        }
        
        private async Task<IEnumerable<PersonRelation>> GetUpdateRelationsStack(string inviter, string invited, RelationType type)
        {
            return new List<PersonRelation>
            {
                CreateRelation(inviter, invited, type),
                CreateRelation(invited, inviter, FccRelationTypeHelper.GetCounterRelationType(type))
            };
        }
        
        public async Task<IEnumerable<PersonRelation>> CreateRelationsMesh(string personId, string invitedId)
        {
            var tasks = (await _repo.ReadAllPersonRelationsThatArePossible(personId, invitedId))
                .Select(async e =>
                {
                    e.Inviter = await _repo.ReadPerson(e.InviterId);
                    e.Invited = await _repo.ReadPerson(e.InvitedId);
                    return e;
                });

            return await Task.WhenAll(tasks);
        }

        public async Task<IEnumerable<KeyValuePair<string, string>>> GetPersonsThatHaveRelativesWithPossibleRelations()
        {
            return await _repo.GetPersonsThatHaveRelativesWithPossibleRelations();
        }

        public async Task<bool> CheckIfSameRelationsAvaible(string personId)
        {
            return await _repo.CheckIfSameRelationsAvaible(personId);
        }

        public async Task<IEnumerable<KeyValuePair<string, string>>> GetPersonsKvpWithPossibleRelations(string personId)
        {
            return await _repo.GetPersonsKvpWithPossibleRelations(personId);
        }

        public async Task<IEnumerable<RelationType>> GetPersonsRelationTypes(string personId)
        {
            return await _repo.GetPersonsRelationTypes(personId);
        }

        private PersonRelation CreateRelation(string inviter, string invited, RelationType type)
        {
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

        public async Task<string> SetPersonBiography(PersonBiography entity)
        {
            return await _repo.CreatePersonBiography(entity);
        }
        public async Task<bool> UpdatePersonBiography(PersonBiography entity)
        {
            return await _repo.UpdatePersonBiography(entity);
        }
        public async Task<PersonBiography> GetPersonBiographyByPersonId(string person)
        {
            return await _repo.ReadPersonBiographyByPersonId(person);
        }

        #endregion

        #region PersonActivity

        public async Task<PersonActivity> GetPersonActivity(string id)
        {
            return await _repo.ReadPersonActivity(id);
        }

        public async Task<IEnumerable<PersonActivity>> GetAllPersonActivityByPerson(string id)
        {
            return await _repo.ReadAllPersonActivityByPerson(id);
        }

        public async Task<IEnumerable<PersonActivity>> GetAllPersonActivityByPerson(string id, ActivityType type)
        {
            return await _repo.ReadAllPersonActivityByPerson(id, type);
        }

        public async Task<string> SetPersonActivity(PersonActivity entity)
        {
            return await _repo.CreatePersonActivity(entity);
        }

        public async Task<bool> UpdatePersonActivity(PersonActivity entity)
        {
            return await _repo.UpdatePersonActivity(entity);
        }

        public async Task<bool> DeletePersonActivity(string id)
        {
            return await _repo.DeletePersonActivity(id);
        }

        #endregion

        #region FileContent

        public async Task<FileContent> GetFileContent(string id)
        {
            return await _repo.ReadFileContent(id);
        }

        public async Task<string> SetFileContent(FileContent entity)
        {
            return await _repo.CreateFileContent(entity);
        }

        public async Task<bool> UpdateFileContent(FileContent entity)
        {
            return await _repo.UpdateFileContent(entity);
        }

        public async Task<bool> DeleteFileContent(string id)
        {
            return await _repo.DeleteFileContent(id);
        }

        public async Task<bool> RenameCategory(string personId, string oldCategory, string newCategory)
        {
            return await _repo.RenameCategory(personId, oldCategory, newCategory);
        }

        public async Task<bool> MoveContentToAnotherCategory(string personId, string contentId, string category)
        {
            return await _repo.MoveContentToAnotherCategory(personId, contentId, category);
        }

        #endregion

    }
}