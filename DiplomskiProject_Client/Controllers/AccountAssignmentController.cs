using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DiplomskiProject_Client.Models;
using DiplomskiProject_Client.ViewModels;

namespace DiplomskiProject_Client.Controllers
{
    public class AccountAssignmentController : Controller
    {

        // PRIKAŽI SVE RAČUNE NEKOG ZAPOSLENIKA
        [HttpGet]
        public ActionResult ShowAllAcc(string id)
        {
            AccountAssignmentServiceClient aas = new AccountAssignmentServiceClient();
            ViewBag.listAllAccForEmp = aas.findAllAccForEmp(id);
            return View();
        }
    }
}