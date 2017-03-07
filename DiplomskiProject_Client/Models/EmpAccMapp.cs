using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DiplomskiProject_Client.Models
{
    public class EmpAccMapp
    {
        [Display(Name = "Employee ID")]
        public int ZapId { get; set; }

        [Display(Name = "Account ID")]
        public int AccId { get; set; }

        [Display(Name = "Description")]
        public string Opis { get; set; }
    }
}