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


        /*
        // ROLECHANGE
        public bool roleChange(string id)
        {
            
            AccountManagement.AccountManagement accMan = new AccountManagement.AccountManagement();
            EmployeeManagement.EmployeeManagement empMan = new EmployeeManagement.EmployeeManagement();

            var findEmp = empMan.findEmp(id);
            return empMan.editOnlyEmpRol(findEmp);

            
            var findRole = empMan.findRol(id);
            string IDrole = findRole.ToString();

            var getAccForRole = accMan.findAllAccByRol(IDrole);
            addAccToEmployee();
            
        }

        
        // ROLECHANGE
        public bool roleChange(string idZap, string idRole)
        {
            AccountManagement.AccountManagement accMan = new AccountManagement.AccountManagement();
            EmployeeManagement.EmployeeManagement empMan = new EmployeeManagement.EmployeeManagement();

            var findID = empMan.findEmp(idZap);
            empMan.editOnlyEmpRol(findID);


            var findRole = empMan.findRol(idRole);
            string IDrole = findRole.ToString();

            var getAccForRole = accMan.findAllAccByRol(IDrole);
            addAccToEmployee(findID);

            return true;

        }*/


        /*
        // DODAJ RAČUN ZAPOSLENIKU
        public bool addAccToEmployee(EmpAccMapp empaccmapp)
        {
            using (BazaAccAssignEntities ben = new BazaAccAssignEntities())
            {
                try
                {
                    EmpAccMappTable eamt = new EmpAccMappTable();
                    eamt.ZaposlenikID = empaccmapp.ZapId;
                    eamt.AccID = empaccmapp.AccId;
                    ben.EmpAccMappTables.Add(eamt);
                    ben.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }*/


        // ADD ACCOUNT TO EMPLOYEE
        public bool addAccToEmployee(EmpAccMapp empaccmapp)
        {
            //Console.SetOut(new System.IO.StreamWriter("e:\\assign.log"));
            //System.Diagnostics.Debug.WriteLine(employeeID + "TEST" + newRoleID);
            //System.Diagnostics.Debug.WriteLine(employeeID.ToString() + "proslijeđeno od rolechange metode" + newRoleID.ToString());
            //string BASE_URL = "http://localhost:25006/AccountManagement.svc/";


            //StreamWriter fsf = new StreamWriter("C:\\Users\\Dodo\\Downloads\\Diplomski\\assignAccount.log");
            //fsf.WriteLine("primljeni parametri:");
            //fsf.WriteLine("employee ID: " + empaccmapp.ZapId);
            //fsf.WriteLine("role ID: " + empaccmapp.RoleId);
            //fsf.Flush();
            //fsf.Close();
            
            
            string findAccURI = "findallaccbyrol/{0}";
            string zajednoURI = BASE_URL_ACCMAN + findAccURI;

            
            var webclient = new WebClient();
            string url = string.Format(zajednoURI, empaccmapp.RoleId);
            var json = webclient.DownloadString(url);
            var js = new JavaScriptSerializer();
            List<ManyToManyFindAccRole> listaIspis = js.Deserialize<List<ManyToManyFindAccRole>>(json);

            //int broj = 99;
            //fsf.WriteLine("test: " + broj);
            //fsf.WriteLine("listaispis: " + listaIspis);
            //fsf.Flush();

            
            using (BazaAccAssignEntities ben = new BazaAccAssignEntities())
            {
                try
                {
                    List<EmpAccMappTable> eamList = new List<EmpAccMappTable>();
                    foreach (var obj in listaIspis)
                    {
                        //int objekt = Convert.ToInt32(obj);
                        EmpAccMappTable eam = new EmpAccMappTable();
                        eam.ZaposlenikID = empaccmapp.ZapId;
                        eam.AccID = obj.AccID;
                        eamList.Add(eam);
                      
                        //ben.EmpAccMappTables.Add(eam);                       
                    }
                    //fsf.WriteLine("eamList: ", eamList);
                    //fsf.Flush();
                    ben.EmpAccMappTables.AddRange(eamList);
                    ben.SaveChanges();
                   
                   // fsf.Close();
                    return true;
                }
                catch
                {
                    //fsf.Close();
                    return false;
                }


            }

            
            //return true;

        }


        // UKLONI RAČUN OD ZAPOSLENIKA
        public bool removeAccFromEmployee(EmpAccMapp empaccmapp)
        {
            StreamWriter fsf = new StreamWriter("C:\\Users\\Dodo\\Downloads\\Diplomski\\removeAccFromEmployee.log");
            fsf.WriteLine("primljeni parametri:");
            fsf.WriteLine("employee ID: " + empaccmapp.ZapId);
            fsf.WriteLine("role ID: " + empaccmapp.RoleId);
            fsf.Flush();

            //string BASE_URL = "http://localhost:25006/AccountManagement.svc/";
            string findAccURI = "findallaccbyrol/{0}";
            string zajednoURI = BASE_URL_ACCMAN + findAccURI;

           

            var webclient = new WebClient();
            string url = string.Format(zajednoURI, empaccmapp.RoleId);
            var json = webclient.DownloadString(url);
            var js = new JavaScriptSerializer();
            List<ManyToManyFindAccRole> listaIspis = js.Deserialize<List<ManyToManyFindAccRole>>(json);

            /*
            foreach(var obje in listaIspis)
            {
                fsf.WriteLine("iz liste " + obje.AccID);
                fsf.Flush();
            }*/
            

            using (BazaAccAssignEntities ben = new BazaAccAssignEntities())
            {
                try
                {
                    List<EmpAccMappTable> eamList = new List<EmpAccMappTable>();
                    int cid = Convert.ToInt32(empaccmapp.ZapId);
                    foreach (var obj in listaIspis)
                    {
                        //int objekt = Convert.ToInt32(obj);
                        //fsf.WriteLine("iz liste: " + obj.AccID);
                        //fsf.Flush();
                        
                        //EmpAccMappTable eamt = ben.EmpAccMappTables.Single(ea => ea.ZaposlenikID == objekt && ea.AccID == objekt);
                        
                        EmpAccMappTable eamt = new EmpAccMappTable();

                        ben.EmpAccMappTables.RemoveRange(ben.EmpAccMappTables.Where(eam => eam.ZaposlenikID == cid));
                        ben.SaveChanges();
                       // eamt.ZaposlenikID = empaccmapp.ZapId;
                        //eamt.AccID = obj.AccID;
                        //eamList.Add(eamt);
                        
                    }

                    //ben.EmpAccMappTables.RemoveRange(eamList);
                    ben.SaveChanges();


                    fsf.Close();
                    return true;
                }
                catch
                {
                    fsf.Close();
                    return false;
                }
            }
        }



    }
}
