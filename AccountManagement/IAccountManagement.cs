using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace AccountManagement
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAccountManagement" in both code and config file together.
    [ServiceContract]
    public interface IAccountManagement
    {
        // Pronađi sve račune
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "findallacc", ResponseFormat = WebMessageFormat.Json)]
        List<Account> findAllAcc();

        // Pronađi sve račune vezane za pojedinu rolu (accounts for roles)
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "findallaccbyrol/{id}", ResponseFormat = WebMessageFormat.Json)]
        List<ManyToManyFindAccRole> findAllAccByRol(string id);
       
        // Pronađi sve role vezane za pojedini račun (roles for account)
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "findallrolbyacc/{id}", ResponseFormat = WebMessageFormat.Json)]
        List<ManyToManyFindRolAcc> findAllRolByAcc(string id);


        // Pronađi specifičan account (prema id-ju)
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "findacc/{id}", ResponseFormat = WebMessageFormat.Json)]
        Account findAcc(string id);

        // Kreiraj account
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "createacc", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool createAcc(Account account);

        // Obriši account 
        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "deleteacc", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool deleteAcc(Account account);

        // Update account data (edit)
        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "updateaccdata", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool updateAccData(Account account);

        // Add role to account
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "addroletoacc", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool addRoleToAcc(AccRoleMapp accrolemapp);

        // Pronađi account u AccRoleMapp (za removeRoleFromAcc)
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "findaccrolemapp/{id}", ResponseFormat = WebMessageFormat.Json)]
        AccRoleMapp findAccRoleMapp(string id);

        // Remove role from acc
        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "removerolefromacc", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool removeRoleFromAcc(AccRoleMapp accrolemapp);

        // Pronađi sve račune vezane za pojedinu rolu (edit accounts for role)
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "findaccsforrole/{id}", ResponseFormat = WebMessageFormat.Json)]
        List<ManyToManyFindAccRole> findAccsForRole(string id);

        // Add account to role
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "addacctorole", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool addAccToRole(AccRoleMapp accrolemapp);

        // Remove account from role
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "removeaccfromrole", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool removeAccFromRole(AccRoleMapp accrolemapp);
    }
}
