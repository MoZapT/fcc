using System.Collections.Generic;
using WebAppFcc.Shared.Common;
using WebAppFcc.Shared.Enums;
using WebAppFcc.Shared.Models;

namespace WebAppFcc.Shared.Viewmodels
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