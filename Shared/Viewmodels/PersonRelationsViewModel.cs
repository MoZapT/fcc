using System.Collections.Generic;
using Shared.Common;
using Shared.Enums;
using Shared.Models;

namespace Shared.Viewmodels
{
    public class PersonRelationsViewModel : BaseViewModel
    {

        #region PROPERTIES

        public Person Person { get; set; }
        public Dictionary<RelationType, List<Person>> Relations { get; set; }
        public bool SameRelationsAvaible { get; set; }

        #endregion

        public PersonRelationsViewModel()
        {
            Relations = new Dictionary<RelationType, List<Person>>();
        }
    }
}