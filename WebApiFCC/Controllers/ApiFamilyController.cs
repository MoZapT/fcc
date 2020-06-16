using Shared.Helpers;
using Shared.Interfaces.Managers;
using Shared.Enums;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;

namespace WebApiFCC.Controllers
{
    [RoutePrefix("{lang}/api")]
    public class ApiFamilyController : ApiController
    {
        private readonly IFccManager _mgrFcc;

        public ApiFamilyController(IFccManager mgrFcc)
        {
            _mgrFcc = mgrFcc;
        }

        [HttpGet]
        [Route("personbiography/save/{personId}/{biographyText}")]
        public async Task<bool> SavePersonBiography(string personId, string biographyText)
        {
            bool success = false;

            var biography = await _mgrFcc.GetPersonBiographyByPersonId(personId);
            if(biography != null)
            {
                biography.BiographyText = biographyText;
                await _mgrFcc.UpdatePersonBiography(biography);
            }
            else
            {
                biography = new PersonBiography();
                biography.Id = Guid.NewGuid().ToString();
                biography.BiographyText = biographyText;
                biography.PersonId = personId;
                await _mgrFcc.SetPersonBiography(biography);
            }

            return success;
        }

        [HttpGet]
        [Route("personactivity/delete/{id}")]
        public async Task<bool> DeletePersonActivity(string id)
        {
            return await _mgrFcc.DeletePersonActivity(id);
        }

        [HttpGet]
        [Route("personname/set/{fName}/{lName}/{patronym}/{date}/{personId}")]
        public async Task<bool> SetPersonName(string fName, string lName, string patronym, string date, string personId)
        {
            if (string.IsNullOrWhiteSpace(fName) ||
                string.IsNullOrWhiteSpace(personId))
            {
                return false;
            }

            PersonName personName = new PersonName();
            personName.Id = Guid.NewGuid().ToString();
            personName.Firstname = fName;
            personName.Lastname = lName;
            personName.Patronym = patronym;
            personName.DateNameChanged = DateTime.Parse(date);
            personName.PersonId = personId;

            var person = await _mgrFcc.GetPerson(personId);
            bool hasNothingChanged = 
                (person.Firstname == fName && 
                person.Lastname == lName && 
                person.Patronym == patronym);

            if (hasNothingChanged)
                return false;

            var result = await _mgrFcc.SetPersonName(personName);
            if (string.IsNullOrWhiteSpace(result))
                return false;

            return true;
        }

        [HttpGet]
        [Route("personname/delete/{id}")]
        public async Task<bool> DeletePersonName(string id)
        {
            return await _mgrFcc.DeletePersonName(id);
        }

        [HttpGet]
        [Route("relation/set/{inviter}/{invited}/{type}")]
        public async Task SetPersonRelation(string inviter, string invited, RelationType type)
        {
            await _mgrFcc.SetPersonRelation(inviter, invited, type);
        }

        [HttpGet]
        [Route("relation/delete/{inviter}/{invited}/{type}")]
        public async Task DeletePersonRelation(string inviter, string invited, RelationType type)
        {
            await _mgrFcc.DeletePersonRelation(inviter, invited, type);
        }

        [HttpGet]
        [Route("typeahead/person/count/{excludePersonId}")]
        public async Task<int> GetRelationsCount(string excludePersonId)
        {
            try
            {
                return await _mgrFcc.PersonTypeaheadWithPossibilitiesCount(excludePersonId);
            }
            catch (Exception)
            {
                //TODO error exception!
                return 0;
            }
        }

        [HttpGet]
        [Route("relationtype/all/{inviter}/{invited}")]
        public async Task<IEnumerable<System.Web.Mvc.SelectListItem>> GetRelationTypes(string inviter, string invited)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(invited) || string.IsNullOrWhiteSpace(inviter))
                {
                    return new List<System.Web.Mvc.SelectListItem>();
                }

                var personInviter = await _mgrFcc.GetPerson(inviter);
                var personInvited = await _mgrFcc.GetPerson(invited);
                if (personInvited == null)
                    return new List<System.Web.Mvc.SelectListItem>();

                return await GetAvaibleRelationTypeSelects(personInviter, personInvited);
            }
            catch (Exception)
            {
                return new List<System.Web.Mvc.SelectListItem>();
            }
        }
        private async Task<IEnumerable<System.Web.Mvc.SelectListItem>> GetAvaibleRelationTypeSelects(Person inviter, Person invited)
        {
            var list = FccRelationTypeHelper.GetFamilySiblingsSelectGroup(invited.Sex);

            var isAlreadyRelated = 
                (await _mgrFcc.GetAllPersonRelationsBetweenPersons(inviter.Id, invited.Id) ?? 
                new List<PersonRelation>()).Any();

            if (isAlreadyRelated)
            {
                return new List<System.Web.Mvc.SelectListItem>();
            }

            if (invited.HasBirthDate && inviter.HasBirthDate)
            {
                list = list
                    .Where(e => 
                    {
                        if ((e.Value == RelationType.FatherMother.ToString() || 
                            e.Value == RelationType.GrandFatherMother.ToString()) &&
                            invited.BirthDate >= inviter.BirthDate)
                        {
                            return false;
                        }
                        else if ((e.Value == RelationType.SonDaughter.ToString() ||
                                 e.Value == RelationType.GrandSonDaughter.ToString()) &&
                                 invited.BirthDate <= inviter.BirthDate)
                        {
                            return false;
                        }

                        return true;
                    });
            }

            return list;
        }

        [HttpGet]
        [Route("typeahead/person/{excludePersonId?}/{query?}")]
        public async Task<IEnumerable<KeyValuePair<string, string>>> GetPersonTypeahead(string excludePersonId, string query = "")
        {
            try
            {
                return await _mgrFcc.PersonTypeaheadWithPossibilities(excludePersonId, query);
            }
            catch (Exception)
            {
                return new List<KeyValuePair<string, string>>();
            }
        }

        [HttpPost]
        [Route("person/photo/upload/{personId}")]
        public async Task<HttpResponseMessage> UploadPersonPhoto(string personId)
        {
            Person person = await _mgrFcc.GetPerson(personId);
            if (person == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            try
            {
                var provider = new MultipartMemoryStreamProvider();
                await Request.Content.ReadAsMultipartAsync(provider);

                foreach (var file in provider.Contents)
                {
                    if (!ContentTypeHelper.IsImage(file.Headers.ContentType.ToString()))
                    {
                        throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                    }

                    FileContent newFile = new FileContent();

                    var dataStream = await file.ReadAsStreamAsync();
                    var ctnLength = Convert.ToInt32(file.Headers.ContentLength);
                    byte[] buffer = new byte[ctnLength];
                    while (dataStream.Read(buffer, 0, ctnLength) > 0)
                    {
                        newFile.Id = Guid.NewGuid().ToString();
                        newFile.BinaryContent = buffer;
                        newFile.FileType = file.Headers.ContentType.ToString();
                        newFile.Name = file.Headers.ContentDisposition.FileName.Replace("\"", "");
                        newFile.DateModified = DateTime.Now;
                    }

                    string result = await _mgrFcc.SetPersonPhoto(personId, newFile);
                    if (string.IsNullOrWhiteSpace(result))
                    {
                        throw new HttpResponseException(HttpStatusCode.InternalServerError);
                    }

                    if (string.IsNullOrWhiteSpace(person.FileContentId))
                    {
                        person.FileContentId = result;
                        person.DateModified = DateTime.Now;
                        await _mgrFcc.UpdatePerson(person);
                    }
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        [HttpGet]
        [Route("person/photo/delete/{personId}/{fileId}")]
        public async Task DeletePersonPhoto(string personId, string fileId)
        {
            await _mgrFcc.DeletePersonPhoto(personId, fileId);
        }

        [HttpGet]
        [Route("person/photo/setmain/{personId}/{fileId}")]
        public async Task MainPersonPhoto(string personId, string fileId)
        {
            var person = await _mgrFcc.GetPerson(personId);
            person.FileContentId = fileId;
            await _mgrFcc.UpdatePerson(person);
        }

        //TODO create cdn, move to cdn!
        [Route("person/photo/get/{contentId}")]
        public async Task<string> GetPhotoAsBase64(string contentId)
        {
            var file = await _mgrFcc.GetFileContent(contentId);
            if (file?.BinaryContent == null)
                return string.Empty;
            string img64 = Convert.ToBase64String(file.BinaryContent);
            string img64Url = string.Format("data:image/" + file.FileType + ";base64,{0}", img64);
            return img64Url;
        }

        [HttpPost]
        [Route("person/file/upload/{personId}/{category}/{activityId?}")]
        public async Task<HttpResponseMessage> UploadPersonDocument(string personId, string category, string activityId = null)
        {
            Person person = await _mgrFcc.GetPerson(personId);
            if (person == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            try
            {
                var provider = new MultipartMemoryStreamProvider();
                await Request.Content.ReadAsMultipartAsync(provider);

                foreach (var file in provider.Contents)
                {
                    if (!ContentTypeHelper.IsSupported(file.Headers.ContentType.ToString()))
                    {
                        throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                    }

                    FileContent newFile = new FileContent();

                    var dataStream = await file.ReadAsStreamAsync();
                    var ctnLength = Convert.ToInt32(file.Headers.ContentLength);
                    byte[] buffer = new byte[ctnLength];
                    while (dataStream.Read(buffer, 0, ctnLength) > 0)
                    {
                        newFile.Id = Guid.NewGuid().ToString();
                        newFile.BinaryContent = buffer;
                        newFile.FileType = file.Headers.ContentType.ToString();
                        newFile.Name = file.Headers.ContentDisposition.FileName.Replace("\"", "");
                        newFile.DateModified = DateTime.Now;
                    }

                    string result = await _mgrFcc.SetPersonDocument(personId, newFile, category, activityId);
                    if (string.IsNullOrWhiteSpace(result))
                    {
                        throw new HttpResponseException(HttpStatusCode.InternalServerError);
                    }

                    if (string.IsNullOrWhiteSpace(person.FileContentId))
                    {
                        person.FileContentId = result;
                        person.DateModified = DateTime.Now;
                        await _mgrFcc.UpdatePerson(person);
                    }
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        [HttpGet]
        [Route("person/file/delete/{personId}/{fileId}")]
        public async Task DeletePersonDocument(string personId, string fileId)
        {
            await _mgrFcc.DeletePersonDocument(personId, fileId);
        }

        [HttpGet]
        [Route("document/categories/{query?}")]
        public async Task<IEnumerable<KeyValuePair<string, string>>> GetDocumentCategories(string query = null)
        {
            try
            {
                return (await _mgrFcc.GetDocumentCategories(query))
                    .Select(e => new KeyValuePair<string, string>(e, e));
            }
            catch (Exception)
            {
                return new List<KeyValuePair<string, string>>();
            }
        }

        [HttpGet]
        [Route("document/move/{personId}/{contentId}/{category}")]
        public async Task<bool> MoveDocumentToAnotherCategory(string personId, string contentId, string category)
        {
            try
            {
                return await _mgrFcc.MoveContentToAnotherCategory(personId, contentId, category);
            }
            catch (Exception)
            {
                return false;
            }
        }

        [HttpGet]
        [Route("document/move/{personId}/{oldCategory}/{newCategory}")]
        public async Task<bool> RenameCategory(string personId, string oldCategory, string newCategory)
        {
            try
            {
                return await _mgrFcc.RenameCategory(personId, oldCategory, newCategory);
            }
            catch (Exception)
            {
                return false;
            }
        }

        [HttpGet]
        [Route("person/personwithpossibilities")]
        public async Task<IEnumerable<KeyValuePair<string, string>>> GetPersonsWithRelationsWithPossibleRelations()
        {
            return await _mgrFcc.GetPersonsThatHaveRelativesWithPossibleRelations();
        }

        [HttpGet]
        [Route("person/possiblerelations/{personId}")]
        public async Task<IEnumerable<KeyValuePair<string, string>>> GetPersonsWithPossibleRelations(string personId)
        {
            return await _mgrFcc.GetPersonsKvpWithPossibleRelations(personId);
        }
    }
}