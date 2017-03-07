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







        /*
        // premješteno u employeeManagement servis
        ////////////////////////////////// ROLECHANGE!!!!!!!!!!!!!!!///////////////////////////////////////
        [HttpGet]
        public ActionResult RoleChange(string idEmp)
        {
            EmployeeManagementServiceClient emsc = new EmployeeManagementServiceClient();
            EmployeeManagementViewModel emvm = new EmployeeManagementViewModel();

            emvm.Employee = emsc.findEmp(idEmp);
            return View("RoleChange", emvm);

        }
        [HttpPost]
        public ActionResult RoleChange(EmployeeManagementViewModel emvm, AccountAssignmentViewModel aavm)
        {
            EmployeeManagementServiceClient emsc = new EmployeeManagementServiceClient();
            emsc.editOnlyEmpRole(emvm.Employee);
            /*
            AccountAssignmentServiceClient aasc = new AccountAssignmentServiceClient();
            aasc.addAccToEmployee(aavm.EmpAccMapp);
            aasc.removeAccFromEmployee(aavm.EmpAccMapp);
            return RedirectToAction("IndexEmployee");
        }*/
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        


    }
}