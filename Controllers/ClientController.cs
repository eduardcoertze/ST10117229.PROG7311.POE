using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ST10117229.PROG7311.POE.Models;

namespace ST10117229.PROG7311.POE.Controllers
{
    public class ClientController : Controller
    {

        //Method to Get from database client table
        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            Client clientModel = new Client();
            return View(clientModel);
        }



        [HttpPost]
        public ActionResult AddOrEdit(Client clientModel)
        {

            Util u1 = new Util();

            //post to database Client table
            using (DatabaseModel dbmodel = new DatabaseModel())
            {
                clientModel.Client_Password = u1.hashPassword(clientModel.Client_Password);
                clientModel.Is_Farmer = false;
                dbmodel.Clients.Add(clientModel);
                dbmodel.SaveChanges();
                ModelState.Clear();

                //viewbag for success
                ViewBag.SuccessMessage = "Registration Successful";
                return View("AddOrEdit", new Client());
            }



        }

        //method to get farmer details from database client table
        [HttpGet]
        public ActionResult AddFarmer(int id = 0)
        {
            Client clientModel = new Client();
            return View(clientModel);
        }


        //method to post farmer details to client database table during registration
        [HttpPost]
        public ActionResult AddFarmer(Client clientModel)
        {

            Util u1 = new Util();

            //post to database Client table
            using (DatabaseModel dbmodel = new DatabaseModel())
            {
                clientModel.Client_Password = u1.hashPassword(clientModel.Client_Password);
                clientModel.Is_Farmer = true;
                dbmodel.Clients.Add(clientModel);
                dbmodel.SaveChanges();
                ModelState.Clear();

                //viewbag for success
                ViewBag.SuccessMessage = "Farmer Registration Successful";
                return View("AddFarmer", new Client());
            }



        }

    }
}