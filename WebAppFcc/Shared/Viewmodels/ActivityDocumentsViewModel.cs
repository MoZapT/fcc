using System.Collections.Generic;
using WebAppFcc.Shared.Common;
using WebAppFcc.Shared.Models;

namespace WebAppFcc.Shared.Viewmodels
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