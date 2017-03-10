using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagement
{
    public class EmpAccMapp
    {
        public int ZapId { get; set; }

        public int AccId { get; set; }

        public int RoleId { get; set; }

        public EmpAccMapp()
        { }

        // konstruktor za create employee (samo za empID)
        public EmpAccMapp(int zaposlenikId)
        {
            ZapId = zaposlenikId;
        }

        // konstruktor za removeAccFromEmployee (za empId i oldRoleID)
        public EmpAccMapp(int zaposlenikId, int oldRoleID)
        {
            ZapId = zaposlenikId;
            RoleId = oldRoleID;
        }
    }
}