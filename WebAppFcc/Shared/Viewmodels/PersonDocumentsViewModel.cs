using System.Collections.Generic;
using WebAppFcc.Shared.Common;
using WebAppFcc.Shared.Models;

namespace WebAppFcc.Shared.Viewmodels
{
    public class PersonDocumentsViewModel : BaseViewModel
    {

        #region PROPERTIES

        public Dictionary<string, IEnumerable<PersonDocument>> Documents { get; set; }

        #endregion

        public PersonDocumentsViewModel()
        {
            Documents = new Dictionary<string, IEnumerable<PersonDocument>>();
        }

    }
}