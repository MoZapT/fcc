using Shared.Viewmodels;
using Shared.Models;
using System.Web.Mvc;
using Shared.Enums;
using Shared.Interfaces.ViewBuilders;
using System.Collections.Generic;
using FamilyControlCenter.Filters;
using System;
using System.Threading.Tasks;
using Shared.Interfaces.Managers;
using System.Linq;

namespace FamilyControlCenter.Controllers
{
    [Authorize(Roles = "User")]
    [RoutePrefix("{lang}/family")]
    [Localization]
    [PreLoadAction]
    public class FamilyController : BaseController
    {
        private readonly IFccViewBuilder _vwbFcc;
        private readonly IFccManager _mgrFcc;

        public FamilyController(IFccViewBuilder vwbFcc, IFccManager mgrFcc)
        {
            _mgrFcc = mgrFcc;
            _vwbFcc = vwbFcc;
        }

        #region Unsorted

        public async Task<ActionResult> PersonDetail(string personId)
        {
            PersonViewModel vm = new PersonViewModel();
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
            vm.Page = page;
            vm.Take = take;
            await _vwbFcc.HandleAction(vm);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Person(PersonViewModel vm)
        {
            await _vwbFcc.HandleAction(vm);
            return View(vm);
        }

        public async Task<PartialViewResult> PersonPhotoSection(string personId)
        {
            KeyValuePair<string, IEnumerable<FileContent>> vm = await _vwbFcc.CreatePartialViewPersonPhotos(personId);
            return PartialView("Person/_PhotoSection", vm);
        }

        [HttpPost]
        public async Task<PartialViewResult> PersonRelations(string personId)
        {
            PersonRelationsViewModel vm = await _vwbFcc.CreatePersonPartialViewRelationsModel(personId);
            return PartialView("Person/_PersonRelations", vm);
        }

        [HttpPost]
        public async Task<PartialViewResult> PersonBiography(string personId)
        {
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
            var vm = await _vwbFcc.CreatePartialViewForMarriageOrLivePartner(personId, spouseId, partnerId);
            return PartialView("Person/_MarriageSection", vm);
        }

        [HttpPost]
        public async Task<PartialViewResult> NamesAndPatronymPartialView(string personId)
        {
            IEnumerable<PersonName> vm = await _vwbFcc.CreatePartialViewForNamesAndPatronymList(personId);
            return PartialView("Person/_PersonNames", vm);
        }

        public async Task<ActionResult> PersonRelationsUpdateStack(string personId = null, string selectedId = null)
        {
            var vm = await _vwbFcc.CreateUpdateRelationsStackViewModel(personId, selectedId);
            return View("_PersonRelationsUpdateStack", vm);
        }

        public async Task<PartialViewResult> PersonRelationsUpdateStackAsPartial(string personId = null, string selectedId = null)
        {
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
            PersonActivity vm;

            if (!string.IsNullOrWhiteSpace(activityId))
                vm = await _vwbFcc.GetPersonActivity(activityId);
            else
                vm = new PersonActivity() { HasBegun = true, DateBegin = DateTime.Now };

            return PartialView("Person/_PersonActivityEditBlock", vm);
        }

        #endregion

        #region Documents

        public async Task<PartialViewResult> PersonDocuments(string personId)
        {
            var vm = await _vwbFcc.CreatePartialViewPersonDocuments(personId);
            return PartialView("Person/_PersonDocuments", vm);
        }

        public async Task<PartialViewResult> PersonDocumentsList(string personId)
        {
            return await LoadPersonDocumentListPartialView(personId);
        }

        [Route("person/document/delete/{personId}/{docs}")]
        public async Task<PartialViewResult> DeletePersonDocumentsList(string personId, IEnumerable<string> docs)
        {
            await _mgrFcc.DeletePersonDocuments(docs);

            return await LoadPersonDocumentListPartialView(personId);
        }

        [Route("person/document/move/{personId}/{docs}/{activity?}")]
        [HttpPost]
        public async Task<PartialViewResult> MovePersonDocumentsList(string personId, IEnumerable<PersonDocumentViewModel> docs, string activity = null)
        {
            await _mgrFcc.MovePersonDocumentsToAnotherCategory(docs.Select(e => e.FileContentId), activity);

            return await LoadPersonDocumentListPartialView(personId);
        }

        private async Task<PartialViewResult> LoadPersonDocumentListPartialView(string personId)
        {
            var vm = await _vwbFcc.CreatePartialViewPersonDocuments(personId);
            return PartialView("Person/_PersonDocumentList", vm);
        }

        #endregion

    }
}