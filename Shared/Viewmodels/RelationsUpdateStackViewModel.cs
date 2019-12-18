using System.Collections.Generic;
using System.Web.Mvc;
using Shared.Common;
using Shared.Enums;
using Shared.Models;

namespace Shared.Viewmodels
{
    public class RelationsUpdateStackViewModel : BaseViewModel
    {
        public string PersonId { get; set; }
        public string PersonName { get; set; }
        public string SelectedPersonIdWithRelations { get; set; }
        public List<SelectListItem> PersonsWithPossibleRelations { get; set; }
        public List<PersonRelation> Relations { get; set; }

        public RelationsUpdateStackViewModel()
        {
            Relations = new List<PersonRelation>();
        }
    }
}