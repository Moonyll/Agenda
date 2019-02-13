using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.Entity.Validation;
using Agenda.Models;
using System.Text.RegularExpressions; // Pour utilisation des regex

namespace Agenda.Controllers
{
    public class CustomerController : Controller
    {
        private AgendaEntities db = new AgendaEntities();
        // Page ajouter client
        public ActionResult addCustomer()
        {
            return View("addCustomer");
        }
        // Page de succès
        public ActionResult Success()
        {
            return View("Success");
        }
        // Page de succès MAJ client
        public ActionResult Maj()
        {
            return View("Maj");
        }
        // Ajout nouveau client
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult addCustomer([Bind(Include = "idCustomer,LastName,FirstName,Mail,PhoneNumber,Budget,Subject")] Customer customer)
        {
            //Déclaration des regex
            string regexName = @"^[A-Za-zéèàêâôûùïüç\-]+$";
            string regexMail = @"[0-9a-zA-Z\.\-]+@[0-9a-zA-Z\.\-]+.[a-zA-Z]{2,4}";
            string regexPhone = @"^[0][0-9]{9}";

            //Vérification que le champ lastname n'est pas null ou vide
            if (!String.IsNullOrEmpty(customer.LastName)) //si le champ lastname n'est pas vide ou null on vérifie la validité de l'entrée
            {
                //Vérification de la validité de l'entrée
                if (!Regex.IsMatch(customer.LastName, regexName)) //si l'entrée utilisateur ne passe pas la regex ajout d'un message d'erreur
                {
                    //Message d'erreur
                    ModelState.AddModelError("LastName", "Veuillez écrire un nom valide");
                }
            }
            else
            {
                //Message d'erreur si le champ lastname est vide ou null
                ModelState.AddModelError("LastName", "Veuillez écrire un nom");
            }
            //Vérification que le champ firstname n'est pas null ou vide
            if (!String.IsNullOrEmpty(customer.FirstName))
            {
                //Vérification de la validité de l'entrée
                if (!Regex.IsMatch(customer.FirstName, regexName))
                {
                    //Message d'erreur
                    ModelState.AddModelError("FirstName", "Veuillez écrire un prénom valide");
                }
            }
            else
            {
                //Message d'erreur
                ModelState.AddModelError("FirstName", "Veuillez écrire un prénom");
            }
            //Vérification que le champ mail n'est pas null ou vide
            if (!String.IsNullOrEmpty(customer.Mail))
            {
                //Vérification de la validité de l'entrée
                if (!Regex.IsMatch(customer.Mail, regexMail))
                {
                    //Message d'erreur
                    ModelState.AddModelError("Mail", "Veuillez écrire un mail valide");
                }
            }
            else
            {
                //Message d'erreur
                ModelState.AddModelError("Mail", "Veuillez écrire un mail");
            }
            //Vérification que le champ phoneNumber n'est pas null ou vide
            if (!String.IsNullOrEmpty(customer.PhoneNumber))
            {
                //Vérification de la validité de l'entrée
                if (!Regex.IsMatch(customer.PhoneNumber, regexPhone))
                {
                    //Message d'erreur
                    ModelState.AddModelError("PhoneNumber", "Veuillez écrire un téléphone valide");
                }
            }
            else
            {
                //Message d'erreur
                ModelState.AddModelError("PhoneNumber", "Veuillez renseigner un numéro de téléphone");
            }

            //si il n'y a pas d'erreur
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer); //insertion dans base
                db.SaveChanges(); //enregistrement des changements
                return RedirectToAction("Success"); //redirection vers la page SuccessAddBroker
            }
            else
            {
                return View(customer); //s'il y a des erreurs réaffichage du formulaire
            }
        }
        //Liste des clients
        public ActionResult ListCustomers()
        {
            return View(db.Customers.ToList());
        }
        // Détails client
        public ActionResult profilCustomer(int id)
        {
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return RedirectToAction("Error");
            }
            return View(customer);
        }
        // Mise à jour client
        public ActionResult profileCustomer(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customerup = db.Customers.Find(id);
            if (customerup == null)
            {
                return RedirectToAction("Error");
            }
            return View(customerup);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult profilCustomer([Bind(Include = "idCustomer,LastName,FirstName,Mail,PhoneNumber,Budget,Subject")] Customer customerup)
        {
            //Déclaration des regex
            string regexName = @"^[A-Za-zéèàêâôûùïüç\-]+$";
            string regexMail = @"[0-9a-zA-Z\.\-]+@[0-9a-zA-Z\.\-]+.[a-zA-Z]{2,4}";
            string regexPhone = @"^[0][0-9]{9}";

            //Vérification que le champ lastname n'est pas null ou vide
            if (!String.IsNullOrEmpty(customerup.LastName)) //si le champ lastname n'est pas vide ou null on vérifie la validité de l'entrée
            {
                //Vérification de la validité de l'entrée
                if (!Regex.IsMatch(customerup.LastName, regexName)) //si l'entrée utilisateur ne passe pas la regex ajout d'un message d'erreur
                {
                    //Message d'erreur
                    ModelState.AddModelError("LastName", "Veuillez écrire un nom valide");
                }
            }
            else
            {
                //Message d'erreur si le champ lastname est vide ou null
                ModelState.AddModelError("LastName", "Veuillez écrire un nom");
            }
            //Vérification que le champ firstname n'est pas null ou vide
            if (!String.IsNullOrEmpty(customerup.FirstName))
            {
                //Vérification de la validité de l'entrée
                if (!Regex.IsMatch(customerup.FirstName, regexName))
                {
                    //Message d'erreur
                    ModelState.AddModelError("FirstName", "Veuillez écrire un prénom valide");
                }
            }
            else
            {
                //Message d'erreur
                ModelState.AddModelError("FirstName", "Veuillez écrire un prénom");
            }
            //Vérification que le champ mail n'est pas null ou vide
            if (!String.IsNullOrEmpty(customerup.Mail))
            {
                //Vérification de la validité de l'entrée
                if (!Regex.IsMatch(customerup.Mail, regexMail))
                {
                    //Message d'erreur
                    ModelState.AddModelError("Mail", "Veuillez écrire un mail valide");
                }
            }
            else
            {
                //Message d'erreur
                ModelState.AddModelError("Mail", "Veuillez écrire un mail");
            }
            //Vérification que le champ phoneNumber n'est pas null ou vide
            if (!String.IsNullOrEmpty(customerup.PhoneNumber))
            {
                //Vérification de la validité de l'entrée
                if (!Regex.IsMatch(customerup.PhoneNumber, regexPhone))
                {
                    //Message d'erreur
                    ModelState.AddModelError("PhoneNumber", "Veuillez écrire un téléphone valide");
                }
            }
            else
            {
                //Message d'erreur
                ModelState.AddModelError("PhoneNumber", "Veuillez renseigner un numéro de téléphone");
            }

            //si il n'y a pas d'erreur
            if (ModelState.IsValid)
            {
                db.Entry(customerup).State = EntityState.Modified;//Mise à jour
                db.SaveChanges(); //enregistrement des changements
                return RedirectToAction("Maj"); //redirection vers la page Maj
            }
            else
            {
                return View(customerup); //s'il y a des erreurs réaffichage du formulaire
            }
        }
            // Suppression client
        public ActionResult Suppr(int? id)
        {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Customer customerdel = db.Customers.Find(id);
                if (customerdel == null)
                {
                    return RedirectToAction("Error");
                }
                return View(customerdel);
        }
        // Suppression client
        [HttpPost, ActionName("Suppr")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customerdel = db.Customers.Find(id);
            db.Customers.Remove(customerdel);
            db.SaveChanges();
            return RedirectToAction("ListCustomers");
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
