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
            UriTemplate = "/{board}/walls/{page}?r16x9={r16x9}&r4x3={r4x3}")]
        IEnumerable<WallEntity> BoardWalls(string board, string page, string r16x9, string r4x3);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/{board}/{thread}/walls/{page}?r16x9={r16x9}&r4x3={r4x3}")]
        IEnumerable<WallEntity> ThreadWalls(string board, string thread, string page, string r16x9, string r4x3);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/teapot")]
        string Teapot();
    }
}
