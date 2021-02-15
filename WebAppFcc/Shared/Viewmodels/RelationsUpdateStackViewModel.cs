using System.Collections.Generic;
using WebAppFcc.Shared.Common;
using WebAppFcc.Shared.Models;

namespace WebAppFcc.Shared.Viewmodels
{
    public class RelationsUpdateStackViewModel : BaseViewModel
    {
        public string PersonId { get; set; }
        public string SelectedPersonIdWithRelations { get; set; }
        public IEnumerable<KeyValuePair<string, string>> PersonList { get; set; }
        public IEnumerable<KeyValuePair<string, string>> PersonsWithPossibleRelations { get; set; }
        public IEnumerable<PersonRelation> InitialRelations { get; set; }

        public RelationsUpdateStackViewModel()
        {
            InitialRelations = new List<PersonRelation>();
        }
    }
}