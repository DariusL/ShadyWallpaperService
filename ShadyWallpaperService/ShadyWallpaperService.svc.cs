using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
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
        private const int ThreadPageSize = 20;
        private const int WallPageSize = 50;
        private MongoDB.Driver.MongoDatabase database;
        public ShadyWallpaperService()
        {
            var connectionString = String.Format("mongodb://{0}:{1}@ds063859.mongolab.com:63859/base", Keys.User, Keys.Pass);
            var client = new MongoDB.Driver.MongoClient(connectionString);
            var server = client.GetServer();
            database = server.GetDatabase("base");
        }
        public IEnumerable<int> Threads(string board, string requestPage, string res16by9, string res4by3)
        {
            var collection = database.GetCollection("threads");
            int page = Convert.ToInt32(requestPage) - 1;
            var entities = collection.AsQueryable<ThreadMongoEntity>()
                .Where(e => e.Board == board)
                .OrderBy(e => e.Time)
                .Skip(page * ThreadPageSize)
                .Take(ThreadPageSize)
                .Select(e => e.ThreadId);

            return entities;
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
