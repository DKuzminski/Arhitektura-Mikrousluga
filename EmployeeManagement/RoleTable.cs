//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EmployeeManagement
{
    using System;
    using System.Collections.Generic;
    
    public partial class RoleTable
    {
        public RoleTable()
        {
            this.EmployeeTables = new HashSet<EmployeeTable>();
        }
    
        public int RoleID { get; set; }
        public string ImeRole { get; set; }
    
        public virtual ICollection<EmployeeTable> EmployeeTables { get; set; }
    }
}
