using MongoDB.Driver;
using ShadyWallpaperService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadyWallpaperServiceTester
{
    class TestingService : ShadyWallpaperService.ShadyWallpaperService
    {
        protected override MongoDatabase CreateDatabase()
        {
            var connectionString = String.Format("mongodb://{0}:{1}@ds049130.mongolab.com:49130/base-test", Keys.User, Keys.Pass);
            var client = new MongoClient(connectionString);
            var server = client.GetServer();
            return server.GetDatabase("base-test");
        }
    }
}
