using System.Collections.Generic;
using Shared.Common;
using Shared.Models;

namespace Shared.Viewmodels.Family
{
    public class PersonViewModel : BaseViewModel
    {

        #region PROPERTIES

        public Person Model { get; set; }
        public List<PersonRelationGroup> Relations { get; set; }
        public List<PersonName> Names { get; set; }
        public List<KeyValuePair<string, string>> PersonSelectionList { get; set; }

        public List<Person> Models { get; set; }

        #endregion

        //#region METHODS

        //#endregion

    }
}