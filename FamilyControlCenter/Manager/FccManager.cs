﻿using Shared.Interfaces.Repositories;
using DataAccessInfrastructure.Repositories;
using System.Collections.Generic;
using Shared.Models;
using Shared.Interfaces.Managers;
using Shared.Viewmodels.Family;
using Shared.Enums;
using System;
using System.Linq;
using System.Web.Mvc.Html;

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

        #region PersonViewModel

        public string HandleAction(PersonViewModel vm)
        {
            switch (vm.Command)
            {
                case ActionCommand.None:
                    break;
                case ActionCommand.Create:
                    return CreatePerson(vm);
                case ActionCommand.Edit:
                    return EditPerson(vm);
                case ActionCommand.Add:
                    _repo.CreatePerson(vm.Model);
                    break;
                case ActionCommand.Update:
                    UpdatePerson(vm);
                    break;
                case ActionCommand.Delete:
                    _repo.DeletePerson(vm.Model.Id);
                    break;
                default:
                    break;
            }

            /* List View */
            return ListPerson(vm);
        }
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

        private string CreatePerson(PersonViewModel vm)
        {
            vm.Model = new Person();
            vm.Names = new List<PersonName>();
            vm.Relations = new List<PersonRelationGroup>();
            vm.State = VmState.Detail;
            vm.PersonSelectionList = _repo.GetPersonSelectList().ToList();
            return string.Empty;
        }
        private string EditPerson(PersonViewModel vm)
        {
            vm.Model = _repo.ReadPerson(vm.Model.Id);
            vm.Names = _repo.ReadAllPersonNameByPersonId(vm.Model.Id).ToList();
            vm.Relations = _repo.ReadAllPersonRelationGroupsByPersonId(vm.Model.Id).ToList();
            vm.State = VmState.Detail;
            vm.PersonSelectionList = _repo.GetPersonSelectList().ToList();
            return string.Empty;
        }
        private bool UpdatePerson(PersonViewModel vm)
        {
            bool success = true;
            var old = _repo.ReadPerson(vm.Model.Id);
            PersonName pname = new PersonName
            {
                Name = old.Name,
                Lastname = old.Lastname,
                Patronym = old.Patronym,
                PersonId = old.Id
            };
            if (old.Name != vm.Model.Name || old.Lastname != vm.Model.Lastname || old.Patronym != vm.Model.Patronym)
            {
                success = !string.IsNullOrWhiteSpace(_repo.CreatePersonName(pname)) ? true : false;
            }

            if (!success)
                return success;

            success = _repo.UpdatePerson(vm.Model);
            if (!success)
                _repo.DeletePersonName(pname.Id);

            return success;
        }
        private string ListPerson(PersonViewModel vm)
        {
            vm.Command = ActionCommand.None;
            vm.Models = _repo.ReadAllPerson().ToList();
            vm.State = VmState.List;
            return string.Empty;
        }

        #endregion

    }
}