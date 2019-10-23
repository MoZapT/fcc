using System.Collections.Generic;
using System.Web.Mvc;
using Shared.Common;
using Shared.Enums;
using Shared.Models;

namespace Shared.Viewmodels
{
    public class PersonPartialViewRelationsModel : BaseViewModel
    {

        #region PROPERTIES

        public Person Person { get; set; }
        public List<PersonRelation> Relations { get; set; }

        #endregion
    }
}