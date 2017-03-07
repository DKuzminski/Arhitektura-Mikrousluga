using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DiplomskiProject_Client.Models
{
    public class Employee
    {
        [Display(Name ="Employee ID")]
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Ime { get; set; }

        [Display(Name = "Last Name")]
        public string Prezime { get; set; }

        [Display(Name = "Address")]
        public string Adresa { get; set; }

        [Display(Name = "City")]
        public string Grad { get; set; }

        [Display(Name = "Employee role ID")]
        public int RoleID { get; set; }
    }
}