using ShadyWallpaperService.DataTypes;
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
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/{board}/threads/{page}?r16x9={r16x9}&r4x3={r4x3}")]
        IEnumerable<ThreadEntity> Threads(string board, string page, string r16x9, string r4x3);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/{board}/walls?r16x9={r16x9}&r4x3={r4x3}")]
        BoardWallsRequest BoardWalls(string board, string r16x9, string r4x3);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/{board}/{thread}/walls?r16x9={r16x9}&r4x3={r4x3}")]
        ThreadWallsRequest ThreadWalls(string board, string thread, string r16x9, string r4x3);
    }
}
