using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ST10117229.PROG7311.POE.Models;

namespace ST10117229.PROG7311.POE.Controllers
{
    public class LoginController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }

        //Http post for authorize
        [HttpPost]
        public ActionResult authorize(Client clientModel)
        {
            Util s1 = new Util();

            using (DatabaseModel db = new DatabaseModel())
            {

                string hashPassword = "";
                
                if(clientModel.Client_Password != null)
                {
                    //hashing of client's password
                    hashPassword = s1.hashPassword(clientModel.Client_Password);
                }
                

                //Adapted from: https://www.youtube.com/watch?v=EyrKUSwi4uI&t=2331s&ab_channel=CodAffection
                //CodAffection
                //2017
                var studentDetails = db.Clients.Where(x => x.Client_Email == clientModel.Client_Email && x.Client_Password.ToString() == hashPassword).FirstOrDefault();
                if (studentDetails == null)
                {
                    clientModel.LoginErrorMessage = "Wrong Username or Password";
                    return View("Index", clientModel);
                }
                else if(studentDetails.Is_Farmer == false)
                {
                    //session creation for client id
                    Session["ClientID"] = studentDetails.Client_ID;

                    return RedirectToAction("Index", "Farmer");

                }
                else
                {
                    //session creation for client id
                    Session["ClientID"] = studentDetails.Client_ID;

                    return RedirectToAction("Index", "Products");
                }
            }
        }
    }
}