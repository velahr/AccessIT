using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ClaimsDocsBizLogic
{
    //start definition of class : Group
    [DataContract]
    public class Group
    {
        //declare public class properties
        [DataMember]
        public int GroupID { get; set; }
        [DataMember]
        public int DepartmentID { get; set; }
        [DataMember]
        public string GroupName { get; set; }
        [DataMember]
        public string DepartmentName { get; set; }
        [DataMember]
        public DateTime IUDateTime { get; set; }

        //initialize class properties;
        public Group()
        {
            GroupID = 0;
            DepartmentID = 0;
            GroupName = "";
            DepartmentName = "";
            IUDateTime = DateTime.Now;
        }
    }//end class definition of class : Group

    //define ICDGroups Service Contract
    [ServiceContract]
    public interface ICDGroups
    {
        [OperationContract]
        int GroupCreate(Group objGroup, string strConnectionString);

        [OperationContract]
        Group GroupRead(Group objGroup, string strConnectionString);

        [OperationContract]
        int GroupUpdate(Group objGroup, string strConnectionString);

        [OperationContract]
        List<Group> GroupSearch(string strSQLString, string strConnectionString);
    }//end : public interface ICDGroups

}//end : namespace ClaimsDocsBizLogic
