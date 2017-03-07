using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

using EmployeeManagement;

namespace AccountAssignment
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAccountAssignment" in both code and config file together.
    [ServiceContract]
    public interface IAccountAssignment
    {

        // Pronađi sve zaposlenik id i račune vezane uz njih (AKO BUDE POTREBNO) 
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "findempacc", ResponseFormat = WebMessageFormat.Json)]
        List<EmpAccMapp> findEmpAcc();

        // PRONAĐI SVE RAČUNE NEKOG ZAPOSLENIKA
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "findallaccforemp/{id}", ResponseFormat = WebMessageFormat.Json)]
        List<ManyToManyEmpAcc> findAllAccForEmp(string id);



        // Dodaj account zaposleniku
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "addacctoemployee", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool addAccToEmployee(EmpAccMapp empaccmapp);


        // Ukloni account od nekog zaposlenika
        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "removeaccfromemployee", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool removeAccFromEmployee(EmpAccMapp empaccmapp);

        /*
        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "editrol", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool editRol(EmpAccMapp empaccmapp);*/

        /*
      // ROLECHANGE
      [OperationContract]
      [WebInvoke(Method = "PUT", UriTemplate = "rolechange", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
      bool roleChange(string id);
      */

        /*
         // Pronađi pojedinog zaposlenika
         [OperationContract]
         [WebInvoke(Method = "GET", UriTemplate = "findemprolchange/{id}", ResponseFormat = WebMessageFormat.Json)]
         Employee findEmpRolChange(string id);*/
    }
}
