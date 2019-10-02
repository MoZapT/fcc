using FamilyControlCenter.Interfaces.Managers;
using FamilyControlCenter.Viewmodels.Family;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace FamilyControlCenter.Controllers
{
    [RoutePrefix("{lang}/family")]
    public class FamilyController : BaseController
    {
        IFccManager _mgrFcc;

        public FamilyController(IFccManager mgrFcc)
        {
            _mgrFcc = mgrFcc;
        }

        //[Authorize]
        public ActionResult Person()
        {
            BeforeLoadAction();
            var vm = new PersonViewModel();
            _mgrFcc.HandleAction(vm);
            return View(vm);
        }

        [/*Authorize,*/ HttpPost]
        public ActionResult Person(PersonViewModel vm)
        {
            BeforeLoadAction();
            _mgrFcc.HandleAction(vm);
            return View(vm);
        }

        [/*Authorize,*/ HttpPost]
        //[Route("api/set/relations/{entity}/{id}")]
        public PartialViewResult SetPersonRelation(PersonRelation entity, string personid)
        {
            BeforeLoadAction();
            var result = _mgrFcc.SetPersonRelations(entity);
            var vm = new PersonViewModel
            {
                Relations = _mgrFcc.GetPersonRelationGroupsByPersonId(personid).ToList()
            };
            return PartialView("Person/_RelationsList", vm);
        }

        [/*Authorize,*/ HttpPost]
        //[Route("api/delete/relations/{id}")]
        public PartialViewResult DeletePersonRelation(string relationid, string personid)
        {
            BeforeLoadAction();
            var result = _mgrFcc.DeletePersonRelation(relationid);

            var vm = new PersonViewModel
            {
                Relations = _mgrFcc.GetPersonRelationGroupsByPersonId(personid).ToList()
            };
            return PartialView("Person/_RelationsList", vm);
        }
    }
}