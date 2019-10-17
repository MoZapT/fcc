using FamilyControlCenter.Helpers;
using FamilyControlCenter.Interfaces.Managers;
using FamilyControlCenter.Viewmodels.Family;
using Shared.Enums;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web.Http;

namespace FamilyControlCenter.Controllers
{
    [RoutePrefix("{lang}/family/api")]
    public class ApiFamilyController : BaseApiController
    {
        IFccManager _mgrFcc;

        public ApiFamilyController(IFccManager mgrFcc)
        {
            _mgrFcc = mgrFcc;
        }

        //[Authorize]
        [Route("typeahead/person/{query?}")]
        public IEnumerable<KeyValuePair<string, string>> GetPersonTypeahead(string query = "")
        {
            try
            {
                return _mgrFcc.PersonTypeahead(query);
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
                ChangeLanguage();

                return FccEnumHelper
                    .GetTranslatedSelectListItemCollection<RelationType>(typeof(RelationType), _mgrFcc.GetPerson(userId).Sex);
            }
            catch (Exception)
            {
                return new List<System.Web.Mvc.SelectListItem>();
            }
        }
    }
}