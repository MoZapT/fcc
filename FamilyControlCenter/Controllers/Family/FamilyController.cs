using Shared.Interfaces.Managers;
using Shared.Models;
using Shared.Viewmodels.Family;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FamilyControlCenter.Controllers
{
    public class FamilyController : Controller
    {
        IFccManager _mgrFcc;

        public FamilyController(IFccManager mgrFcc)
        {
            _mgrFcc = mgrFcc;
        }

        //[Authorize]
        public ActionResult Person()
        {
            var vm = new PersonViewModel();
            var result = _mgrFcc.HandleAction(vm);
            return View(vm);
        }

        [/*Authorize,*/ HttpPost]
        public ActionResult Person(PersonViewModel vm)
        {
            var result = _mgrFcc.HandleAction(vm);
            ModelState.Clear();
            return View(vm);
        }

        [/*Authorize,*/ HttpPost]
        //[Route("api/set/relations/{entity}/{id}")]
        public PartialViewResult SetPersonRelation(PersonRelation entity, string id)
        {
            var result = _mgrFcc.SetPersonRelations(entity);
            var vm = new PersonViewModel
            {
                Relations = _mgrFcc.GetPersonRelationGroupsByPersonId(id).ToList()
            };
            return PartialView("Person/_RelationsList", vm);
        }

        [/*Authorize,*/ HttpPost]
        //[Route("api/delete/relations/{id}")]
        public PartialViewResult DeletePersonRelation(string relationid, string personid)
        {
            var result = _mgrFcc.DeletePersonRelation(relationid);

            var vm = new PersonViewModel
            {
                Relations = _mgrFcc.GetPersonRelationGroupsByPersonId(personid).ToList()
            };
            return PartialView("Person/_RelationsList", vm);
        }
    }
}