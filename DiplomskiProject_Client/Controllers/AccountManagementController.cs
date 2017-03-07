using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DiplomskiProject_Client.Models;
using DiplomskiProject_Client.ViewModels;
using System.IO;

namespace DiplomskiProject_Client.Controllers
{
    public class AccountManagementController : Controller
    {
        // IPSIŠI SVE RAČUNE U TABLICI
        public ActionResult IndexAccount()
        {
            AccountManagementServiceClient amsc = new AccountManagementServiceClient();
            ViewBag.listAccounts = amsc.findAllAcc();
            return View();
        }





        // KREIRANJE NOVOG RAČUNA
        [HttpGet]
        public ActionResult CreateAccount()
        {
            return View("CreateAccount");
        }

        [HttpPost]
        public ActionResult CreateAccount(AccountManagementViewModel amvm)
        {
            AccountManagementServiceClient amsc = new AccountManagementServiceClient();
            amsc.createAcc(amvm.Account);
            return RedirectToAction("IndexAccount");
        }




        // BRISANJE RAČUNA
        public ActionResult DeleteAccount(string id)
        {
            AccountManagementServiceClient amsc = new AccountManagementServiceClient();
            amsc.deleteAcc(amsc.findAcc(id));
            return RedirectToAction("IndexAccount");
        }







        // EDIT RAČUNA
        [HttpGet]
        public ActionResult UpdateAccountData(string id)
        {
            AccountManagementServiceClient amsc = new AccountManagementServiceClient();
            AccountManagementViewModel amvm = new AccountManagementViewModel();
            amvm.Account = amsc.findAcc(id);
            return View("UpdateAccountData", amvm);
        }

        [HttpPost]
        public ActionResult UpdateAccountData(AccountManagementViewModel amvm)
        {
            AccountManagementServiceClient amsc = new AccountManagementServiceClient();
            amsc.updateAccData(amvm.Account);
            return RedirectToAction("IndexAccount");
        }



        //PRIKAZ JEDNOG ZAPOSLENIKA
        [HttpGet]
        public ActionResult ShowAccountData(string id)
        {
            AccountManagementServiceClient amsc = new AccountManagementServiceClient();
            AccountManagementViewModel amvm = new AccountManagementViewModel();
            amvm.Account = amsc.findAcc(id);
            return View("ShowAccountData", amvm);
        }




        // OVO JE DIO CHECKBOX-A U MVC-U
        // PRIKAZ SVIH RAČUNA VEZANIH ZA POJEDINU ROLU
        [HttpGet]
        public ActionResult GetAccForRol(string id)
        {
            AccountManagementServiceClient amsc = new AccountManagementServiceClient();
            ViewBag.listAccForRol = amsc.findAllAccByRol(id);
            return View();

        }

        [HttpPost]
        public ActionResult GetAccForRol(List<string> Podaci)
        {

            StreamWriter fsf = new StreamWriter("C:\\Users\\Dodo\\Downloads\\Diplomski\\provjera.log");
            fsf.WriteLine("primljeni parametri:");
            foreach(var obj in Podaci)
            {
                fsf.WriteLine("employee ID: " + obj);
                fsf.Flush();
            }
            
            fsf.Close();
            /*
            //System.Diagnostics.Debug.WriteLine(ids); 
            AccountManagementServiceClient amsc = new AccountManagementServiceClient();
            AccountManagementViewModel amvm = new AccountManagementViewModel();

            foreach(var item in Podaci)
            {
                if(collection != null)
                {
                string objekt = item.ToString();
                var accounti = amsc.findAccRoleMapp(objekt);
                amsc.removeAccFromRole(accounti);
                }
            }*/
            return RedirectToAction("GetAccForRol");
        }

       


        /*
        [HttpPost]
        public ActionResult GetAccForRol(FormCollection formCollection)
        {
            //System.Diagnostics.Debug.WriteLine(ids); // ovo dodaj za ispis sadržaja u createEmp i roleChange
            AccountManagementServiceClient amsc = new AccountManagementServiceClient();
            AccountManagementViewModel amvm = new AccountManagementViewModel();

            if (formCollection != null)
            {
                string[] ids = formCollection["accId"].Split(new char[] {','});

                if (ids != null || ids.Length != 0)
                {
                    foreach (string id in ids)
                    {
                        var accounti = amsc.findAccRoleMapp(id);
                        amsc.removeAccFromRole(accounti);

                    }
                }
            }

            return RedirectToAction("GetAccForRol");
        }*/
        




        // PRIKAZ SVIH ROLA VEZANIH UZ RAČUN
        [HttpGet]
        public ActionResult GetRolForAcc(string id)
        {
            AccountManagementServiceClient amsc = new AccountManagementServiceClient();
            ViewBag.listRolForAcc = amsc.findAllRolByAcc(id);
            return View();
        }




        // ADD ROLE TO ACCOUNT
        [HttpGet]
        public ActionResult AddRoleToAccount()
        {
            return View("AddRoleToAccount");
        }

        [HttpPost]
        public ActionResult AddRoleToAccount(AccountManagementViewModel amvm) 
        {
            AccountManagementServiceClient amsc = new AccountManagementServiceClient();
            amsc.addRoleToAcc(amvm.AccRoleMapp);
            return RedirectToAction("IndexAccount");
        }



        // REMOVE ROLE FROM ACCOUNT
        public ActionResult RemoveRoleFromAccount(string id)
        {
            AccountManagementServiceClient amsc = new AccountManagementServiceClient();
            amsc.removeRoleFromAcc(amsc.findAccRoleMapp(id));
            return RedirectToAction("IndexAccount");
        }
















        /*
        // PRIKAZ SVIH RAČUNA VEZANIH ZA POJEDINU ROLU (EDIT ACCOUNTS FOR ROLE CHECKBOX)
        [HttpGet]
        public ActionResult FindAccountForRole(string id)
        {
            AccountManagementServiceClient amsc = new AccountManagementServiceClient();
            ViewBag.listEditAccForRole = amsc.findAccsForRole(id);
            return View();
        }*/

















        // ADD ACCOUNT TO ROLE
        [HttpGet]
        public ActionResult AddaccountToRole() 
        {
            return View("AddaccountToRole");
        }

        [HttpPost]
        public ActionResult AddaccountToRole(AccountManagementViewModel amvm)
        {
            AccountManagementServiceClient amsc = new AccountManagementServiceClient();
            amsc.addAccToRole(amvm.AccRoleMapp);
            return RedirectToAction("IndexAccount");
        }



        // REMOVE ACCOUNT FROM ROLE
        public ActionResult RemoveAccountFromRole(string accID)
        {
            AccountManagementServiceClient amsc = new AccountManagementServiceClient();
            amsc.removeRoleFromAcc(amsc.findAccRoleMapp(accID));
            return RedirectToAction("IndexAccount");
        }

    }
}