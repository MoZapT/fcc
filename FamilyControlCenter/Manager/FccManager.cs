using Shared.Interfaces.Common;
using Shared.Interfaces.Repositories;
using DataAccessInfrastructure.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Models;
using FamilyControlCenter.Viewmodels.Family;
using FamilyControlCenter.Common;

namespace FamilyControlCenter.Manager
{
    public class FccManager
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
                case ActionCommand.List:
                    break;
                case ActionCommand.Create:
                    vm.Model = new Person();
                    vm.RelationGroups = new PersonRelationGroup();
                    vm.Names = new List<PersonName>();
                    return string.Empty;
                case ActionCommand.Edit:
                    vm.Model = _repo.ReadPerson(vm.Model.Id);
                    vm.RelationGroups = _repo.ReadPersonRelationGroup("");
                    vm.Names = _repo.ReadAllPersonName();
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
                case ActionCommand.Back:
                    break;
                default:
                    break;
            }

            /* List View */
            vm.Models = _repo.ReadAllPerson();

            return string.Empty;
        }
    }
}