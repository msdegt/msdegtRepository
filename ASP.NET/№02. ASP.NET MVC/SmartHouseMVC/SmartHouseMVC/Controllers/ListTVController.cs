using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartHouseMVC.Controllers
{
    public class ListTVController : Controller
    {
        public ActionResult List()
        {
            ViewBag.Title = "Список каналов";
            return View((string[])TempData["Mass"]);
        }
        public ActionResult GoBack()
        {
            return RedirectToAction("Index", "SmartHouse");
        }
    }
}