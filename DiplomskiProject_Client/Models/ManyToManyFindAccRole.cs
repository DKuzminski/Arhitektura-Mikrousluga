using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DiplomskiProject_Client.Models
{
    public class ManyToManyFindAccRole
    {
        [Display(Name = "Account ID")]
        public int AccID { get; set; }

        [Display(Name = "Account Name")]
        public string ImeAccounta { get; set; }

        [Display(Name = "Account type")]
        public string TipAccounta { get; set; }

        [Display(Name = "Account role ID")]
        public int RoleID { get; set; }
    }
}