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
    public class BrokerController : Controller
    {
        private AgendaEntities db = new AgendaEntities();
        // Page ajouter courtier
        public ActionResult addBroker()
        {
            return View("addBroker");
        }
        // Page de succès
        public ActionResult Success()
        {
            return View("Success");
        }
        // Page de succès MAJ courtier
        public ActionResult Maj()
        {
            return View("Maj");
        }
        // Add A New Broker
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult addBroker([Bind(Include = "idBroker,LastName,FirstName,Mail,PhoneNumber")] Broker broker)
        {
            //Déclaration des regex
            string regexName = @"^[A-Za-zéèàêâôûùïüç\-]+$";
            string regexMail = @"[0-9a-zA-Z\.\-]+@[0-9a-zA-Z\.\-]+.[a-zA-Z]{2,4}";
            string regexPhone = @"^[0][0-9]{9}";

            //Vérification que le champ lastname n'est pas null ou vide
            if (!String.IsNullOrEmpty(broker.LastName)) //si le champ lastname n'est pas vide ou null on vérifie la validité de l'entrée
            {
                //Vérification de la validité de l'entrée
                if (!Regex.IsMatch(broker.LastName, regexName)) //si l'entrée utilisateur ne passe pas la regex ajout d'un message d'erreur
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
            if (!String.IsNullOrEmpty(broker.FirstName))
            {
                //Vérification de la validité de l'entrée
                if (!Regex.IsMatch(broker.FirstName, regexName))
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
            if (!String.IsNullOrEmpty(broker.Mail))
            {
                //Vérification de la validité de l'entrée
                if (!Regex.IsMatch(broker.Mail, regexMail))
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
            if (!String.IsNullOrEmpty(broker.PhoneNumber))
            {
                //Vérification de la validité de l'entrée
                if (!Regex.IsMatch(broker.PhoneNumber, regexPhone))
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
                db.Brokers.Add(broker); //insertion dans base
                db.SaveChanges(); //enregistrement des changements
                return RedirectToAction("Success"); //redirection vers la page SuccessAddBroker
            }
            else
            {
                return View(broker); //s'il y a des erreurs réaffichage du formulaire
            }
        }
        //Liste des courtiers
        public ActionResult ListBrokers()
        {
            return View(db.Brokers.ToList());
        }
        // Détails courtier
        public ActionResult profilBroker(int id) // int? entier qui peux être nul !
        {
            Broker broker = db.Brokers.Find(id);
            if (broker == null)
            {
                return RedirectToAction("Error"); // Mettre une page d'erreur
            }
            return View(broker);
        }
        // Mise à jour courtier
        public ActionResult profileBroker(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Broker brokerup = db.Brokers.Find(id);
            if (brokerup == null)
            {
                return RedirectToAction("Error");
            }
            return View(brokerup);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult profilBroker([Bind(Include = "idBroker,LastName,FirstName,Mail,PhoneNumber")] Broker brokerup)
        {
        //Déclaration des regex
        string regexName = @"^[A-Za-zéèàêâôûùïüç\-]+$";
            string regexMail = @"[0-9a-zA-Z\.\-]+@[0-9a-zA-Z\.\-]+.[a-zA-Z]{2,4}";
            string regexPhone = @"^[0][0-9]{9}";

            //Vérification que le champ lastname n'est pas null ou vide
            if (!String.IsNullOrEmpty(brokerup.LastName)) //si le champ lastname n'est pas vide ou null on vérifie la validité de l'entrée
            {
                //Vérification de la validité de l'entrée
                if (!Regex.IsMatch(brokerup.LastName, regexName)) //si l'entrée utilisateur ne passe pas la regex ajout d'un message d'erreur
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
            if (!String.IsNullOrEmpty(brokerup.FirstName))
            {
                //Vérification de la validité de l'entrée
                if (!Regex.IsMatch(brokerup.FirstName, regexName))
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
            if (!String.IsNullOrEmpty(brokerup.Mail))
            {
                //Vérification de la validité de l'entrée
                if (!Regex.IsMatch(brokerup.Mail, regexMail))
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
            if (!String.IsNullOrEmpty(brokerup.PhoneNumber))
            {
                //Vérification de la validité de l'entrée
                if (!Regex.IsMatch(brokerup.PhoneNumber, regexPhone))
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
                db.Entry(brokerup).State = EntityState.Modified;//Mise à jour
                db.SaveChanges(); //enregistrement des changements
                return RedirectToAction("Maj"); //redirection vers la page Maj
            }
            else
            {
                return View(brokerup); //s'il y a des erreurs réaffichage du formulaire
            }
        }
    }
}
