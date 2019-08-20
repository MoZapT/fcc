using FamilyControlCenter.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shared.Models;

namespace FamilyControlCenter.Viewmodels.Family
{
    public class PersonViewModel : BaseViewModel
    {

        #region PROPERTIES

        public Person Model { get; set; }
        public PersonRelationGroup RelationGroups { get; set; }
        public IEnumerable<PersonName> Names { get; set; }

        #endregion

        #region METHODS

        public PersonViewModel()
        {
            Initialize();
        }
        public override void Initialize()
        {
            Model = new Person();

            base.Initialize();
        }

        #endregion

    }
}