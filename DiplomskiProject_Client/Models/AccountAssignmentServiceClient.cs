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
    public class AccountAssignmentServiceClient
    {
        public string BASE_URL = "http://localhost:17636/AccountAssignment.svc/";

        // PRONAĐI SVE RAČUNE POJEDINOG ZAPOSLENIKA
        public List<ManyToManyEmpAcc> findAllAccForEmp(string id)
        {
            try
            {
                var webclient = new WebClient();
                string url = string.Format(BASE_URL + "findallaccforemp/{0}", id);
                var json = webclient.DownloadString(url);
                var js = new JavaScriptSerializer();
                return js.Deserialize<List<ManyToManyEmpAcc>>(json);
            }
            catch
            {
                return null;
            }
        }





        // DODAJ RAČUN ZAPOSLENIKU
        public bool addAccToEmployee(EmpAccMapp empaccmapp)
        {
            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(EmpAccMapp));
                MemoryStream mem = new MemoryStream();
                ser.WriteObject(mem, empaccmapp);
                string data = Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
                WebClient webClient = new WebClient();
                webClient.Headers["Content-type"] = "application/json";
                webClient.Encoding = Encoding.UTF8;
                webClient.UploadString(BASE_URL + "addacctoemployee", "POST", data);

                return true;
            }
            catch
            {
                return false;
            }
        }






        // UKLONI RAČUN ZAPOSLENIKA
        public bool removeAccFromEmployee(EmpAccMapp empaccmapp)
        {
            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(EmpAccMapp));
                MemoryStream mem = new MemoryStream();
                ser.WriteObject(mem, empaccmapp);
                string data = Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
                WebClient webClient = new WebClient();
                webClient.Headers["Content-type"] = "application/json";
                webClient.Encoding = Encoding.UTF8;
                webClient.UploadString(BASE_URL + "removeaccfromemployee", "DELETE", data);

                return true;
            }
            catch
            {
                return false;
            }
        }













    }
}