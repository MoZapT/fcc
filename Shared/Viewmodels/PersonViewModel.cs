using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Shared.Common;
using Shared.Enums;
using Shared.Models;

namespace Shared.Viewmodels
{
    public class PersonViewModel : BaseViewModel
    {

        #region PROPERTIES

        public List<FileContent> Photos { get; set; }
        public List<PersonRelation> PersonRelations { get; set; }
        public List<PersonName> PersonNames { get; set; }

        public Person Model { get; set; }
        public Person MarriedOn { get; set; }
        public Person PartnerOf { get; set; }
        public List<Person> Models { get; set; }

        #endregion

        public PersonViewModel()
        {
            Paging = new PagingViewModel(Take);
        }
    }
}