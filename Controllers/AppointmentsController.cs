using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Agenda.Models;
using System.Text.RegularExpressions; // Pour utilisation des regex

namespace Agenda.Controllers
{
    public class AppointmentsController : Controller
    {
        private AgendaEntities db = new AgendaEntities();
        // Liste des RDV
        public ActionResult AppointmentList()
        {
            var appointments = db.Appointments.Include(a => a.Broker).Include(a => a.Customer);
            return View(appointments.ToList());
        }
        // Détail RDV
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return RedirectToAction("Error", "Error");
            }
            return View(appointment);
        }
        // Création I RDV
        public ActionResult addAppointment(int? id)
        {
            if (id == null)
            {
                ViewBag.idBroker = new SelectList(db.Brokers, "idBroker", "LastName");
                ViewBag.idCustomer = new SelectList(db.Customers, "idCustomer", "LastName");
                return View();
            }
            else
            {
                ViewBag.idBroker = new SelectList(db.Brokers, "idBroker", "LastName",id);
                ViewBag.idCustomer = new SelectList(db.Customers, "idCustomer", "LastName");
                return View();
            }
        }
        // Création II RDV
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult addAppointment([Bind(Include = "idAppointment,DateHour,idBroker,idCustomer")] Appointment appointment)
        {
            ViewBag.idBroker = new SelectList(db.Brokers, "idBroker", "LastName", appointment.idBroker);
            ViewBag.idCustomer = new SelectList(db.Customers, "idCustomer", "LastName", appointment.idCustomer);
            Appointment test = db.Appointments.Where(x => x.idBroker == appointment.idBroker && x.DateHour == appointment.DateHour).FirstOrDefault();
            if (test == null) // Test si un courtier a déjà un rendez-vous !
            {
                if (ModelState.IsValid)
                {
                db.Appointments.Add(appointment);
                db.SaveChanges();
                return RedirectToAction("AppointmentList");
                }
                else
                {
                return View(appointment);
                }
            }
            else
            {
            return View("Already");
            }
        }
        // Edition I RDV
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return RedirectToAction("Error", "Error");
            }
            ViewBag.idBroker = new SelectList(db.Brokers, "idBroker", "LastName", appointment.idBroker);
            ViewBag.idCustomer = new SelectList(db.Customers, "idCustomer", "LastName", appointment.idCustomer);
            return View(appointment);
        }
        // Edition II RDV
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idAppointment,DateHour,idBroker,idCustomer")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appointment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AppointmentList");
            }
            ViewBag.idBroker = new SelectList(db.Brokers, "idBroker", "LastName", appointment.idBroker);
            ViewBag.idCustomer = new SelectList(db.Customers, "idCustomer", "LastName", appointment.idCustomer);
            return View(appointment);
        }
        // Suppression I RDV
        public ActionResult DelApp(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment delapp = db.Appointments.Find(id);
            if (delapp == null)
            {
                return RedirectToAction("Error", "Error");
            }
            return View(delapp);
        }
        // Suppression II RDV
        [HttpPost, ActionName("DelApp")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Appointment delapp = db.Appointments.Find(id);
            db.Appointments.Remove(delapp);
            db.SaveChanges();
            return RedirectToAction("AppointmentList");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
