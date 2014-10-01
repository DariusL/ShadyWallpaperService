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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class ShadyWallpaperService : IShadyWallpaperService
    {
        private MongoDB.Driver.MongoDatabase database;
        public ShadyWallpaperService()
        {
            var client = new MongoDB.Driver.MongoClient();
            var server = client.GetServer();
            database = server.GetDatabase("base");
        }
        public ThreadsRequest Threads(string board, string res16by9, string res4by3)
        {
            var ret = new ThreadsRequest();
            ret.Board = board;
            ret.R4X3 = res4by3;
            ret.R16X9 = res16by9;
            return ret;
        }

        public BoardWallsRequest BoardWalls(string board, string res16by9, string res4by3)
        {
            var ret = new BoardWallsRequest();
            ret.Board = board;
            ret.R4X3 = res4by3;
            ret.R16X9 = res16by9;
            return ret;
        }

        public ThreadWallsRequest ThreadWalls(string board, string thread, string res16by9, string res4by3)
        {
            var ret = new ThreadWallsRequest();
            ret.ThreadId = Convert.ToInt32(thread);
            ret.R4X3 = res4by3;
            ret.R16X9 = res16by9;
            return ret;
        }
    }
}
