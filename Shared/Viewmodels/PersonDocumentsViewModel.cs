using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Shared.Common;
using Shared.Enums;
using Shared.Models;

namespace Shared.Viewmodels
{
    public class PersonDocumentsViewModel : BaseViewModel
    {

        #region PROPERTIES

        public Dictionary<string, List<PersonDocument>> Documents { get; set; }

        #endregion

        public PersonDocumentsViewModel()
        {
            Documents = new Dictionary<string, List<PersonDocument>>();
        }
    }
}