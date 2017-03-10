using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.ServiceModel;
using System.Text;
using System.Web.Script.Serialization;

namespace EmployeeManagement
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "EmployeeManagement" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select EmployeeManagement.svc or EmployeeManagement.svc.cs at the Solution Explorer and start debugging.

    public class EmployeeManagement : IEmployeeManagement
    {
        public string BASE_URL_EMPMAN = "http://localhost:48757/EmployeeManagement.svc/";
        public string BASE_URL_ASSIGN = "http://localhost:17636/AccountAssignment.svc/";
        public string BASE_URL_ACCMAN = "http://localhost:49443/AccountManagement.svc/";


        // PRONAĐI SVE ZAPOSLENIKE
        public List<Employee> findAllEmp()
        {
            using (BazaEmployeeEntities ben = new BazaEmployeeEntities())
            {
                return ben.EmployeeTables.Select(bt => new Employee
                {
                    Id = bt.ZaposlenikID,
                    Ime = bt.ImeZap,
                    Prezime = bt.PrezimeZap,
                    Adresa = bt.AdresaZap,
                    Grad = bt.GradZap,
                    RoleID = bt.RoleID
                }).ToList();
            }
        }

        // PRONAĐI SVE ROLE
        public List<Role> findAllRol()
        {
            using (BazaEmployeeEntities ben = new BazaEmployeeEntities())
            {
                return ben.RoleTables.Select(rt => new Role
                {
                    Id = rt.RoleID,
                    Ime = rt.ImeRole
                }).ToList();
            }
        }

        // PRONAĐI SVE ZAPOSLENIKE KOJI IMAJU ISTU ROLU
        public List<Employee> findAllEmpByRol(string id)
        {
            using (BazaEmployeeEntities ben = new BazaEmployeeEntities())
            {
                int cid = Convert.ToInt32(id);
                return ben.EmployeeTables.Where(et => et.RoleID == cid).Select(ete => new Employee
                {
                    Id = ete.ZaposlenikID,
                    Ime = ete.ImeZap,
                    Prezime = ete.PrezimeZap,
                    Adresa = ete.AdresaZap,
                    Grad = ete.GradZap,
                    RoleID = ete.RoleID
                }).ToList();
            }
        }

        // PRONAĐI SAMO ROLU ZAPOSLENIKA (ZA ROLECHANGE)
        public List<EmpRole> findRoleForEmp(string id)
        {
            using (BazaEmployeeEntities ben = new BazaEmployeeEntities())
            {
                int cid = Convert.ToInt32(id);

                var x = from b in ben.RoleTables
                        join a in ben.EmployeeTables on b.RoleID equals a.RoleID
                        where a.ZaposlenikID == cid
                        select new EmpRole { RoleId = a.RoleID };
                return x.ToList();
            }
        }

        // PRONAĐI POJEDINOG ZAPOSLENIKA
        public Employee findEmp(string id)
        {
            using (BazaEmployeeEntities ben = new BazaEmployeeEntities())
            {
                int cid = Convert.ToInt32(id);
                return ben.EmployeeTables.Where(et => et.ZaposlenikID == cid).Select(ete => new Employee
                {
                    Id = ete.ZaposlenikID,
                    Ime = ete.ImeZap,
                    Prezime = ete.PrezimeZap,
                    Adresa = ete.AdresaZap,
                    Grad = ete.GradZap,
                    RoleID = ete.RoleID
                }).First();
            }
        }

        // PRONAĐI ZAPOSLENIKA U EMPACCMAPP TABLICI (AKO BUDE POTREBNO)
        public EmpAccMapp findEmpAccMapp(string id)
        {
            using (BazaEmployeeEntities ben = new BazaEmployeeEntities())
            {
                int cid = Convert.ToInt32(id);
                return ben.EmpAccMappTables.Where(eam => eam.ZaposlenikID == cid).Select(eamt => new EmpAccMapp
                {
                    ZapId = eamt.ZaposlenikID,
                    AccId = eamt.AccID
                }).First();
            }
        }

        // PRONAĐI POJEDINU ROLU
        public Role findRol(string id)
        {
            using (BazaEmployeeEntities ben = new BazaEmployeeEntities())
            {
                int cid = Convert.ToInt32(id);
                return ben.RoleTables.Where(rt => rt.RoleID == cid).Select(rtr => new Role
                {
                    Id = rtr.RoleID,
                    Ime = rtr.ImeRole
                }).First();
            }
        }

        // KREIRAJ ZAPOSLENIKA
        public bool createEmp(Employee employee)
        {
            string uriAddAccToEmp = "addacctoemployee";
            string zajednoURI = BASE_URL_ASSIGN + uriAddAccToEmp;
            int empID = 0;
            int roleID = 0;

            using (BazaEmployeeEntities ben = new BazaEmployeeEntities())
            {
                 try
                 {
                     EmployeeTable emt = new EmployeeTable();
                     emt.ImeZap = employee.Ime;
                     emt.PrezimeZap = employee.Prezime;
                     emt.AdresaZap = employee.Adresa;
                     emt.GradZap = employee.Grad;
                     emt.RoleID = employee.RoleID;
                     ben.EmployeeTables.Add(emt);
                     ben.SaveChanges();

                     empID = emt.ZaposlenikID;
                     roleID = emt.RoleID;
                 }
                 catch
                 { 
                     return false;
                 }

             }

            // pozivanje AddAccountToEmployee
            EmpAccMapp empaccmapp = new EmpAccMapp(empID, roleID);

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(EmpAccMapp));
            MemoryStream mem = new MemoryStream(); ser.WriteObject(mem, empaccmapp);
            string data = Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);

            WebClient webClient = new WebClient();
            webClient.Headers["Content-type"] = "application/json";
            webClient.Encoding = Encoding.UTF8;
            webClient.UploadString(zajednoURI, "POST", data);

            return true;
        }

        // KREIRAJ ROLU
        public bool createRol(Role role)
        {
            using (BazaEmployeeEntities ben = new BazaEmployeeEntities())
            {
                try
                {
                    RoleTable rt = new RoleTable();
                    rt.ImeRole = role.Ime;
                    ben.RoleTables.Add(rt);
                    ben.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        // UREDI ZAPOSLENIKA (BEZ ROLE)
        public bool editEmp(Employee employee)
        {
            using (BazaEmployeeEntities ben = new BazaEmployeeEntities())
            {
                try
                {
                    int cid = Convert.ToInt32(employee.Id);
                    EmployeeTable et = ben.EmployeeTables.Single(ete => ete.ZaposlenikID == cid);
                    et.ImeZap = employee.Ime;
                    et.PrezimeZap = employee.Prezime;
                    et.AdresaZap = employee.Adresa;
                    et.GradZap = employee.Grad;
                    ben.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        // UREDI SAMO ROLU
        public bool editRol(Role role)
        {
            using (BazaEmployeeEntities ben = new BazaEmployeeEntities())
            {
                try
                {
                    int cid = Convert.ToInt32(role.Id);
                    RoleTable rt = ben.RoleTables.Single(rtr => rtr.RoleID == cid);
                    rt.ImeRole = role.Ime;
                    ben.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        // UREDI SAMO ROLU ZAPOSLENIKA
        public bool editOnlyEmpRol(Employee employee)
        {
            string prviURI = "removeaccfromemployee";
            string prviZaj = BASE_URL_ASSIGN + prviURI;

            string drugiURI = "addacctoemployee";
            string drugiZaj = BASE_URL_ASSIGN + drugiURI;

            int oldRole = 0;
            int newRoleID = 0;
            string newRolID = null;
            using (BazaEmployeeEntities ben = new BazaEmployeeEntities())
            {
                try
                {
                    int cid = Convert.ToInt32(employee.Id);
                    EmployeeTable et = ben.EmployeeTables.Single(ete => ete.ZaposlenikID == cid);
                    oldRole = et.RoleID;
                    et.RoleID = employee.RoleID;
                    ben.SaveChanges();
                    newRoleID = et.RoleID;
                    newRolID = newRoleID.ToString();
                }
                catch
                {
                    return false;
                }

                // pozivanje removeAccFromEmployee
                int employeeId = employee.Id; // id zaposlenika koji se prosljeđuje
                EmpAccMapp empaccmappDel = new EmpAccMapp(employeeId, oldRole);

                DataContractJsonSerializer serDel = new DataContractJsonSerializer(typeof(EmpAccMapp));
                MemoryStream memDel = new MemoryStream();
                serDel.WriteObject(memDel, empaccmappDel);
                string dataDel = Encoding.UTF8.GetString(memDel.ToArray(), 0, (int)memDel.Length);

                WebClient webClientDel = new WebClient();
                webClientDel.Headers["Content-type"] = "application/json";
                webClientDel.Encoding = Encoding.UTF8;
                webClientDel.UploadString(prviZaj, "DELETE", dataDel);

                // pozivanje addAccountToEmployee
                EmpAccMapp empaccmappAdd = new EmpAccMapp(employee.Id, employee.RoleID);

                DataContractJsonSerializer serAdd = new DataContractJsonSerializer(typeof(EmpAccMapp));
                MemoryStream memAdd = new MemoryStream();
                serAdd.WriteObject(memAdd, empaccmappAdd);
                string dataAdd = Encoding.UTF8.GetString(memAdd.ToArray(), 0, (int)memAdd.Length);

                WebClient webClientAdd = new WebClient();
                webClientAdd.Headers["Content-type"] = "application/json";
                webClientAdd.Encoding = Encoding.UTF8;
                webClientAdd.UploadString(drugiZaj, "POST", dataAdd);
                return true;
            }
        }

        // OBRIŠI JEDNOG ZAPOSLENIKA
        public bool deleteEmp(Employee employee)
        {
            using (BazaEmployeeEntities ben = new BazaEmployeeEntities())
            {
                try
                {
                    int cid = Convert.ToInt32(employee.Id);
                    EmployeeTable et = ben.EmployeeTables.Single(ete => ete.ZaposlenikID == cid);
                    ben.EmployeeTables.Remove(et);
                    ben.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        // OBRIŠI JEDNU ROLU
        public bool deleteRol(Role role)
        {
            using (BazaEmployeeEntities ben = new BazaEmployeeEntities())
            {
                try
                {
                    int cid = Convert.ToInt32(role.Id);
                    RoleTable rt = ben.RoleTables.Single(rtr => rtr.RoleID == cid);
                    ben.RoleTables.Remove(rt);
                    ben.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        // PRONAĐI POJEDINU ROLU ZA ROLECHANGE
        public RoleRoleChange findRolForRolechange(string id)
        {
            using (BazaEmployeeEntities ben = new BazaEmployeeEntities())
            {
                int cid = Convert.ToInt32(id);
                return ben.RoleTables.Where(rt => rt.RoleID == cid).Select(rtr => new RoleRoleChange
                {
                    Id = rtr.RoleID
                }).First();
            }
        }


        // ROLECHANGE
        public bool roleChange(Employee employee)
        {
            try
            {
                editOnlyEmpRol(employee);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

