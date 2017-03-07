using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DiplomskiProject_Client.Models;

namespace DiplomskiProject_Client.ViewModels
{
    public class AccountManagementViewModel
    {
        public Account Account { get; set; }

        public Role Role { get; set; }

        public AccRoleMapp AccRoleMapp { get; set; }

        public ManyToManyFindAccRole ManyToManyFindAccRole { get; set; }

        public ManyToManyFindRolAcc ManyToManyFindRolAcc { get;set;}
    }
}