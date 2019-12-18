using System.Collections.Generic;
using Shared.Common;
using Shared.Models;

namespace Shared.Viewmodels
{
    public class RelationsUpdateStackViewModel : BaseViewModel
    {
        public string PersonId { get; set; }
        public string SelectedPersonIdWithRelations { get; set; }
        public List<KeyValuePair<string, string>> PersonList { get; set; }
        public List<KeyValuePair<string, string>> PersonsWithPossibleRelations { get; set; }
        public List<PersonRelation> InitialRelations { get; set; }

        public RelationsUpdateStackViewModel()
        {
            InitialRelations = new List<PersonRelation>();
        }
    }
}