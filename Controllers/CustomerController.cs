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
using PagedList;

namespace Agenda.Controllers
{
    public class CustomerController : Controller
    {
        private AgendaEntities db = new AgendaEntities();
        //Déclaration des regex
        string regexName = @"^[A-Za-zéèàêâôûùïüç\-]+$";
        string regexMail = @"[0-9a-zA-Z\.\-]+@[0-9a-zA-Z\.\-]+.[a-zA-Z]{2,4}";
        string regexPhone = @"^[0][0-9]{9}";
        //string regexSubject = @"^[A-Za-zéèêëâäàçîïôö-&.,'\ ]+$";
        string regexSubject = @"^[A-Za-zéèàêâôûùïüç\-]+$";
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
            ////Déclaration des regex
            //string regexName = @"^[A-Za-zéèàêâôûùïüç\-]+$";
            //string regexMail = @"[0-9a-zA-Z\.\-]+@[0-9a-zA-Z\.\-]+.[a-zA-Z]{2,4}";
            //string regexPhone = @"^[0][0-9]{9}";

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
        //return View(db.Customers.ToList());
        //return View(db.Customers.SqlQuery("Select * from Customers").ToList<Customer>()); // Méthode Sql Query
        var request = "SELECT [idCustomer], [LastName], [FirstName], [Mail], [PhoneNumber], [Budget], [Subject] " +
                "FROM [dbo].[Customers] " +
                "ORDER BY [LastName] ASC";
        var listCustomers = db.Customers.SqlQuery(request);
        return View(listCustomers);
        }
        // Détails client
        public ActionResult profilCustomer(int? id)
        {
            Customer customer_detail = db.Customers.Find(id);
            if (customer_detail == null || id == null) // Vérification de customer et id (url correcte) !!
            {
                return RedirectToAction("Error", "Error");
            }
            return View(customer_detail);
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
                return RedirectToAction("Error", "Error");
            }
            return View(customerup);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult profilCustomer([Bind(Include = "idCustomer,LastName,FirstName,Mail,PhoneNumber,Budget,Subject")] Customer customerup)
        {
            ////Déclaration des regex
            //string regexName = @"^[A-Za-zéèàêâôûùïüç\-]+$";
            //string regexMail = @"[0-9a-zA-Z\.\-]+@[0-9a-zA-Z\.\-]+.[a-zA-Z]{2,4}";
            //string regexPhone = @"^[0][0-9]{9}";

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
                var alreadyUsed = db.Customers.Where(customer => customer.Mail == customerup.Mail).SingleOrDefault();
                if (!Regex.IsMatch(customerup.Mail, regexMail))
                {
                    //Message d'erreur
                    ModelState.AddModelError("Mail", "Veuillez écrire un mail valide");
                }
                else if (alreadyUsed != null)
                {
                    //Message d'erreur
                    ModelState.AddModelError("Mail", "Veuillez écrire un mail non utilisé");
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
            if(customerup.Budget <= 0)
            {
                //Message d'erreur
                ModelState.AddModelError("Budget", "Veuillez indiquer un budget valide");
            }
            if (!String.IsNullOrEmpty(customerup.Subject))
            {
                //Vérification de la validité de l'entrée
                if (!Regex.IsMatch(customerup.Subject, regexSubject))
                {
                    //Message d'erreur
                    ModelState.AddModelError("Subject", "Veuillez écrire un sujet valide");
                }
            }
            else
            {
                //Message d'erreur
                ModelState.AddModelError("Subject", "Veuillez écrire un sujet");
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
                    return RedirectToAction("Error", "Error");
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
        //METHODE TRYUPDATEMODEL//
        public ActionResult Custadds()
        {
            return View("Custadds");
        }
        public ActionResult Succes()
        {
            return View("Succes");
        }

        [HttpPost, ActionName("Custadds")]
        [ValidateAntiForgeryToken]
        public ActionResult Custadd()
        {
            Customer custadd = new Customer();
            TryUpdateModel(custadd);

            if (ModelState.IsValid)
            {
                db.Customers.Add(custadd);
                db.SaveChanges();
                return RedirectToAction("Succes");
            }
            else
            {
                return View(custadd);
            }
        }
        public ActionResult Pagination()
        {
            var compte = db.Customers.Count();//Nombre total de clients dans la base de données
            var nbcust = 5;// Nombre de clients par page
            double value = compte / nbcust; // Total client / nb clients a afficher par page
            var nbpage = Math.Ceiling(value) + 1; // Nombre de pages
                                                  // Création de la liste des pages
            var listnumber = new List<SelectListItem>();
            for (var n = 1; n <= nbpage; n++)
                listnumber.Add(new SelectListItem { Text = n.ToString(), Value = n.ToString() });
            ViewBag.listnumber = listnumber;
            return View();
        }
        [HttpPost]
        public ActionResult getSelectedValue(SelectListItem item)
        {
            // Reqête sql pour lister les clients
            var req = "SELECT [idCustomer], [LastName], [FirstName], [Mail], [PhoneNumber], [Budget], [Subject] " +
                "FROM [dbo].[Customers] " +
                "ORDER BY [LastName] ASC";
            var list = db.Customers.SqlQuery(req);
            // Fin de la Reqête sql pour lister les clients
            //*//
            // Déclaration des variables
            var compte = db.Customers.Count();//Nombre total de clients dans la base de données
            var nbcust = 5;// Nombre de clients par page
            double value = compte / nbcust; // Total client / nb clients a afficher par page
            var nbpage = Math.Ceiling(value) + 1; // Nombre de pages
            //*//
            //Récupère la valeur selectionée dans la liste
            var selectedValue = Request.Form["drp"].ToString();
            int j = Convert.ToInt32(selectedValue); // Conversion de la valeur en integer
            var t = j * nbcust;// Variable pour prendre t éléments de la base de données
            var s = j * nbcust - nbcust;// Variable pour omettre s éléments de la base de données
            //*//
            // Création de la liste des pages
            var listnumber = new List<SelectListItem>();
            for (var n = 1; n <= nbpage; n++)
                listnumber.Add(new SelectListItem { Text = n.ToString(), Value = n.ToString() });
            ViewBag.listnumber = listnumber;
            // Fin création liste
            //*//
            //Retour de la vue
            return View("PagedList", list.Take(t).Skip(s));
        }
        public ActionResult PagedList()
        {
           return View("PagedList");
        }
        [ChildActionOnly] // Vue Partielle pour la pagination
        public ActionResult Pager()
        {
          return PartialView("Pager");
        }
    }
}
