using System.Collections.Generic;
using System.Linq;
using WebAppFcc.Shared.Common;
using WebAppFcc.Shared.Enums;
using WebAppFcc.Shared.Models;

namespace WebAppFcc.Shared.Viewmodels
{
    public class PersonViewModel : BaseViewModel
    {

        #region PROPERTIES

        public IEnumerable<FileContent> Photos { get; set; }
        public IEnumerable<PersonRelation> PersonRelations { get; set; }
        public IEnumerable<PersonName> PersonNames { get; set; }

        public Person Model { get; set; }
        public Person MarriedOn { get; set; }
        public Person PartnerOf { get; set; }
        public IEnumerable<Person> Models { get; set; }
        public Dictionary<string, string> PersonIcons { get; set; }

        #endregion

        public PersonViewModel()
        {
            if (Take <= 0)
            {
                Take = 10;
            }
            Paging = new PagingViewModel(Take);
        }
    }
}