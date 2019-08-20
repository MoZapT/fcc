using FamilyControlCenter.Viewmodels.Family;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FamilyControlCenter.Controllers
{
    public class FamilyController : Controller
    {
        //[Authorize]
        public ActionResult PersonList()
        {
            PersonListViewModel vm = new PersonListViewModel();
            vm.HandleAction();

            return View("PersonList", vm);
        }

        //[Authorize, HttpPost]
        [HttpPost]
        public ActionResult PersonList(PersonListViewModel vm)
        {
            vm.HandleAction();

            return View("PersonList", vm);
        }
        //[Authorize]
        public ActionResult Person()
        {
            PersonViewModel vm = new PersonViewModel();
            vm.HandleAction();

            return View("Person", vm);
        }

        //[Authorize, HttpPost]
        [HttpPost]
        public ActionResult Person(PersonViewModel vm)
        {
            vm.HandleAction();

            return View("Person", vm);
        }
    }
}