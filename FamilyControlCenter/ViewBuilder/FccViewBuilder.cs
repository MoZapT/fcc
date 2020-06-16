using System.Collections.Generic;
using Shared.Models;
using Shared.Enums;
using System;
using System.Linq;
using Shared.Viewmodels;
using Shared.Interfaces.ViewBuilders;
using Shared.Interfaces.Managers;
using System.Threading.Tasks;

namespace FamilyControlCenter.ViewBuilder
{
    public class FccViewBuilder : IFccViewBuilder
    {
        private readonly IFccManager _mgrFcc;

        public FccViewBuilder(IFccManager mgrFcc)
        {
            _mgrFcc = mgrFcc;
        }

        #region PersonViewModel

        public async Task HandleAction(PersonViewModel vm)
        {
            vm.State = VmState.List;
            await HandleCommand(vm);
            await HandleState(vm);
        }

        public async Task<PersonDocumentsViewModel> CreatePartialViewPersonDocuments(string personId)
        {
            var vm = new PersonDocumentsViewModel();
            vm.Documents.Add(personId, await _mgrFcc.GetAllDocumentsByPersonId(personId));
            return vm;
        }

        public async Task<KeyValuePair<string, IEnumerable<FileContent>>> CreatePartialViewPersonPhotos(string personId)
        {
            var person = await _mgrFcc.GetPerson(personId);
            return new KeyValuePair<string, IEnumerable<FileContent>>(person.FileContentId, await _mgrFcc.GetAllPhotosByPersonId(personId));
        }

        public async Task<PersonBiographyViewModel> CreatePartialViewPersonBiography(string personId)
        {
            PersonBiographyViewModel biography = new PersonBiographyViewModel();
            var bio = await _mgrFcc.GetPersonBiographyByPersonId(personId);

            biography.PersonBiography = bio ??
                new PersonBiography() { Id = Guid.NewGuid().ToString() };

            if (bio == null)
            {
                return biography;
            }

            foreach (var activity in biography.ActivityTypeLoadingList)
            {
                biography.Activities.Add(activity, await _mgrFcc.GetAllPersonActivityByPerson(personId, activity));
            }

            return biography;
        }

        public async Task<bool> SavePersonActivity(string personId, string bioId, PersonActivity newact)
        {
            var bio = _mgrFcc.GetPersonBiographyByPersonId(personId);
            if (bio == null)
            {
                bioId = await _mgrFcc.SetPersonBiography(new PersonBiography()
                {
                    Id = bioId,
                    PersonId = personId,
                    BiographyText = null
                });
            }

            var dbRec = await _mgrFcc.GetPersonActivity(newact.Id);
            if (dbRec == null)
            {
                newact.Id = Guid.NewGuid().ToString();
                newact.IsActive = true;
                newact.DateCreated = DateTime.Now;
                newact.BiographyId = bioId;
                newact.DateModified = DateTime.Now;
                return !string.IsNullOrWhiteSpace(await _mgrFcc.SetPersonActivity(newact));
            }

            dbRec.DateBegin = newact.DateBegin;
            dbRec.HasBegun = newact.HasBegun;
            dbRec.HasEnded = newact.HasEnded;
            dbRec.DateEnd = newact.DateEnd;
            dbRec.Activity = newact.Activity;
            dbRec.ActivityType = newact.ActivityType;
            dbRec.DateModified = DateTime.Now;
            return await _mgrFcc.UpdatePersonActivity(dbRec);
        }

        public async Task<IEnumerable<PersonName>> CreatePartialViewForNamesAndPatronymList(string personId)
        {
            return await _mgrFcc.GetAllPersonName(personId);
        }

        public async Task<Tuple<Person, Person, Person>> CreatePartialViewForMarriageOrLivePartner(string personId, string spouseId, string partnerId)
        {
            var tuple = new Tuple<Person, Person, Person>
                (await _mgrFcc.GetPerson(personId), await _mgrFcc.GetPerson(spouseId), await _mgrFcc.GetPerson(partnerId));

            return tuple;
        }

        public async Task<PersonRelationsViewModel> CreatePersonPartialViewRelationsModel(string personId)
        {
            var vm = new PersonRelationsViewModel();
            vm.Person = await _mgrFcc.GetPerson(personId);
            vm.SameRelationsAvaible = await _mgrFcc.CheckIfSameRelationsAvaible(personId);

            foreach (var relationsType in await _mgrFcc.GetPersonsRelationTypes(personId))
            {
                vm.Relations.Add(relationsType, await _mgrFcc.GetPersonByRelationType(personId, relationsType));
            }

            return vm;
        }

        private async Task HandleCommand(PersonViewModel vm)
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
                    await SavePerson(vm);
                    vm.State = VmState.Detail;
                    vm.Command = ActionCommand.Open;
                    break;
                case ActionCommand.Delete:
                    await _mgrFcc.DeletePerson(vm.Model.Id);
                    break;
                case ActionCommand.Cancel:
                default:
                    break;
            }
        }

        private async Task HandleState(PersonViewModel vm)
        {
            switch (vm.State)
            {
                case VmState.Detail:
                    if (vm.Command == ActionCommand.Open)
                    {
                        await GetEditPerson(vm);
                    }
                    else
                    {
                        CreateEmptyPerson(vm);
                    }

                    break;
                case VmState.List:
                default:
                    await PersonList(vm);
                    break;
            }
        }
        
        private async Task PersonList(PersonViewModel vm)
        {
            vm.Command = ActionCommand.Cancel;
            vm.Models = await _mgrFcc.GetListPerson();
            int tcount = vm.Models.Count(); //TODO totalcount
            vm.Paging = new PagingViewModel(vm.Skip, vm.Take, tcount);
            vm.Models = vm.Models.Skip(vm.Skip).Take(vm.Take);
            vm.PersonIcons = new Dictionary<string, string>();

            foreach (Person p in vm.Models)
            {
                var file = await _mgrFcc.GetMainPhotoByPersonId(p.Id);
                if (file?.BinaryContent == null)
                    continue;
                string img64 = Convert.ToBase64String(file.BinaryContent);
                string img64Url = string.Format("data:image/" + file.FileType + ";base64,{0}", img64);
                vm.PersonIcons.Add(p.Id, img64Url);
            }
        }

        private async Task SavePerson(PersonViewModel vm)
        {
            try
            {
                //save person as new
                if (!await _mgrFcc.ExistPerson(vm.Model.Id))
                {
                    await _mgrFcc.SetPerson(vm.Model);
                }

                //update already existing person
                await _mgrFcc.UpdatePerson(vm.Model);
            }
            catch (Exception)
            {
                //TODO logging
            }
        }

        private async Task GetEditPerson(PersonViewModel vm)
        {
            try
            {
                vm.Model = await _mgrFcc.GetPerson(vm.Model.Id);
                vm.Photos = await _mgrFcc.GetAllPhotosByPersonId(vm.Model.Id);
                vm.MarriedOn = (await _mgrFcc.GetPersonByRelationType(vm.Model.Id, RelationType.HusbandWife)).FirstOrDefault();
                vm.PartnerOf = (await _mgrFcc.GetPersonByRelationType(vm.Model.Id, RelationType.LivePartner)).FirstOrDefault();
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

        #region RelationsUpdateStack

        public async Task<RelationsUpdateStackViewModel> CreateUpdateRelationsStackViewModel(string personId, string selectedId)
        {
            var vm = new RelationsUpdateStackViewModel();
            vm.PersonId = personId;
            vm.PersonList = await _mgrFcc.GetPersonsThatHaveRelativesWithPossibleRelations();
            vm.PersonsWithPossibleRelations = await _mgrFcc.GetPersonsKvpWithPossibleRelations(personId);
            vm.InitialRelations = await CreateRelationsUpdateStackPartial(personId, vm.PersonsWithPossibleRelations.FirstOrDefault().Key);

            return vm;
        }

        public async Task<IEnumerable<PersonRelation>> CreateRelationsUpdateStackPartial(string personId, string selectedId)
        {
            return await _mgrFcc.CreateRelationsMesh(personId, selectedId);
        }

        #endregion

        public async Task<PersonActivity> GetPersonActivity(string activityId)
        {
            return await _mgrFcc.GetPersonActivity(activityId);
        }
    }
}