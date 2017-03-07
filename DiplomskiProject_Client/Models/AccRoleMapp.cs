using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DiplomskiProject_Client.Models
{
    public class AccRoleMapp
    {
        [Display(Name = "Account ID")]
        public int AccId { get; set; }

        [Display(Name = "Role ID")]
        public int RoleId { get; set; }
    }
}