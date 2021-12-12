using WAFcc.Models;

namespace WAFcc.Viewmodels
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