using System.Collections.Generic;
using Shared.Common;
using Shared.Models;

namespace Shared.Viewmodels
{
    public class ActivityDocumentsViewModel : BaseViewModel
    {

        #region PROPERTIES

        public PersonActivity Activity { get; set; }
        public IEnumerable<PersonDocument> Documents { get; set; }

        #endregion

        public ActivityDocumentsViewModel()
        {
            Documents = new List<PersonDocument>();
        }

    }
}