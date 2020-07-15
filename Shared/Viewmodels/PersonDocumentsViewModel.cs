using System.Collections.Generic;
using Shared.Common;
using Shared.Models;

namespace Shared.Viewmodels
{
    public class PersonDocumentsViewModel : BaseViewModel
    {

        #region PROPERTIES

        public Dictionary<string, IEnumerable<PersonDocument>> Documents { get; set; }
        public bool LoadCategories { get; set; }

        #endregion

        public PersonDocumentsViewModel()
        {
            Documents = new Dictionary<string, IEnumerable<PersonDocument>>();
        }

    }
}