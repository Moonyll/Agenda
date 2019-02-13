using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Agenda.Controllers
{
    public class HomeController : Controller
    {
        //// GET: Home
        //public ActionResult Index()
        //{
        //    return View();
        //}
        // GET: Appointments
        public ActionResult Index()
        {
            //Redirection vers la liste des clients
            return RedirectToAction("AppointmentList", "Appointments");
        }




    }
}