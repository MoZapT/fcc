using FamilyControlCenter.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shared.Models;

namespace FamilyControlCenter.Viewmodels.Family
{
    public class PersonListViewModel : BaseViewModel
    {

        #region PROPERTIES

        public IEnumerable<Person> Models { get; set; }
        //public PersonRelationGroup RelationGroups { get; set; }
        //public IEnumerable<PersonName> Names { get; set; }

        #endregion

        #region METHODS

        public PersonListViewModel()
        {
            Initialize();
        }
        public override void Initialize()
        {
            Models = new List<Person>();

            base.Initialize();
        }

        #endregion

    }
}