using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ShadyWallpaperService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IShadyWallpaperService
    {

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "{id}")]
        CompositeType GetData(string id);

        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        [DataMember]
        public bool BoolValue { get; private set; }
        [DataMember]
        public string StringValue { get; private set; }
        public CompositeType(bool b, string s)
        {
            BoolValue = b;
            StringValue = s;
        }
    }
}
