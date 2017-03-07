using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DiplomskiProject_Client.Models
{
    public class Role
    {
        [Display(Name ="Role ID")]
        public int Id { get; set; }

        [Display(Name = "Role Name")]
        public string Ime { get; set; }
    }
}