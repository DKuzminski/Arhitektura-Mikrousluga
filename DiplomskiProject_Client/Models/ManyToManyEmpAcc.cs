using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DiplomskiProject_Client.Models
{
    public class ManyToManyEmpAcc
    {
        [Display(Name = "Account Name")]
        public string ImeAcc { get; set; }

        [Display(Name = "Account type")]
        public string TipAcc { get; set; }

        [Display(Name = "Account ID")]
        public int AccID { get; set; }
    }
}