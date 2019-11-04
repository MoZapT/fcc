﻿using DataAccessInfrastructure.Repositories;
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

        public List<FileContent> CreatePartialViewPersonDocuments(string personId)
        {
            List<FileContent> list = _mgrFcc.GetAllDocumentsByPersonId(personId);
            return list;
        }

        public KeyValuePair<string, List<FileContent>> CreatePartialViewPersonPhotos(string personId)
        {
            var person = _mgrFcc.GetPerson(personId);
            return new KeyValuePair<string, List<FileContent>>(person.FileContentId, _mgrFcc.GetAllPhotosByPersonId(personId));
        }

        public PersonBiographyViewModel CreatePartialViewPersonBiography(string personId)
        {
            PersonBiographyViewModel biography = new PersonBiographyViewModel();
            var bio = _mgrFcc.GetPersonBiographyByPersonId(personId);

            biography.PersonBiography = bio ??
                new PersonBiography() { Id = Guid.NewGuid().ToString() };

            if (bio == null)
            {
                return biography;
            }

            biography.Kindergarden = _mgrFcc.GetAllPersonActivityByPerson(personId, ActivityType.Kindergarden);
            biography.ElementarySchool = _mgrFcc.GetAllPersonActivityByPerson(personId, ActivityType.ElementarySchool);
            biography.MiddleSchool = _mgrFcc.GetAllPersonActivityByPerson(personId, ActivityType.MiddleSchool);
            biography.Highschool = _mgrFcc.GetAllPersonActivityByPerson(personId, ActivityType.Highschool);
            biography.Practice = _mgrFcc.GetAllPersonActivityByPerson(personId, ActivityType.Practice);
            biography.College = _mgrFcc.GetAllPersonActivityByPerson(personId, ActivityType.College);
            biography.TechnicalCollege = _mgrFcc.GetAllPersonActivityByPerson(personId, ActivityType.TechnicalCollege);
            biography.Trainee = _mgrFcc.GetAllPersonActivityByPerson(personId, ActivityType.Trainee);
            biography.University = _mgrFcc.GetAllPersonActivityByPerson(personId, ActivityType.University);
            biography.Unemployed = _mgrFcc.GetAllPersonActivityByPerson(personId, ActivityType.Unemployed);
            biography.Working = _mgrFcc.GetAllPersonActivityByPerson(personId, ActivityType.Working);
            biography.Enterpreneur = _mgrFcc.GetAllPersonActivityByPerson(personId, ActivityType.Enterpreneur);
            biography.Other = _mgrFcc.GetAllPersonActivityByPerson(personId, ActivityType.Other);

            return biography;
        }

        public bool SavePersonActivity(string personId, string bioId, PersonActivity newact)
        {
            bool success;
            var dbRec = _mgrFcc.GetPersonActivity(newact.Id);

            if (dbRec == null)
            {
                newact.Id = Guid.NewGuid().ToString();
                newact.IsActive = true;
                newact.DateCreated = DateTime.Now;
                newact.BiographyId = bioId;
                newact.DateModified = DateTime.Now;
                success = string.IsNullOrWhiteSpace(_mgrFcc.SetPersonActivity(newact)) ? false : true;
            }
            else
            {
                dbRec.DateBegin = newact.DateBegin;
                dbRec.DateEnd = newact.DateEnd;
                dbRec.Activity = newact.Activity;
                dbRec.ActivityType = newact.ActivityType;
                dbRec.DateModified = DateTime.Now;
                success = _mgrFcc.UpdatePersonActivity(dbRec);
            }

            return success;
        }

        public List<PersonName> CreatePartialViewForNamesAndPatronymList(string personId)
        {
            return _mgrFcc.GetAllPersonName(personId);
        }

        public KeyValuePair<Person, Person> CreatePartialViewForMarriageOrLivePartner(string personId, string spouseId)
        {
            var kvp = new KeyValuePair<Person, Person>(_mgrFcc.GetPerson(personId), _mgrFcc.GetPerson(spouseId));

            return kvp;
        }

        public List<PersonRelation> CreatePersonPartialViewRelationsModel(string personId)
        {
            return _mgrFcc.GetAllPersonRelationsByInviterId(personId);
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
                    vm.State = VmState.Detail;
                    vm.Command = ActionCommand.Open;
                    break;
                case ActionCommand.Delete:
                    _mgrFcc.DeletePerson(vm.Model.Id);
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
                    PersonList(vm);
                    break;
            }
        }
        
        private void PersonList(PersonViewModel vm)
        {
            vm.Command = ActionCommand.Cancel;
            vm.Models = _mgrFcc.GetListPerson();
            var tcount = vm.Models.Count();
            vm.Models = vm.Models.Skip(vm.Skip).Take(vm.Take).ToList();
            vm.Paging = new PagingViewModel(vm.Skip, vm.Take, tcount);
        }

        private bool SavePerson(PersonViewModel vm)
        {
            bool success = false;

            try
            {
                //save person as new
                if (!_mgrFcc.ExistPerson(vm.Model.Id))
                {
                    _mgrFcc.SetPerson(vm.Model);
                }

                //update already existing person
                success = _mgrFcc.UpdatePerson(vm.Model);

                return success;
            }
            catch (Exception)
            {
                return success;
            }
        }

        private void GetEditPerson(PersonViewModel vm)
        {
            try
            {
                vm.Model = _mgrFcc.GetPerson(vm.Model.Id);
                vm.Photos = _mgrFcc.GetAllPhotosByPersonId(vm.Model.Id);
                vm.MarriedOn = _mgrFcc.GetPersonByRelationType(vm.Model.Id, RelationType.HusbandWife).FirstOrDefault();
                vm.PersonNames = new List<PersonName>();
                vm.PersonRelations = new List<PersonRelation>();
            }
            catch (Exception)
            {
                vm.Model = new Person();
            }
        }

        private void CreateEmptyPerson(PersonViewModel vm)
        {
            vm.Model = new Person() { Id = Guid.NewGuid().ToString() };
            vm.PersonNames = new List<PersonName>();
            vm.PersonRelations = new List<PersonRelation>();
        }

        #endregion
    }
}