using System.Collections.Generic;
using Shared.Common;
using Shared.Models;

namespace Shared.Viewmodels
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