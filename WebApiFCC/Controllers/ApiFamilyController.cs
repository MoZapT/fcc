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

        //[Authorize]
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

        //[Authorize]
        [HttpGet]
        [Route("relation/set/{inviter}/{invited}/{type}")]
        public void SetPersonRelation(string inviter, string invited, RelationType type)
        {
            _mgrFcc.SetPersonRelation(inviter, invited, type);
        }

        //[Authorize]
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

                //var personInviter = _mgrFcc.GetPerson(invited);
                var personInvited = _mgrFcc.GetPerson(invited);
                if (personInvited == null)
                    return new List<System.Web.Mvc.SelectListItem>();

                return GetAvaibleRelationTypeSelects(inviter, personInvited);
            }
            catch (Exception)
            {
                return new List<System.Web.Mvc.SelectListItem>();
            }
        }
        private IEnumerable<System.Web.Mvc.SelectListItem> GetAvaibleRelationTypeSelects(string inviter, Person personInvited)
        {
            var list = FccRelationTypeHelper.GetFamilySiblingsSelectGroup(personInvited.Sex);

            var alreadyExistingRelationTypes = _mgrFcc
                .GetAllPersonRelationsBetweenPersons(inviter, personInvited.Id)
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

            return list;
        }

        //[Authorize]
        [HttpGet]
        [Route("typeahead/person/{excludePersonId?}/{query?}")]
        public IEnumerable<KeyValuePair<string, string>> GetPersonTypeahead(string excludePersonId, string query = "")
        {
            try
            {
                return _mgrFcc.PersonTypeahead(excludePersonId, query);
            }
            catch (Exception)
            {
                return new List<KeyValuePair<string, string>>();
            }
        }
    }
}