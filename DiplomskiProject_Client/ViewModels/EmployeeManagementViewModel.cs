using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DiplomskiProject_Client.Models;

namespace DiplomskiProject_Client.ViewModels
{
    public class EmployeeManagementViewModel
    {
        public Employee Employee { get; set; }

        public Role Role { get; set; }
    }
}