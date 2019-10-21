using DataAccessInfrastructure.Repositories;
using System.Collections.Generic;
using Shared.Models;
using Shared.Enums;
using System;
using System.Linq;
using System.Web.Mvc.Html;
using Shared.Viewmodels;
using Shared.Interfaces.ViewBuilders;
using Shared.Interfaces.Repositories;
using System.Threading.Tasks;
using Shared.Interfaces.Managers;

namespace Data.ViewBuilder
{
    public class FccViewBuilder : IFccViewBuilder
    {
        private IFccManager _mgrFcc;

        public FccViewBuilder(IFccManager mgrFcc)
        {
            _mgrFcc = mgrFcc;
        }

        #region PersonViewModel

        public void HandleAction(PersonViewModel vm)
        {
            vm.State = VmState.List;
            HandleCommand(vm);
            HandleState(vm);
        }

        private void HandleCommand(PersonViewModel vm)
        {
            switch (vm.Command)
            {
                case ActionCommand.Open:
                    vm.State = VmState.Detail;
                    break;
                case ActionCommand.New:
                    vm.State = VmState.Detail;
                    break;
                case ActionCommand.Save:
                    SavePerson(vm);
                    break;
                case ActionCommand.Delete:
                    _mgrFcc.DeletePersonRelation(vm.Model.Id);
                    break;
                case ActionCommand.Cancel:
                default:
                    break;
            }
        }

        private void HandleState(PersonViewModel vm)
        {
            switch (vm.State)
            {
                case VmState.Detail:
                    if (vm.Command == ActionCommand.Open)
                    {
                        GetEditPerson(vm);
                    }
                    else
                    {
                        CreateEmptyPerson(vm);
                    }

                    break;
                case VmState.List:
                default:
                    vm.Command = ActionCommand.Cancel;
                    vm.Models = _mgrFcc.GetListPerson();
                    break;
            }
        }

        private bool SavePerson(PersonViewModel vm)
        {
            bool success = true;

            //save person as new
            if (!_mgrFcc.ExistPerson(vm.Model.Id))
            {
                _mgrFcc.SetPerson(vm.Model);
            }

            //update already existing person
            success = _mgrFcc.UpdatePerson(vm.Model);

            return success;
        }

        private void GetEditPerson(PersonViewModel vm)
        {
            vm.Model = _mgrFcc.GetPerson(vm.Model.Id);
        }

        private void CreateEmptyPerson(PersonViewModel vm)
        {
            vm.Model = new Person() { Id = Guid.NewGuid().ToString() };
        }

        #endregion
    }
}