using Shared.Interfaces.Repositories;
using DataAccessInfrastructure.Repositories;
using System.Collections.Generic;
using Shared.Models;
using Shared.Interfaces.Managers;
using Shared.Viewmodels.Family;
using Shared.Common;

namespace FamilyControlCenter.Manager
{
    public class FccManager : IFccManager
    {
        private ISqlRepository _repo;

        public FccManager()
        {
            //_repo = new SqlRepository();
            _repo = new LocalRepository();
        }

        public string HandleAction(PersonViewModel vm)
        {
            switch (vm.Command)
            {
                case ActionCommand.None:
                    break;
                case ActionCommand.Create:
                    vm.Model = new Person();
                    vm.RelationGroups = new PersonRelationGroup();
                    vm.Names = new List<PersonName>();
                    vm.State = VmState.Detail;
                    return string.Empty;
                case ActionCommand.Edit:
                    vm.Model = _repo.ReadPerson(vm.Model.Id);
                    vm.RelationGroups = _repo.ReadPersonRelationGroup("");
                    vm.Names = _repo.ReadAllPersonName();
                    vm.State = VmState.Detail;
                    return string.Empty;
                case ActionCommand.Add:
                    _repo.CreatePerson(vm.Model);
                    break;
                case ActionCommand.Update:
                    _repo.UpdatePerson(vm.Model);
                    break;
                case ActionCommand.Delete:
                    _repo.DeletePerson(vm.Model.Id);
                    break;
                default:
                    break;
            }

            /* List View */
            vm.Command = ActionCommand.None;
            vm.Models = _repo.ReadAllPerson();
            vm.State = VmState.List;

            return string.Empty;
        }
    }
}