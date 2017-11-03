using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ClaimsDocsBizLogic
{
    //start definition of class : DocumentField
    [ServiceContract]
    public interface ICDDocumentGenerator
    {
        [OperationContract]
        void GenerateDocument();
    }
}
