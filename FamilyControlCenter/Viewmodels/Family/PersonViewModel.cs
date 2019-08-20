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

        public new Person Model { get; set; }
        public PersonRelationGroup RelationGroups { get; set; }
        public IEnumerable<PersonName> Names { get; set; }
        public bool BornTimeKnown { get; set; }
        public bool DeadTimeKnown { get; set; }

        #endregion

        #region METHODS

        public PersonViewModel()
        {
            Initialize();
        }
        public override void Initialize()
        {
            Model = new Person();
            Model.Id = Guid.NewGuid().ToString();

            base.Initialize();
        }
        public override void HandleAction()
        {
            base.HandleAction();
        }

        private void Save()
        {
            if (string.IsNullOrWhiteSpace(Model.Id))
            {
                MgrFcc.UpdatePerson(Model);
            }
            else
            {
                MgrFcc.SetPerson(Model);
            }
        }
        private void Add()
        {
            Model = new Person();
        }
        private void Edit()
        {
            Model = MgrFcc.GetPerson(Model.Id).Result;
        }
        private void Delete()
        {
            MgrFcc.DeletePerson(Model.Id);
        }

        #endregion

    }
}