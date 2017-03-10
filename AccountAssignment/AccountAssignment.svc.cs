using AccountManagement;
using EmployeeManagement;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web.Script.Serialization;

namespace AccountAssignment
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AccountAssignment" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select AccountAssignment.svc or AccountAssignment.svc.cs at the Solution Explorer and start debugging.
    public class AccountAssignment : IAccountAssignment
    {
        public string BASE_URL_EMPMAN = "http://localhost:48757/EmployeeManagement.svc/";
        public string BASE_URL_ASSIGN = "http://localhost:17636/AccountAssignment.svc/";
        public string BASE_URL_ACCMAN = "http://localhost:49443/AccountManagement.svc/";

        // PRONAĐI SVE RAČUNE
        public List<EmpAccMapp> findEmpAcc()
        {
            using (BazaAccAssignEntities ben = new BazaAccAssignEntities())
            {
                return ben.EmpAccMappTables.Select(at => new EmpAccMapp
                {
                    ZapId = at.ZaposlenikID,
                    AccId = at.AccID
                }).ToList();
            }
        }

        // PRONAĐI SVE RAČUNE POJEDINOG ZAPOSLENIKA
        public List<ManyToManyEmpAcc> findAllAccForEmp(string id)
        {
            using (BazaAccAssignEntities ben = new BazaAccAssignEntities())
            {
                int cid = Convert.ToInt32(id);

                var x = from b in ben.AccountTables
                        join a in ben.EmpAccMappTables on b.AccID equals a.AccID
                        where a.ZaposlenikID == cid
                        select new ManyToManyEmpAcc { AccID = b.AccID, ImeAcc = b.ImeAcc, TipAcc = b.TipAcc };
                return x.ToList();
            }
        }


        // ADD ACCOUNT TO EMPLOYEE
        public bool addAccToEmployee(EmpAccMapp empaccmapp)
        {
            
            string findAccURI = "findallaccbyrol/{0}";
            string zajednoURI = BASE_URL_ACCMAN + findAccURI;

            
            var webclient = new WebClient();
            string url = string.Format(zajednoURI, empaccmapp.RoleId);
            var json = webclient.DownloadString(url);
            var js = new JavaScriptSerializer();
            List<ManyToManyFindAccRole> listaIspis = js.Deserialize<List<ManyToManyFindAccRole>>(json);

            using (BazaAccAssignEntities ben = new BazaAccAssignEntities())
            {
                try
                {
                    List<EmpAccMappTable> eamList = new List<EmpAccMappTable>();
                    foreach (var obj in listaIspis)
                    {
                        EmpAccMappTable eam = new EmpAccMappTable();
                        eam.ZaposlenikID = empaccmapp.ZapId;
                        eam.AccID = obj.AccID;
                        eamList.Add(eam);            
                    }
                    ben.EmpAccMappTables.AddRange(eamList);
                    ben.SaveChanges();

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }


        // UKLONI RAČUN OD ZAPOSLENIKA
        public bool removeAccFromEmployee(EmpAccMapp empaccmapp)
        {
            string findAccURI = "findallaccbyrol/{0}";
            string zajednoURI = BASE_URL_ACCMAN + findAccURI;

            var webclient = new WebClient();
            string url = string.Format(zajednoURI, empaccmapp.RoleId);
            var json = webclient.DownloadString(url);
            var js = new JavaScriptSerializer();
            List<ManyToManyFindAccRole> listaIspis = js.Deserialize<List<ManyToManyFindAccRole>>(json);

            using (BazaAccAssignEntities ben = new BazaAccAssignEntities())
            {
                try
                {
                    List<EmpAccMappTable> eamList = new List<EmpAccMappTable>();
                    int cid = Convert.ToInt32(empaccmapp.ZapId);
                    foreach (var obj in listaIspis)
                    {
                        EmpAccMappTable eamt = new EmpAccMappTable();

                        ben.EmpAccMappTables.RemoveRange(ben.EmpAccMappTables.Where(eam => eam.ZaposlenikID == cid));
                        ben.SaveChanges();
                    }

                    ben.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
