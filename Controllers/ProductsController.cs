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
    public class ProductsController : Controller
    {
        //database model declaration
        private DatabaseModel db = new DatabaseModel();

       
        //method to view farmer specific products to farmer
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Client);
            var productsList = products.ToList();
            var farmerList = productsList.Where(x => x.Client_ID == Int32.Parse(Session["ClientID"].ToString()));
            return View(farmerList);
        }

        //method to display user specif products to employee
        public ActionResult ViewFarmerProducts(int? id, string sortOrder, string searchString)
        {
            Client client = db.Clients.Find(id);

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";


            var products = db.Products.Include(p => p.Client);
            var productsList = products.ToList();
            var farmerList = productsList.Where(x => x.Client_ID == Constants.selectedID);


            //Adapted from: https://docs.microsoft.com/en-us/aspnet/whitepapers/aspnet-data-access-content-map
            //Microsoft
            //2022

            var productSortedList = from s in farmerList
                           select s;


            //searching of products by product type
            if (!String.IsNullOrEmpty(searchString))
            {
                productSortedList = productSortedList.Where(s => s.Product_Type.ToLower().Contains(searchString.ToLower()));
            }
            //sorting of products
            switch (sortOrder)
            {
                case "name_desc":
                    productSortedList = productSortedList.OrderByDescending(s => s.Product_Type);
                    break;
                case "Date":
                    productSortedList = productSortedList.OrderBy(s => s.Product_Date);
                    break;
                case "date_desc":
                    productSortedList = productSortedList.OrderByDescending(s => s.Product_Date);
                    break;
                default:
                    productSortedList = productSortedList.OrderBy(s => s.Product_Type);
                    break;
            }


            return View(productSortedList);


        }



        //method to get product details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //method to get product details for creation
        public ActionResult Create()
        {
            ViewBag.Client_ID = new SelectList(db.Clients, "Client_ID", "Client_Name");
            return View();
        }

        //mthod to create product
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Product_ID,Product_Name,Product_Type,Product_Date,Product_Price,Client_ID")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.Client_ID = Int32.Parse(Session["ClientID"].ToString());
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Client_ID = new SelectList(db.Clients, "Client_ID", "Client_Name", product.Client_ID);

            return View(product);
        }

        
        //method to get edit product details
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.Client_ID = new SelectList(db.Clients, "Client_ID", "Client_Name", product.Client_ID);
            return View(product);
        }

        //method to post editted product to database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Product_ID,Product_Name,Product_Type,Product_Date,Product_Price,Client_ID")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Client_ID = new SelectList(db.Clients, "Client_ID", "Client_Name", product.Client_ID);
            return View(product);
        }

        //method to get deleted products details
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //method to remove product from database
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
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
