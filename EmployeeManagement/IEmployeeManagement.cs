using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace EmployeeManagement
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IEmployeeManagement" in both code and config file together.
    [ServiceContract]
    public interface IEmployeeManagement
    {

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "findallemp", ResponseFormat = WebMessageFormat.Json)]
        List<Employee> findAllEmp();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "findallrol", ResponseFormat = WebMessageFormat.Json)]
        List<Role> findAllRol();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "findallempbyrol/{id}", ResponseFormat = WebMessageFormat.Json)]
        List<Employee> findAllEmpByRol(string id);







        // Pronađi pojedinog zaposlenika
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "findemp/{id}", ResponseFormat = WebMessageFormat.Json)]
        Employee findEmp(string id);


        // Pronađi samo rolu pojedinog zaposlenika ()za rolechange
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "findroleforemp/{id}", ResponseFormat = WebMessageFormat.Json)]
        List<EmpRole> findRoleForEmp(string id);


        // Pronađi pojedinog zaposlenika u EmpAccMapp tablici (ZA SVAKI SLUČAJ)
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "findempaccmapp/{id}", ResponseFormat = WebMessageFormat.Json)]
        EmpAccMapp findEmpAccMapp(string id);



        // Pronađi pojedinu rolu
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "findrol/{id}", ResponseFormat = WebMessageFormat.Json)]
        Role findRol(string id);





        // Kreiraj zaposlenika
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "createemp", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool createEmp(Employee employee);

        // Kreiraj rolu
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "createrol", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool createRol(Role role);





        // Uredi zaposlenika (edit)
        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "editemp", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool editEmp(Employee employee);

        // Uredi rolu zaposlenika
        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "editonlyemprol", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool editOnlyEmpRol(Employee employee);

        // Uredi rolu
        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "editrol", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool editRol(Role role);




        // Obriši zaposlenika 
        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "deleteemp", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool deleteEmp(Employee employee);

        // Obriši rolu 
        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "deleterol", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool deleteRol(Role role);






        // Pronađi pojedinu rolu za roleChange
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "findrolforrolechange/{id}", ResponseFormat = WebMessageFormat.Json)]
        RoleRoleChange findRolForRolechange(string id);

        // ROLECHANGE
        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "rolechange", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool roleChange(Employee employee);
    }
}
