using WAFcc.Models;

namespace WAFcc.Viewmodels
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