using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ClaimsDocsBizLogic
{
    //define User Data Contract
    [DataContract]
    public class User
    {
        //declare public class properties
        [DataMember]
        public int UserID { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string UserPassword { get; set; }
        [DataMember]
        public int DepartmentID { get; set; }
        [DataMember]
        public string Approver { get; set; }
        [DataMember]
        public string Designer { get; set; }
        [DataMember]
        public string Administrator { get; set; }
        [DataMember]
        public string Reviewer { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Phone { get; set; }
        [DataMember]
        public string Signature { get; set; }
        [DataMember]
        public string Active { get; set; }
        [DataMember]
        public string SignatureName { get; set; }
        [DataMember]
        public string EMailAddress { get; set; }
        [DataMember]
        public DateTime IUDateTime { get; set; }
        [DataMember]
        public string DepartmentName { get; set; }
        [DataMember]
        public List<UserGroup> listUserGroup {get;set;}

        //initialize class properties;
        public User()
        {
            UserID = 0;
            UserName = "";
            UserPassword = "";
            DepartmentID = 0;
            Approver = "";
            Designer = "";
            Administrator = "";
            Reviewer = "";
            Title = "";
            Phone = "";
            Signature = "";
            Active = "";
            SignatureName = "";
            EMailAddress = "";
            IUDateTime = DateTime.Now;
        }
    }//end class definition of class : User

    //define User Group Data Contract
    [DataContract]
    public class UserGroup
    {
        //declare public class properties
        [DataMember]
        public int UserID { get; set; }
        [DataMember]
        public int GroupID { get; set; }
        [DataMember]
        public string GroupName { get; set; }
        [DataMember]
        public int DepartmentID { get; set; }
        [DataMember]
        public string DepartmentName { get; set; }
        [DataMember]
        public DateTime IUDateTime { get; set; }
    }

    //Define ICDUsers Service Contract
    [ServiceContract]
    public interface ICDUsers
    {
        [OperationContract]
        User UserLogin(User objUser, string strConnectionString);

        [OperationContract]
        int UserCreate(User objUser, string strConnectionString);

        [OperationContract]
        User UserRead(User objUser, string strConnectionString);

        [OperationContract]
        User UserReadByUserName(User objUser, string strConnectionString);

        [OperationContract]
        int UserUpdate(User objUser, string strConnectionString);

        [OperationContract]
        List<User> UserSearch(string strSQLString, string strConnectionString);

        [OperationContract]
        List<UserGroup> UserGroupSearch(int intUserID, string strConnectionString);

        [OperationContract]
        List<UserGroup> UserGroupSearchByUserName(string strUserName, string strConnectionString);

        [OperationContract]
        int UserGroupClear(UserGroup objUserGroup, string strConnectionString);

        [OperationContract]
        int UserGroupCreate(UserGroup objUserGroup, string strConnectionString);

    }//end : public interface ICDUsers


}//end : namespace ClaimsDocsBizLogic
