using Shared.Interfaces.Common;
using Shared.Interfaces.Repositories;
using DataAccessInfrastructure.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Models;
using FamilyControlCenter.Viewmodels.Family;

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

        public async Task<PersonViewModel> GetPerson(string id)
        {
            var vm = new PersonViewModel();
            vm.Model = await _repo.ReadPerson(id);
            vm.RelationGroups = await _repo.ReadPersonRelationGroup(id);
            vm.Names = await _repo.ReadAllPersonName();

            return vm;
        }
        public async Task<PersonListViewModel> GetPersons()
        {
            var vm = new PersonListViewModel();
            vm.Models = await _repo.ReadAllPerson();
            //vm.Names = await _repo.ReadAllPersonName();
            //vm.RelationGroups = await _repo.ReadPersonRelationGroup();

            return vm;
        }
        public async Task<string> SetPerson(PersonViewModel model)
        {
            return await _repo.CreatePerson(model);
        }
        public async Task<bool> UpdatePerson(PersonViewModel model)
        {
            return await _repo.UpdatePerson(model);
        }
        public async Task<bool> DeletePerson(string id)
        {
            return await _repo.DeletePerson(id);
        }
    }
}