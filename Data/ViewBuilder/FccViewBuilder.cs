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
        private readonly IFccManager _mgrFcc;

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

        public PersonDocumentsViewModel CreatePartialViewPersonDocuments(string personId)
        {
            return new PersonDocumentsViewModel();
            //return _mgrFcc.GetAllDocumentsByPersonId(personId);
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

            foreach (var activity in biography.ActivityTypeLoadingList)
            {
                biography.Activities.Add(activity, _mgrFcc.GetAllPersonActivityByPerson(personId, activity));
            }

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

        public Tuple<Person, Person, Person> CreatePartialViewForMarriageOrLivePartner(string personId, string spouseId, string partnerId)
        {
            var tuple = new Tuple<Person, Person, Person>
                (_mgrFcc.GetPerson(personId), _mgrFcc.GetPerson(spouseId), _mgrFcc.GetPerson(partnerId));

            return tuple;
        }

        public PersonRelationsViewModel CreatePersonPartialViewRelationsModel(string personId)
        {
            var vm = new PersonRelationsViewModel();
            vm.Person = _mgrFcc.GetPerson(personId);
            foreach (var relationsType in vm.RelationTypeLoadingList)
            {
                var personList = _mgrFcc.GetPersonByRelationType(personId, relationsType);
                if (personList?.Count <= 0)
                    continue;

                vm.Relations.Add(relationsType, personList);
            }

            return vm;
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
            vm.PersonIcons = new Dictionary<string, string>();


            foreach (Person p in vm.Models)
            {
                var file = _mgrFcc.GetMainPhotoByPersonId(p.Id);
                if (file?.BinaryContent == null)
                    continue;
                string img64 = Convert.ToBase64String(file.BinaryContent);
                string img64Url = string.Format("data:image/" + file.FileType + ";base64,{0}", img64);
                vm.PersonIcons.Add(p.Id, img64Url);
            }
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
                vm.PartnerOf = _mgrFcc.GetPersonByRelationType(vm.Model.Id, RelationType.LivePartner).FirstOrDefault();
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