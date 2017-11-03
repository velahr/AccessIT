using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ClaimsDocsBizLogic
{
    //start definition of class : Department
    [DataContract]
    public class Department
    {
        //declare public class properties
        [DataMember]
        public int DepartmentID { get; set; }
        [DataMember]
        public string DepartmentName { get; set; }
        [DataMember]
        public DateTime IUDateTime { get; set; }

        //initialize class properties;
        public Department()
        {
            DepartmentID = 0;
            DepartmentName = "";
            IUDateTime = DateTime.Now;
        }
    }//end class definition of class : Department

    //define ICDDepartments Service Contract
    [ServiceContract]
    public interface ICDDepartments
    {
        [OperationContract]
        int DepartmentCreate(Department objDepartment, string strConnectionString);

        [OperationContract]
        Department DepartmentRead(Department objDepartment, string strConnectionString);

        [OperationContract]
        int DepartmentUpdate(Department objDepartment, string strConnectionString);

        [OperationContract]
        List<Department> DepartmentSearch(string strSQLString, string strConnectionString);
    }//end : public interface ICDDepartments



}//end : namespace ClaimsDocsBizLogic
