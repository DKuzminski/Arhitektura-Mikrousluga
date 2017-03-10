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
    public class AccountManagementServiceClient
    {
        public string BASE_URL = "http://localhost:49443/AccountManagement.svc/";
        // PRONAĐI SVE RAČUNE
        public List<Account> findAllAcc()
        {
            try
            {
                var webclient = new WebClient();
                var json = webclient.DownloadString(BASE_URL + "findallacc");
                var js = new JavaScriptSerializer();
                return js.Deserialize<List<Account>>(json);
            }
            catch
            {
                return null;
            }
        }

        // PRONAĐI SVE RAČUNE PREMA NEKOJ ROLI
        public List<ManyToManyFindAccRole> findAllAccByRol(string id)
        {
            try
            {
                var webclient = new WebClient();
                string url = string.Format(BASE_URL + "findallaccbyrol/{0}", id);
                var json = webclient.DownloadString(url);
                var js = new JavaScriptSerializer();
                return js.Deserialize<List<ManyToManyFindAccRole>>(json);
            }
            catch
            {
                return null;
            }
        }


        // PRONAĐI SVE ROLE VEZTANE ZA POJEDINI RAČUN
        public List<ManyToManyFindRolAcc> findAllRolByAcc(string id)
        {
            try
            {
                var webclient = new WebClient();
                string url = string.Format(BASE_URL + "findallrolbyacc/{0}", id);
                var json = webclient.DownloadString(url);
                var js = new JavaScriptSerializer();
                return js.Deserialize<List<ManyToManyFindRolAcc>>(json);
            }
            catch
            {
                return null;
            }
        }


        // PRONAĐI SPECIFIČAN RAČUN (PREMA ID-ju)
        public Account findAcc(string id)
        {
            try
            {
                var webclient = new WebClient();
                string url = string.Format(BASE_URL + "findacc/{0}", id);
                var json = webclient.DownloadString(url);
                var js = new JavaScriptSerializer();
                return js.Deserialize<Account>(json);
            }
            catch
            {
                return null;
            }
        }


        // KREIRAJ NOVI RAČUN
        public bool createAcc(Account account)
        {
            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Account));
                MemoryStream mem = new MemoryStream();
                ser.WriteObject(mem, account);
                string data = Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
                WebClient webClient = new WebClient();
                webClient.Headers["Content-type"] = "application/json";
                webClient.Encoding = Encoding.UTF8;
                webClient.UploadString(BASE_URL + "createacc", "POST", data);

                return true;
            }
            catch
            {
                return false;
            }
        }



        // DELETE ACCOUNT (AKO)
        public bool deleteAcc(Account account)
        {
            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Account));
                MemoryStream mem = new MemoryStream();
                ser.WriteObject(mem, account);
                string data = Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
                WebClient webClient = new WebClient();
                webClient.Headers["Content-type"] = "application/json";
                webClient.Encoding = Encoding.UTF8;
                webClient.UploadString(BASE_URL + "deleteacc", "DELETE", data);

                return true;
            }
            catch
            {
                return false;
            }
        }


        // UPDATE ACCOUNT
        public bool updateAccData(Account account)
        {
            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Account));
                MemoryStream mem = new MemoryStream();
                ser.WriteObject(mem, account);
                string data = Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
                WebClient webClient = new WebClient();
                webClient.Headers["Content-type"] = "application/json";
                webClient.Encoding = Encoding.UTF8;
                webClient.UploadString(BASE_URL + "updateaccdata", "PUT", data);

                return true;
            }
            catch
            {
                return false;
            }
        }



        // ADD ROLE TO ACC
        public bool addRoleToAcc(AccRoleMapp accrolemapp)
        {
            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(AccRoleMapp));
                MemoryStream mem = new MemoryStream();
                ser.WriteObject(mem, accrolemapp);
                string data = Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
                WebClient webClient = new WebClient();
                webClient.Headers["Content-type"] = "application/json";
                webClient.Encoding = Encoding.UTF8;
                webClient.UploadString(BASE_URL + "addroletoacc", "POST", data);

                return true;
            }
            catch
            {
                return false;
            }
        }


        // PRONAĐI POJEDINI ACCOUNT U ACCROLEMAPP (Za removeRoleFromAcc)
        public AccRoleMapp findAccRoleMapp(string id)
        {
            try
            {
                var webclient = new WebClient();
                string url = string.Format(BASE_URL + "findaccrolemapp/{0}", id);
                var json = webclient.DownloadString(url);
                var js = new JavaScriptSerializer();
                return js.Deserialize<AccRoleMapp>(json);
            }
            catch
            {
                return null;
            }
        }

        // REMOVE ROLE FROM ACC
        public bool removeRoleFromAcc(AccRoleMapp accrolemapp)
        {
            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(AccRoleMapp));
                MemoryStream mem = new MemoryStream();
                ser.WriteObject(mem, accrolemapp);
                string data = Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
                WebClient webClient = new WebClient();
                webClient.Headers["Content-type"] = "application/json";
                webClient.Encoding = Encoding.UTF8;
                webClient.UploadString(BASE_URL + "removerolefromacc", "DELETE", data);

                return true;
            }
            catch
            {
                return false;
            }
        }


        // PRONAĐI SVE RAČUNE PREMA NEKOJ ROLI (EDIT ACC FOR ROLE)
        public List<ManyToManyFindAccRole> findAccsForRole(string id)
        {
            try
            {
                var webclient = new WebClient();
                string url = string.Format(BASE_URL + "findaccsforrole/{0}", id);
                var json = webclient.DownloadString(url);
                var js = new JavaScriptSerializer();
                return js.Deserialize<List<ManyToManyFindAccRole>>(json);
            }
            catch
            {
                return null;
            }
        }


        /*
        // ADD ACCOUNT TO ROLE
        public bool addRoleToAcc(AccRoleMapp accrolemapp)
        {
            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(AccRoleMapp));
                MemoryStream mem = new MemoryStream();
                ser.WriteObject(mem, accrolemapp);
                string data = Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
                WebClient webClient = new WebClient();
                webClient.Headers["Content-type"] = "application/json";
                webClient.Encoding = Encoding.UTF8;
                webClient.UploadString(BASE_URL + "addroletoacc", "POST", data);

                return true;
            }
            catch
            {
                return false;
            }
        }

        // REMOVE ACCOUNT FROM ROLE
        public bool removeRoleFromAcc(AccRoleMapp accrolemapp)
        {
            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(AccRoleMapp));
                MemoryStream mem = new MemoryStream();
                ser.WriteObject(mem, accrolemapp);
                string data = Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
                WebClient webClient = new WebClient();
                webClient.Headers["Content-type"] = "application/json";
                webClient.Encoding = Encoding.UTF8;
                webClient.UploadString(BASE_URL + "removerolefromacc", "DELETE", data);

                return true;
            }
            catch
            {
                return false;
            }
        }*/

        // ADD ACCOUNT TO ROLE
        public bool addAccToRole(AccRoleMapp accrolemapp)
        {
            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(AccRoleMapp));
                MemoryStream mem = new MemoryStream();
                ser.WriteObject(mem, accrolemapp);
                string data = Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
                WebClient webClient = new WebClient();
                webClient.Headers["Content-type"] = "application/json";
                webClient.Encoding = Encoding.UTF8;
                webClient.UploadString(BASE_URL + "addacctorole", "POST", data);

                return true;
            }
            catch
            {
                return false;
            }
        }


        // REMOVE ACCOUNT FROM ROLE
        public bool removeAccFromRole(AccRoleMapp accrolemapp)
        {
            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(AccRoleMapp));
                MemoryStream mem = new MemoryStream();
                ser.WriteObject(mem, accrolemapp);
                string data = Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
                WebClient webClient = new WebClient();
                webClient.Headers["Content-type"] = "application/json";
                webClient.Encoding = Encoding.UTF8;
                webClient.UploadString(BASE_URL + "removeaccfromrole", "DELETE", data);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}