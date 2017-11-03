using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ClaimsDocsBizLogic
{
    //start definition of class : Program
    [DataContract]
    public class Program
    {
        //declare public class properties
        [DataMember]
        public int ProgramID { get; set; }
        [DataMember]
        public string ProgramCode { get; set; }
        [DataMember]
        public DateTime IUDateTime { get; set; }

        //initialize class properties;
        public Program()
        {
            ProgramID = 0;
            ProgramCode = "";
            IUDateTime = DateTime.Now;
        }
    }//end class definition of class : Program


    [ServiceContract]
    public interface ICDProgram
    {
        [OperationContract]
        int ProgramCreate(Program objProgram, string strConnectionString);

        [OperationContract]
        Program ProgramRead(Program objProgram, string strConnectionString);

        [OperationContract]
        int ProgramUpdate(Program objProgram, string strConnectionString);

        [OperationContract]
        List<Program> ProgramSearch(string strSQLString, string strConnectionString);
    }//end : public interface ICDProgram
}//end : namespace ClaimsDocsBizLogic

