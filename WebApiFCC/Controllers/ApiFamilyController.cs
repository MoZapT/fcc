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

                var alreadyExistingRelationTypes = _mgrFcc
                    .ReadAllPersonRelationsBetweenPersons(inviter, invited)
                    .Select(e => new System.Web.Mvc.SelectListItem()
                    {
                        Text = rMgr.GetLocalizedStringForEnumValue(e.RelationType, personInvited.Sex),
                        Value = e.RelationType.ToString()
                    });

                return GetAvaibleRelationTypeSelects(personInvited)
                    .Where(e => alreadyExistingRelationTypes
                        .FirstOrDefault(i => i.Value == e.Value) == null);
            }
            catch (Exception)
            {
                return new List<System.Web.Mvc.SelectListItem>();
            }
        }
        private List<System.Web.Mvc.SelectListItem> GetAvaibleRelationTypeSelects(Person personInvited)
        {
            var rMgr = Resources.Resource.ResourceManager;

            return new List<System.Web.Mvc.SelectListItem>()
                {
                    new System.Web.Mvc.SelectListItem()
                    {
                        Value = ((int)RelationType.FatherMother).ToString(),
                        Text = rMgr.GetLocalizedStringForEnumValue(RelationType.FatherMother, personInvited.Sex)
                    },
                    new System.Web.Mvc.SelectListItem()
                    {
                        Value = ((int)RelationType.SonDaughter).ToString(),
                        Text = rMgr.GetLocalizedStringForEnumValue(RelationType.SonDaughter, personInvited.Sex)
                    },
                    new System.Web.Mvc.SelectListItem()
                    {
                        Value = ((int)RelationType.BrotherSister).ToString(),
                        Text = rMgr.GetLocalizedStringForEnumValue(RelationType.BrotherSister, personInvited.Sex)
                    },
                };
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