using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccountManagement
{
    public class Account
    {
        public int Id { get; set; }

        public string Ime { get; set; }

        public string Tip { get; set; }

        public DateTime? Kreirano { get; set; }

        public DateTime? Izmjena { get; set; }
    }
}