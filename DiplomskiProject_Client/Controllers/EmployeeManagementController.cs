using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DiplomskiProject_Client.Models;
using DiplomskiProject_Client.ViewModels;

namespace DiplomskiProject_Client.Controllers
{
    public class EmployeeManagementController : Controller
    {
        // ISPIS SVIH ZAPOSLENIKA
        public ActionResult IndexEmployee()
        {
            EmployeeManagementServiceClient emsc = new EmployeeManagementServiceClient();
            ViewBag.listEmployees = emsc.findAllEmp();
            return View();
        }




        // KREIRANJE NOVOG ZAPOSLENIKA
        [HttpGet]
        public ActionResult CreateEmp()
        {
            return View("CreateEmp");
        }

        [HttpPost]
        public ActionResult CreateEmp(EmployeeManagementViewModel emvm)
        {
            //try
            //{
                EmployeeManagementServiceClient emsc = new EmployeeManagementServiceClient();
                emsc.createEmp(emvm.Employee);
                return RedirectToAction("IndexEmployee");
           /* }
            catch(Exception e)
            {
                return View("Error");
            }*/
        }



        // BRISANJE ZAPOSLENIKA
        public ActionResult DeleteEmp(string id)
        {
            EmployeeManagementServiceClient emsc = new EmployeeManagementServiceClient();
            emsc.deleteEmp(emsc.findEmp(id));
            return RedirectToAction("IndexEmployee");
        }




        // UREDI PODATKE ZAPOSLENIKA
        [HttpGet]
        public ActionResult EditEmp(string id)
        {
            EmployeeManagementServiceClient emsc = new EmployeeManagementServiceClient();
            EmployeeManagementViewModel emvm = new EmployeeManagementViewModel();
            emvm.Employee = emsc.findEmp(id);
            return View("EditEmp", emvm);
        }

        [HttpPost]
        public ActionResult EditEmp(EmployeeManagementViewModel emvm)
        {
            EmployeeManagementServiceClient emsc = new EmployeeManagementServiceClient();
            emsc.editEmp(emvm.Employee);
            return RedirectToAction("IndexEmployee");
        }





        // UREDI SAMO ROLU ZAPOSLENIKA
        [HttpGet]
        public ActionResult EditOnlyEmpRole(string id)
        {
            EmployeeManagementServiceClient emsc = new EmployeeManagementServiceClient();
            EmployeeManagementViewModel emvm = new EmployeeManagementViewModel();
            emvm.Employee = emsc.findEmp(id);
            return View("EditOnlyEmpRole", emvm);
        }

        [HttpPost]
        public ActionResult EditOnlyEmpRole(EmployeeManagementViewModel emvm)
        {
            EmployeeManagementServiceClient emsc = new EmployeeManagementServiceClient();
            emsc.editOnlyEmpRole(emvm.Employee);
            return RedirectToAction("IndexEmployee");
        }





        // PRIKAŽI PODATKE SAMO JEDNOG ZAPOSLENIKA
        [HttpGet]
        public ActionResult ShowEmpData(string id)
        {
            EmployeeManagementServiceClient emsc = new EmployeeManagementServiceClient();
            EmployeeManagementViewModel emvm = new EmployeeManagementViewModel();
            emvm.Employee = emsc.findEmp(id);
            return View("ShowEmpData", emvm);
        }





        // PRIKAZ SVIH ZAPOSLENIKA KOJI SU VEZANI UZ JEDNU ROLU
        [HttpGet]
        public ActionResult GetEmpForRole(string id)
        {
            EmployeeManagementServiceClient emsc = new EmployeeManagementServiceClient();
            ViewBag.listEmpForRol = emsc.findAllEmpByRol(id);
            return View();
        }












//////////// ROLE DIO //////////////////////////////////////////////////////


        // PRIKAZ SVIH ROLA
        public ActionResult IndexRole()
        {
            EmployeeManagementServiceClient emsc = new EmployeeManagementServiceClient();
            ViewBag.listRoles = emsc.findAllRol();
            return View();
        }


        // KREIRAJ NOVU ROLU
        [HttpGet]
        public ActionResult CreateRole()
        {
            return View("CreateRole");
        }

        [HttpPost]
        public ActionResult CreateRole(EmployeeManagementViewModel emvm)
        {
            EmployeeManagementServiceClient emsc = new EmployeeManagementServiceClient();
            emsc.createRol(emvm.Role);
            return RedirectToAction("IndexRole");
        }




        // BRISANJE ROLE
        public ActionResult DeleteRole(string id)
        {
            EmployeeManagementServiceClient emsc = new EmployeeManagementServiceClient();
            emsc.deleteRol(emsc.findRol(id));
            return RedirectToAction("IndexRole");
        }



        // UREDI ROLU
        [HttpGet]
        public ActionResult EditRole(string id)
        {
            EmployeeManagementServiceClient emsc = new EmployeeManagementServiceClient();
            EmployeeManagementViewModel emvm = new EmployeeManagementViewModel();
            emvm.Role = emsc.findRol(id);
            return View("EditRole", emvm);
        }

        [HttpPost]
        public ActionResult EditRole(EmployeeManagementViewModel emvm)
        {
            EmployeeManagementServiceClient emsc = new EmployeeManagementServiceClient();
            emsc.editRol(emvm.Role);
            return RedirectToAction("IndexRole");
        }



        // PODACI SAMO JEDNE ROLE
        [HttpGet]
        public ActionResult ShowRolData(string id)
        {
            EmployeeManagementServiceClient emsc = new EmployeeManagementServiceClient();
            EmployeeManagementViewModel emvm = new EmployeeManagementViewModel();
            emvm.Role = emsc.findRol(id);
            return View("ShowRolData", emvm);
        }




///////////////////////////////////////// ROLECHANGE ///////////////////////////////////////
        [HttpGet]
        public ActionResult RoleChange(string id)
        {
            EmployeeManagementServiceClient emsc = new EmployeeManagementServiceClient();
            EmployeeManagementViewModel emvm = new EmployeeManagementViewModel();
            emvm.Employee = emsc.findEmp(id);
            return View("RoleChange", emvm);

        }
        [HttpPost]
        public ActionResult RoleChange(EmployeeManagementViewModel emvm)
        {
            EmployeeManagementServiceClient emsc = new EmployeeManagementServiceClient();
            emsc.roleChange(emvm.Employee);
            /*
            AccountAssignmentServiceClient aasc = new AccountAssignmentServiceClient();
            aasc.addAccToEmployee(aavm.EmpAccMapp);
            aasc.removeAccFromEmployee(aavm.EmpAccMapp);*/
            return RedirectToAction("IndexEmployee");
        }
    }
}