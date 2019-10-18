using DataAccessInfrastructure.Repositories;
using System.Collections.Generic;
using Shared.Models;
using Shared.Enums;
using System;
using System.Linq;
using System.Web.Mvc.Html;
using Shared.Viewmodels;
using Shared.Interfaces.Managers;
using Shared.Interfaces.Repositories;
using System.Threading.Tasks;

namespace Data.Manager
{
    public class FccManager : IFccManager
    {
        private ISqlRepository _repo;

        public FccManager()
        {
            _repo = new SqlRepository();
            //_repo = new LocalRepository();
        }

        #region PersonViewModel

        public void HandleAction(PersonViewModel vm)
        {
            vm.State = VmState.List;

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
                    DeletePerson(vm);
                    break;
                case ActionCommand.Cancel:
                default:
                    break;
            }

            switch (vm.State)
            {
                case VmState.Detail:
                    if (vm.Command == ActionCommand.Open)
                    {
                        var personName = _repo.ReadLastPersonName(vm.Model.Id);
                        vm.Model = _repo.ReadPerson(vm.Model.Id);
                        vm.Names = _repo.ReadAllPersonNameByPersonId(vm.Model.Id).ToList();
                        vm.Relations = _repo.ReadAllPersonRelationGroupsByPersonId(vm.Model.Id).ToList();
                    }
                    else
                    {
                        vm.Model = new Person() { Id = Guid.NewGuid().ToString() };
                        vm.Names = new List<PersonName>();
                        vm.Relations = new List<PersonRelationGroup>();
                    }

                    break;
                case VmState.List:
                default:
                    vm.Command = ActionCommand.Cancel;
                    vm.Models = _repo.ReadAllPerson().ToList();
                    break;
            }
        }

        private bool SavePerson(PersonViewModel vm)
        {
            bool success = true;
            bool isExisting = _repo.ReadPerson(vm.Model.Id) != null ? true : false;

            //save person as new
            if (!isExisting)
            {
                string result = _repo.CreatePerson(vm.Model);
                success = string.IsNullOrWhiteSpace(result) ? false : true;

                return success;
            }

            //update already existing person
            success = _repo.UpdatePerson(vm.Model);

            return success;
        }
        private bool DeletePerson(PersonViewModel vm)
        {
            bool success = true;

            success = _repo.DeletePerson(vm.Model.Id);

            return success;
        }

        #endregion

        public bool DeletePersonRelation(string id)
        {
            return _repo.DeletePersonRelation(id);
        }

        public string SetPersonRelations(PersonRelation entity)
        {
            entity.Id = Guid.NewGuid().ToString();
            entity.DateCreated = DateTime.Now;
            entity.DateModified = DateTime.Now;
            return _repo.CreatePersonRelation(entity);
        }

        public IEnumerable<PersonRelationGroup> GetPersonRelationGroupsByPersonId(string id)
        {
            return _repo.ReadAllPersonRelationGroupsByPersonId(id);
        }

        public IEnumerable<KeyValuePair<string, string>> PersonTypeahead(string excludePersonId, string query)
        {
            return _repo.GetPersonSelectList(excludePersonId, query);
        }

        public Person GetPerson(string userId)
        {
            return _repo.ReadPerson(userId);
        }
    }
}