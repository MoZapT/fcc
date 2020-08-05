using System.Collections.Generic;
using Shared.Common;
using Shared.Models;

namespace Shared.Viewmodels
{
    public class ActivityDocumentsViewModel : BaseViewModel
    {

        #region PROPERTIES

        public Dictionary<string, IEnumerable<PersonDocument>> Documents { get; set; }

        #endregion

        public ActivityDocumentsViewModel()
        {
            Documents = new Dictionary<string, IEnumerable<PersonDocument>>();
        }

    }
}