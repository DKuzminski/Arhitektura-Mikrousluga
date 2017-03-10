using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace DiplomskiProject_Client.Models
{ 
    public class EmployeeManagementServiceClient
    {
        public string BASE_URL = "http://localhost:48757/EmployeeManagement.svc/";

        // PRONAĐI SVE ZAPOSLENIKE
        public List<Employee> findAllEmp()
        {
            try
            {
                var webclient = new WebClient();
                var json = webclient.DownloadString(BASE_URL + "findallemp");
                var js = new JavaScriptSerializer();
                return js.Deserialize<List<Employee>>(json);
            }
            catch
            {
                return null;
            }
        }


        // PRONAĐI SVE ROLE
        public List<Role> findAllRol()
        {
            try
            {
                var webclient = new WebClient();
                var json = webclient.DownloadString(BASE_URL + "findallrol");
                var js = new JavaScriptSerializer();
                return js.Deserialize<List<Role>>(json);
            }
            catch
            {
                return null;
            }
        }


        // PRONAĐI SVE ZAPOSLENIKE KOJI IMAJU ISTU ROLU
        public List<Employee> findAllEmpByRol(string id)
        {
            try
            {
                var webclient = new WebClient();
                string url = string.Format(BASE_URL + "findallempbyrol/{0}", id);
                var json = webclient.DownloadString(url);
                var js = new JavaScriptSerializer();
                return js.Deserialize<List<Employee>>(json);
            }
            catch
            {
                return null;
            }
        }

        // PRONAĐI POJEDINOG ZAPOSLENIKA
        public Employee findEmp(string id)
        {
            try
            {
                var webclient = new WebClient();
                string url = string.Format(BASE_URL + "findemp/{0}", id);
                var json = webclient.DownloadString(url);
                var js = new JavaScriptSerializer();
                return js.Deserialize<Employee>(json);
            }
            catch
            {
                return null;
            }
        }


        // PRONAĐI POJEDINU ROLU
        public Role findRol(string id)
        {
            try
            {
                var webclient = new WebClient();
                string url = string.Format(BASE_URL + "findrol/{0}", id);
                var json = webclient.DownloadString(url);
                var js = new JavaScriptSerializer();
                return js.Deserialize<Role>(json);
            }
            catch
            {
                return null;
            }
        }


        // KREIRAJ ZAPOSLENIKA
        public bool createEmp(Employee employee)
        {
            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Employee));
                MemoryStream mem = new MemoryStream();
                ser.WriteObject(mem, employee);
                string data = Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
                WebClient webClient = new WebClient();
                webClient.Headers["Content-type"] = "application/json";
                webClient.Encoding = Encoding.UTF8;
                webClient.UploadString(BASE_URL + "createemp", "POST", data);

                return true;
            }
            catch
            {
                return false;
            }
        }


        // KREIRAJ ROLU
        public bool createRol(Role role)
        {
            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Role));
                MemoryStream mem = new MemoryStream();
                ser.WriteObject(mem, role);
                string data = Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
                WebClient webClient = new WebClient();
                webClient.Headers["Content-type"] = "application/json";
                webClient.Encoding = Encoding.UTF8;
                webClient.UploadString(BASE_URL + "createrol", "POST", data);

                return true;
            }
            catch
            {
                return false;
            }
        }


        // UREDI PODATKE ZAPOSLENIKA (BEZ ROLE)
        public bool editEmp(Employee employee)
        {
            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Employee));
                MemoryStream mem = new MemoryStream();
                ser.WriteObject(mem, employee);
                string data = Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
                WebClient webClient = new WebClient();
                webClient.Headers["Content-type"] = "application/json";
                webClient.Encoding = Encoding.UTF8;
                webClient.UploadString(BASE_URL + "editemp", "PUT", data);

                return true;
            }
            catch
            {
                return false;
            }
        }


        // UREDI SAMO ROLU ZAPOSLENIKA
        public bool editOnlyEmpRole(Employee employee)
        {
            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Employee));
                MemoryStream mem = new MemoryStream();
                ser.WriteObject(mem, employee);
                string data = Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
                WebClient webClient = new WebClient();
                webClient.Headers["Content-type"] = "application/json";
                webClient.Encoding = Encoding.UTF8;
                webClient.UploadString(BASE_URL + "editonlyemprole", "PUT", data);

                return true;
            }
            catch
            {
                return false;
            }
        }


        // UREDI PODATKE ROLE (SAMO ROLE)
        public bool editRol(Role role)
        {
            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Role));
                MemoryStream mem = new MemoryStream();
                ser.WriteObject(mem, role);
                string data = Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
                WebClient webClient = new WebClient();
                webClient.Headers["Content-type"] = "application/json";
                webClient.Encoding = Encoding.UTF8;
                webClient.UploadString(BASE_URL + "editrol", "PUT", data);

                return true;
            }
            catch
            {
                return false;
            }
        }


        // OBRIŠI ZAPOSLENIKA
        public bool deleteEmp(Employee employee)
        {
            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Employee));
                MemoryStream mem = new MemoryStream();
                ser.WriteObject(mem, employee);
                string data = Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
                WebClient webClient = new WebClient();
                webClient.Headers["Content-type"] = "application/json";
                webClient.Encoding = Encoding.UTF8;
                webClient.UploadString(BASE_URL + "deleteemp", "DELETE", data);

                return true;
            }
            catch
            {
                return false;
            }
        }


        // KREIRAJ ROLU
        public bool deleteRol(Role role)
        {
            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Role));
                MemoryStream mem = new MemoryStream();
                ser.WriteObject(mem, role);
                string data = Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
                WebClient webClient = new WebClient();
                webClient.Headers["Content-type"] = "application/json";
                webClient.Encoding = Encoding.UTF8;
                webClient.UploadString(BASE_URL + "deleterol", "DELETE", data);

                return true;
            }
            catch
            {
                return false;
            }
        }

        // ROLECHANGE
        public bool roleChange(Employee employee)
        {
            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Employee));
                MemoryStream mem = new MemoryStream();
                ser.WriteObject(mem, employee);
                string data = Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
                WebClient webClient = new WebClient();
                webClient.Headers["Content-type"] = "application/json";
                webClient.Encoding = Encoding.UTF8;
                webClient.UploadString(BASE_URL + "rolechange", "PUT", data);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}