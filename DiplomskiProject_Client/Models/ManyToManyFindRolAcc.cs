using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DiplomskiProject_Client.Models
{
    public class ManyToManyFindRolAcc
    {
        [Display(Name = "Role ID")]
        public int RoleID { get; set; }

        [Display(Name = "Role Name")]
        public string ImeRole { get; set; }
    }
}