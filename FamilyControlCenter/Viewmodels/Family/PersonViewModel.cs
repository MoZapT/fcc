using System.Collections.Generic;
using System.Web.Mvc;
using Shared.Common;
using Shared.Enums;
using Shared.Models;

namespace FamilyControlCenter.Viewmodels.Family
{
    public class PersonViewModel : BaseViewModel
    {

        #region PROPERTIES

        public Person Model { get; set; }
        public List<PersonRelationGroup> Relations { get; set; }
        public List<PersonName> Names { get; set; }

        public List<Person> Models { get; set; }

        #endregion
    }
}