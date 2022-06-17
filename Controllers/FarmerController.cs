using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ST10117229.PROG7311.POE.Models;

namespace ST10117229.PROG7311.POE.Controllers
{
    public class FarmerController : Controller
    {
        //database model declaration
        private DatabaseModel db = new DatabaseModel();

        
        //method to recieve data on farmer client from database
        public ActionResult Index()
        {

            var clientList = db.Clients.ToList();

            var farmerList = clientList.Where(x => x.Is_Farmer == true);


            return View(farmerList);
        }






        //method to display farmer specific products
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }

            Constants.selectedID = client.Client_ID;

            return RedirectToAction("ViewFarmerProducts","Products");
        }

        
        public ActionResult Create()
        {
            return View();
        }

        
        //method for the create a farmer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Client_ID,Is_Farmer,Client_Name,Client_Surname,Client_Email,Client_Password")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Clients.Add(client);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(client);
        }

        //method to get farmer details to allow for editting
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);

            

            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        //method to edit farmer details
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Client_ID,Is_Farmer,Client_Name,Client_Surname,Client_Email,Client_Password")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(client);
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
