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

        public KeyValuePair<string, List<FileContent>> CreatePartialViewPersonPhotos(string personId)
        {
            var person = _mgrFcc.GetPerson(personId);
            return new KeyValuePair<string, List<FileContent>>(person.FileContentId, _mgrFcc.GetAllPhotosByPersonId(personId));
        }

        public PersonBiography CreatePartialViewPersonBiography(string personId)
        {
            return _mgrFcc.GetPersonBiographyByPersonId(personId);
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
                    vm.Command = ActionCommand.Cancel;
                    vm.Models = _mgrFcc.GetListPerson();
                    break;
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
                vm.PersonBiography = new PersonBiography();
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
            vm.PersonBiography = new PersonBiography();
            vm.PersonNames = new List<PersonName>();
            vm.PersonRelations = new List<PersonRelation>();
        }

        #endregion
    }
}