using MongoDB.Driver;
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
            var connectionString = String.Format("mongodb://localhost:27017");
            var client = new MongoClient(connectionString);
            var server = client.GetServer();
            return server.GetDatabase("base");
        }
    }
}
