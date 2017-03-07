using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DiplomskiProject_Client.Models;

namespace DiplomskiProject_Client.ViewModels
{
    public class AccountAssignmentViewModel
    {
        public Employee Employee { get; set; }

        public EmpAccMapp EmpAccMapp { get; set; }

        public Account Account { get; set; }

        public ManyToManyEmpAcc ManyToManyEmpAcc { get; set; }
    }
}