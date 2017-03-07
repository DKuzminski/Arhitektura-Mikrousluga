using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DiplomskiProject_Client.Models
{
    public class Account
    {
        [Display(Name = "Account ID")]
        public int Id { get; set; }

        [Display(Name = "Account Name")]
        public string Ime { get; set; }

        [Display(Name = "Account type")]
        public string Tip { get; set; }

        [Display(Name = "Created on")]
        public DateTime? Kreirano { get; set; }

        [Display(Name = "Last modified")]
        public DateTime? Izmjena { get; set; }
    }
}