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
    [RoutePrefix("{lang}/family")]
    public class FamilyController : BaseController
    {
        IFccViewBuilder _vwbFcc;

        public FamilyController(IFccViewBuilder vwbFcc)
        {
            _vwbFcc = vwbFcc;
        }

        [Authorize]
        public ActionResult Person()
        {
            BeforeLoadAction();
            var vm = new PersonViewModel();
            _vwbFcc.HandleAction(vm);
            return View(vm);
        }

        [Authorize(Roles = "User"), HttpPost]
        public ActionResult Person(PersonViewModel vm)
        {
            BeforeLoadAction();
            _vwbFcc.HandleAction(vm);
            return View(vm);
        }

        [Authorize, HttpPost]
        public PartialViewResult PersonRelations(string personId)
        {
            BeforeLoadAction();
            List<PersonRelation> vm = _vwbFcc.CreatePersonPartialViewRelationsModel(personId);
            ViewData = new ViewDataDictionary() { new KeyValuePair<string, object>("PersonId", personId) };
            return PartialView("Person/_PersonRelations", vm);
        }

        [Authorize, HttpPost]
        public PartialViewResult PersonBiography(string personId)
        {
            BeforeLoadAction();
            var vm = _vwbFcc.CreatePersonPartialViewRelationsModel(personId);
            return PartialView("Person/_PersonBiography", vm);
        }

        [Authorize, HttpPost]
        public PartialViewResult MarriagePartialView(string personId, string spouseId)
        {
            BeforeLoadAction();
            var vm = _vwbFcc.CreatePartialViewForMarriageOrLivePartner(personId, spouseId);
            return PartialView("Person/_MarriageSection", vm);
        }

        [Authorize, HttpPost]
        public PartialViewResult NamesAndPatronymPartialView(string personId)
        {
            BeforeLoadAction();
            List<PersonName> vm = _vwbFcc.CreatePartialViewForNamesAndPatronymList(personId);
            return PartialView("Person/_PersonNames", vm);
        }
    }
}