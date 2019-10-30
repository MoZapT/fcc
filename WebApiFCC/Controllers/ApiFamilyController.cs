using Shared.Helpers;
using Shared.Interfaces.Managers;
using Shared.Viewmodels;
using Shared.Enums;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web.Http;
using WebApiFCC;
using System.Web.Http.Cors;

namespace FamilyControlCenter.Controllers
{
    [RoutePrefix("{lang}/api")]
    public class ApiFamilyController : ApiController
    {
        IFccManager _mgrFcc;

        public ApiFamilyController()
        {
            _mgrFcc = ManagerCollection.Configuration.FccManager;
        }

        [HttpGet]
        [Route("personbiography/save/{personId}/{biographyText}")]
        public bool SavePersonBiography(string personId, string biographyText/*PersonBiography biography*/)
        {
            bool success = false;

            var biography = _mgrFcc.GetPersonBiographyByPersonId(personId);
            if(biography != null)
            {
                biography.BiographyText = biographyText;
                _mgrFcc.UpdatePersonBiography(biography);
            }
            else
            {
                biography = new PersonBiography();
                biography.Id = Guid.NewGuid().ToString();
                biography.BiographyText = biographyText;
                biography.PersonId = personId;
                _mgrFcc.SetPersonBiography(biography);
            }

            return success;
        }

        [HttpGet]
        [Route("personname/set/{fName}/{lName}/{patronym}/{date}/{personId}")]
        public bool SetPersonName(string fName, string lName, string patronym, string date, string personId)
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

            var person = _mgrFcc.GetPerson(personId);
            bool hasNothingChanged = 
                (person.Firstname == fName && 
                person.Lastname == lName && 
                person.Patronym == patronym) ? true : false;

            if (hasNothingChanged)
                return false;

            var result = _mgrFcc.SetPersonName(personName);
            if (string.IsNullOrWhiteSpace(result))
                return false;

            return true;
        }

        [HttpGet]
        [Route("personname/delete/{id}")]
        public bool DeletePersonName(string id)
        {
            return _mgrFcc.DeletePersonName(id);
        }

        [HttpGet]
        [Route("relation/set/{inviter}/{invited}/{type}")]
        public void SetPersonRelation(string inviter, string invited, RelationType type)
        {
            _mgrFcc.SetPersonRelation(inviter, invited, type);
        }

        [HttpGet]
        [Route("relation/delete/{inviter}/{invited}/{type}")]
        public void DeletePersonRelation(string inviter, string invited, RelationType type)
        {
            _mgrFcc.DeletePersonRelation(inviter, invited, type);
        }

        [HttpGet]
        [Route("relationtype/all/{inviter}/{invited}")]
        public IEnumerable<System.Web.Mvc.SelectListItem> GetRelationTypes(string inviter, string invited)
        {
            var rMgr = Resources.Resource.ResourceManager;

            try
            {
                if (string.IsNullOrWhiteSpace(invited) || string.IsNullOrWhiteSpace(inviter))
                {
                    return new List<System.Web.Mvc.SelectListItem>();
                }

                var personInviter = _mgrFcc.GetPerson(inviter);
                var personInvited = _mgrFcc.GetPerson(invited);
                if (personInvited == null)
                    return new List<System.Web.Mvc.SelectListItem>();

                return GetAvaibleRelationTypeSelects(personInviter, personInvited);
            }
            catch (Exception)
            {
                return new List<System.Web.Mvc.SelectListItem>();
            }
        }
        private IEnumerable<System.Web.Mvc.SelectListItem> GetAvaibleRelationTypeSelects(Person inviter, Person invited)
        {
            var list = FccRelationTypeHelper.GetFamilySiblingsSelectGroup(invited.Sex);

            var alreadyExistingRelationTypes = _mgrFcc
                .GetAllPersonRelationsBetweenPersons(inviter.Id, invited.Id)
                .Select(e => e.RelationType);

            var exclusionList = new List<RelationType>();
            foreach (var type in alreadyExistingRelationTypes)
            {
                exclusionList = exclusionList
                    .Concat(FccRelationTypeHelper.GetRelationTypeExclusion(type))
                    .ToList();
            }

            list = list
                .Where(e => !exclusionList.Contains((RelationType)int.Parse(e.Value)));

            if (invited.HasBirthDate && inviter.HasBirthDate)
            {
                list = list
                    .Where(e => !((RelationType)int.Parse(e.Value) == RelationType.FatherMother &&
                        invited.BirthDate >= inviter.BirthDate))
                    .Where(e => !((RelationType)int.Parse(e.Value) == RelationType.SonDaughter &&
                        invited.BirthDate <= inviter.BirthDate));
            }

            return list;
        }

        [HttpGet]
        [Route("typeahead/person/{excludePersonId?}/{query?}")]
        public IEnumerable<KeyValuePair<string, string>> GetPersonTypeahead(string excludePersonId, string query = "")
        {
            try
            {
                return _mgrFcc.PersonTypeaheadWithPossibilities(excludePersonId, query);
            }
            catch (Exception)
            {
                return new List<KeyValuePair<string, string>>();
            }
        }

        [HttpPost]
        [Route("person/photo/upload/{personId}")]
        public IEnumerable<KeyValuePair<string, string>> UploadPersonPhoto(string personId)
        {
            //            var httpCode = new HttpStatusCodeResult(HttpStatusCode.OK);
            var context = HttpContext;
            //            string prId = context.Request.Params["prId"];
            //#if DEBUG
            //            string fileDirectory = @"Z:\Intranet\PurchaseRequisition\" + prId + @"\";
            //#else
            //                    string fileDirectory = @"\\kdsv1606\Intranet\PurchaseRequisition\" + prId + @"\";
            //#endif

            //            if (!Directory.Exists(fileDirectory))
            //            {
            //                Directory.CreateDirectory(fileDirectory);
            //            }

            if (context.Request.Files.Count <= 0)
            {
                context.Response.Write("No file uploaded");
            }
            else
            {
                for (int i = 0; i < context.Request.Files.Count; ++i)
                {
                    HttpPostedFileBase file = context.Request.Files[i];
                    var size = file.ContentLength;
                    string filePath = fileDirectory + file.FileName;

                    if (context.Request.Form != null)
                    {
                        string imageid = context.Request.Form.ToString();
                        imageid = imageid.Substring(imageid.IndexOf('=') + 1);

                        if (file != null)
                        {
                            string ext = file.FileName.Substring(file.FileName.IndexOf('.'));
                            byte[] data;
                            using (Stream inputStream = file.InputStream)
                            {
                                MemoryStream memoryStream = inputStream as MemoryStream;
                                if (memoryStream == null)
                                {
                                    memoryStream = new MemoryStream();
                                    inputStream.CopyTo(memoryStream);
                                }
                                data = memoryStream.ToArray();

                                using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                                {
                                    fs.Write(data, 0, data.Length);
                                }
                            }
                            //if (ext.ToLower().Contains("gif") || ext.ToLower().Contains("jpg") || ext.ToLower().Contains("jpeg") || ext.ToLower().Contains("png"))
                            //{
                            //byte[] data;
                            //using (Stream inputStream = file.InputStream)
                            //{
                            //    MemoryStream memoryStream = inputStream as MemoryStream;
                            //    if (memoryStream == null)
                            //    {
                            //        memoryStream = new MemoryStream();
                            //        inputStream.CopyTo(memoryStream);
                            //    }
                            //    data = memoryStream.ToArray();

                            //    using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                            //    {
                            //        fs.Write(data, 0, data.Length);
                            //    }
                            //}
                            //}
                        }
                    }
                    else
                    {
                        context.Response.Write("Error: No file uploaded");
                    }

                    try
                    {
                        var prFile = new PurchaseRequisitionFileModel();
                        prFile.Id = Guid.NewGuid().ToString();
                        prFile.PurchaseRequisitionId = prId;
                        prFile.ContentType = file.ContentType;
                        prFile.Name = file.FileName;
                        prFile.Size = size;
                        prFile.Url = filePath;
                        prFile.DateCreated = DateTime.Now;
                        prFile.DateModified = DateTime.Now;
                        prFile.IsActive = true;

                        var dbFile = _mgrPr.GetPurchaseRequisitionFileByFileName(file.FileName);
                        if (string.IsNullOrWhiteSpace(dbFile.Id) || prFile.PurchaseRequisitionId != dbFile.PurchaseRequisitionId)
                        {
                            _mgrPr.SetPurchaseRequisitionFile(prFile);
                        }
                        else
                        {
                            prFile.Id = dbFile.Id;
                            _mgrPr.UpdatePurchaseRequisitionFile(prFile);
                        }

                        context.Response.Write("File uploaded");
                    }
                    catch (Exception)
                    {
                        context.Response.Write("Error: Exception occured while uploading!");
                    }
                }
            }

            return httpCode;
        }
    }
}