using System.Collections.Generic;
using System.Web.Mvc;
using Shared.Common;
using Shared.Enums;
using Shared.Models;

namespace Shared.Viewmodels
{
    public class PersonViewModel : BaseViewModel
    {

        #region PROPERTIES

        public PersonPartialViewRelationsModel RelationsPartialViewModel {get; set; }

        public Person Model { get; set; }
        public List<Person> Models { get; set; }

        #endregion
    }
}