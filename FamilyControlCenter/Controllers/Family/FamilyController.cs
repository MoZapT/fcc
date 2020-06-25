using Shared.Viewmodels;
using Shared.Models;
using System.Web.Mvc;
using Shared.Enums;
using Shared.Interfaces.ViewBuilders;
using System.Collections.Generic;
using FamilyControlCenter.Filters;
using System;
using System.Threading.Tasks;

namespace FamilyControlCenter.Controllers
{
    //[Authorize(Roles = "User")]
    [RoutePrefix("{lang}/family")]
    [Localization]
    public class FamilyController : BaseController
    {
        private readonly IFccViewBuilder _vwbFcc;

        public FamilyController(IFccViewBuilder vwbFcc)
        {
            _vwbFcc = vwbFcc;
        }

        public async Task<ActionResult> PersonDetail(string personId)
        {
            PersonViewModel vm = new PersonViewModel();
            BeforeLoadAction(vm);
            vm.Model = new Person();
            vm.Model.Id = personId;
            vm.Command = ActionCommand.Open;
            vm.State = VmState.Detail;
            await _vwbFcc.HandleAction(vm);
            return View("Person", vm);
        }

        public async Task<ActionResult> Person(int page = 1, int take = 10)
        {
            var vm = new PersonViewModel();
            BeforeLoadAction(vm);
            vm.Page = page;
            vm.Take = take;
            await _vwbFcc.HandleAction(vm);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Person(PersonViewModel vm)
        {
            BeforeLoadAction(vm);
            await _vwbFcc.HandleAction(vm);
            return View(vm);
        }

        public async Task<PartialViewResult> PersonDocuments(string personId)
        {
            BeforeLoadAction();
            var vm = await _vwbFcc.CreatePartialViewPersonDocuments(personId);
            return PartialView("Person/_PersonDocuments", vm);
        }

        public async Task<PartialViewResult> PersonPhotoSection(string personId)
        {
            BeforeLoadAction();
            KeyValuePair<string, IEnumerable<FileContent>> vm = await _vwbFcc.CreatePartialViewPersonPhotos(personId);
            return PartialView("Person/_PhotoSection", vm);
        }

        [HttpPost]
        public async Task<PartialViewResult> PersonRelations(string personId)
        {
            BeforeLoadAction();
            PersonRelationsViewModel vm = await _vwbFcc.CreatePersonPartialViewRelationsModel(personId);
            return PartialView("Person/_PersonRelations", vm);
        }

        [HttpPost]
        public async Task<PartialViewResult> PersonBiography(string personId)
        {
            BeforeLoadAction();
            var vm = await _vwbFcc.CreatePartialViewPersonBiography(personId);
            return PartialView("Person/_PersonBiography", vm);
        }

        [HttpPost]
        public async Task SavePersonActivity(string personId, string bioId, PersonActivity newact)
        {
            await _vwbFcc.SavePersonActivity(personId, bioId, newact);
        }

        [HttpPost]
        public async Task<PartialViewResult> MarriagePartialView(string personId, string spouseId, string partnerId)
        {
            BeforeLoadAction();
            var vm = await _vwbFcc.CreatePartialViewForMarriageOrLivePartner(personId, spouseId, partnerId);
            return PartialView("Person/_MarriageSection", vm);
        }

        [HttpPost]
        public async Task<PartialViewResult> NamesAndPatronymPartialView(string personId)
        {
            BeforeLoadAction();
            IEnumerable<PersonName> vm = await _vwbFcc.CreatePartialViewForNamesAndPatronymList(personId);
            return PartialView("Person/_PersonNames", vm);
        }

        public async Task<ActionResult> PersonRelationsUpdateStack(string personId = null, string selectedId = null)
        {
            BeforeLoadAction();
            var vm = await _vwbFcc.CreateUpdateRelationsStackViewModel(personId, selectedId);
            return View("_PersonRelationsUpdateStack", vm);
        }

        public async Task<PartialViewResult> PersonRelationsUpdateStackAsPartial(string personId = null, string selectedId = null)
        {
            BeforeLoadAction();
            var vm = await _vwbFcc.CreateUpdateRelationsStackViewModel(personId, selectedId);
            return PartialView("RelationsUpdateStack/_PersonRelationsUpdateStack", vm);
        }

        [HttpPost]
        public async Task<PartialViewResult> LoadRelationsPartialForPersonRelationsUpdateStack(string personId, string selectedId)
        {
            var vm = await _vwbFcc.CreateRelationsUpdateStackPartial(personId, selectedId);
            return PartialView("RelationsUpdateStack/_RelationsStack", vm);
        }

        [HttpPost]
        public async Task<PartialViewResult> PersonActivityEdit(string activityId)
        {
            BeforeLoadAction();
            PersonActivity vm;

            if (!string.IsNullOrWhiteSpace(activityId))
                vm = await _vwbFcc.GetPersonActivity(activityId);
            else
                vm = new PersonActivity() { HasBegun = true, DateBegin = DateTime.Now };

            return PartialView("Person/_PersonActivityEditBlock", vm);
        }
    }
}