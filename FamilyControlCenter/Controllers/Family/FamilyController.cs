using FamilyControlCenter.Manager;
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
        FccManager Mgr { get; set; }

        //[Authorize]
        public ActionResult PersonList()
        {
            PersonListViewModel vm = Mgr.GetPersons();
            return View(vm);
        }

        [/*Authorize,*/ HttpPost]
        public ActionResult PersonList(PersonListViewModel vm)
        {
            return View(vm);
        }
        //[Authorize]
        public ActionResult Person(string id)
        {
            PersonViewModel vm = Mgr.GetPerson(id).Result;
            return View(vm);
        }

        [/*Authorize,*/ HttpPost]
        public ActionResult Person(PersonViewModel vm)
        {
            return View(vm);
        }
    }
}