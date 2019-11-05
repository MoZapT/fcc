using Shared.Interfaces.Managers;
using Shared.Viewmodels;
using Shared.Models;
using System.Linq;
using System.Web.Mvc;
using Shared.Enums;
using System;
using Shared.Interfaces.ViewBuilders;
using System.Collections.Generic;

namespace FamilyControlCenter.Controllers
{
    [Authorize(Roles = "User")]
    [RoutePrefix("{lang}/family")]
    public class FamilyController : BaseController
    {
        IFccViewBuilder _vwbFcc;

        public FamilyController(IFccViewBuilder vwbFcc)
        {
            _vwbFcc = vwbFcc;
        }

        public ActionResult Person(int page = 1, int take = 10)
        {
            var vm = new PersonViewModel();
            BeforeLoadAction(vm);
            vm.Page = page;
            vm.Take = take;
            _vwbFcc.HandleAction(vm);
            return View(vm);
        }

        public ActionResult PersonDetail(string personId)
        {
            PersonViewModel vm = new PersonViewModel();
            BeforeLoadAction(vm);
            vm.Model = new Person();
            vm.Model.Id = personId;
            vm.Command = ActionCommand.Open;
            vm.State = VmState.Detail;
            _vwbFcc.HandleAction(vm);
            return View("Person", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Person(PersonViewModel vm)
        {
            BeforeLoadAction(vm);
            _vwbFcc.HandleAction(vm);
            return View(vm);
        }

        public PartialViewResult PersonDocuments(string personId/*, int section?*/)
        {
            BeforeLoadAction();
            List<PersonDocument> vm = _vwbFcc.CreatePartialViewPersonDocuments(personId);
            return PartialView("Person/_PersonDocuments", vm);
        }

        public PartialViewResult PersonPhotoSection(string personId)
        {
            BeforeLoadAction();
            KeyValuePair<string, List<FileContent>> vm = _vwbFcc.CreatePartialViewPersonPhotos(personId);
            return PartialView("Person/_PhotoSection", vm);
        }

        [HttpPost]
        public PartialViewResult PersonRelations(string personId)
        {
            BeforeLoadAction();
            List<PersonRelation> vm = _vwbFcc.CreatePersonPartialViewRelationsModel(personId);
            ViewData = new ViewDataDictionary() { new KeyValuePair<string, object>("PersonId", personId) };
            return PartialView("Person/_PersonRelations", vm);
        }

        [HttpPost]
        public PartialViewResult PersonBiography(string personId)
        {
            BeforeLoadAction();
            var vm = _vwbFcc.CreatePartialViewPersonBiography(personId);
            return PartialView("Person/_PersonBiography", vm);
        }

        [HttpPost]
        public void SavePersonActivity(string personId, string bioId, PersonActivity newact)
        {
            var vm = _vwbFcc.SavePersonActivity(personId, bioId, newact);
        }

        [HttpPost]
        public PartialViewResult MarriagePartialView(string personId, string spouseId, string partnerId)
        {
            BeforeLoadAction();
            var vm = _vwbFcc.CreatePartialViewForMarriageOrLivePartner(personId, spouseId, partnerId);
            return PartialView("Person/_MarriageSection", vm);
        }

        [HttpPost]
        public PartialViewResult NamesAndPatronymPartialView(string personId)
        {
            BeforeLoadAction();
            List<PersonName> vm = _vwbFcc.CreatePartialViewForNamesAndPatronymList(personId);
            return PartialView("Person/_PersonNames", vm);
        }
    }
}