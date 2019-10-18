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
        [Route("typeahead/person/{excludePersonId?}/{query?}")]
        public void SetPersonRelation(PersonRelation from, PersonRelation to, RelationType type)
        {
            _mgrFcc.SetPersonRelation(from, to, type);
        }

        //[Authorize]
        [Route("typeahead/person/{excludePersonId?}/{query?}")]
        public void DeletePersonRelation(PersonRelation from, PersonRelation to, RelationType type)
        {
            throw new NotImplementedException();
        }

        //[Authorize]
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

        [Route("relationtype/all/{userId}")]
        public IEnumerable<System.Web.Mvc.SelectListItem> GetRelationTypes(string userId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userId))
                {
                    return new List<System.Web.Mvc.SelectListItem>();
                }

                var person = _mgrFcc.GetPerson(userId);
                if (person == null)
                    return new List<System.Web.Mvc.SelectListItem>();

                return FccEnumHelper
                    .GetTranslatedSelectListItemCollection<RelationType>
                        (typeof(RelationType), 
                        person.Sex);
            }
            catch (Exception)
            {
                return new List<System.Web.Mvc.SelectListItem>();
            }
        }
    }
}