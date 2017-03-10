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

        // Pronađi sve zaposlenik id i račune vezane uz njih (ako) 
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

    }
}
