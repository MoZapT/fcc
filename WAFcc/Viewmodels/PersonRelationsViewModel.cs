using WAFcc.Enums;
using WAFcc.Models;

namespace WAFcc.Viewmodels
{
    public class PersonRelationsViewModel : BaseViewModel
    {

        #region PROPERTIES

        public Person Person { get; set; }
        public Dictionary<RelationType, IEnumerable<Person>> Relations { get; set; }
        public bool SameRelationsAvaible { get; set; }

        #endregion

        public PersonRelationsViewModel()
        {
            Relations = new Dictionary<RelationType, IEnumerable<Person>>();
        }
    }
}