using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using ShadyWallpaperService.DataTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ShadyWallpaperService;

namespace ShadyWallpaperService
{
    public class ShadyWallpaperService : IShadyWallpaperService
    {
        private const int ThreadPageSize = 20;
        private const int WallPageSize = 50;

        private MongoDatabase database;

        public ShadyWallpaperService()
        {
            database = CreateDatabase();
            database.Server.Ping();
        }

        protected virtual MongoDatabase CreateDatabase()
        {
            var connectionString = String.Format("mongodb://{0}:{1}@ds063859.mongolab.com:63859/base", Keys.User, Keys.Pass);
            var client = new MongoClient(connectionString);
            var server = client.GetServer();
            return server.GetDatabase("base");
        }

        public IEnumerable<ThreadEntity> Threads(string board, string requestPage, string res16by9, string res4by3)
        {
            var threadCollection = database.GetCollection("threads");
            var postCollection = database.GetCollection("posts");
            var context = WebOperationContext.Current;

            try
            {
                int page = Convert.ToInt32(requestPage) - 1;
                var enum16by9 = (int)(res16by9 != null ? TypeUtils.ParseEnum<R16By9>(res16by9) : R16By9.None);
                var enum4by3 = (int)(res4by3 != null ? TypeUtils.ParseEnum<R4By3>(res4by3) : R4By3.None);

                var threadIds = postCollection.AsQueryable<WallEntity>()
                    .Where(w => w.Board == board)
                    .WhereSize(enum16by9, enum4by3)
                    .Select(w => w.ThreadId)
                    .Distinct();

                var threads = threadCollection.AsQueryable<ThreadEntity>()
                    .Where(t => t.Id.In(threadIds))
                    .OrderBy(t => t.Time)
                    .Skip(page * ThreadPageSize)
                    .Take(ThreadPageSize)
                    .ToList();

                new LogEvent(String.Format("board threads request: board: {0}, page: {1}, res16by9: {2}, res4by3: {3}, result count: {4}",
                    board, requestPage, res16by9, res4by3, threads.Count())).Raise();

                return threads;
            }
            catch(FormatException)
            {
                //422 Unprocessable Entity
                context.OutgoingResponse.StatusCode = (System.Net.HttpStatusCode)422;
            }
            catch(OverflowException)
            {
                //422 Unprocessable Entity
                context.OutgoingResponse.StatusCode = (System.Net.HttpStatusCode)422;
            }
            catch(Exception e)
            {
                e.ToString();
            }

            return null;
        }

        public IEnumerable<WallEntity> BoardWalls(string board, string requestPage, string res16by9, string res4by3)
        {
            var postCollection = database.GetCollection("posts");
            var context = WebOperationContext.Current;

            try
            {
                int page = Convert.ToInt32(requestPage) - 1;
                var enum16by9 = (int)(res16by9 != null ? TypeUtils.ParseEnum<R16By9>(res16by9) : R16By9.None);
                var enum4by3 = (int)(res4by3 != null ? TypeUtils.ParseEnum<R4By3>(res4by3) : R4By3.None);

                var walls = postCollection.AsQueryable<WallEntity>()
                    .Where(w => w.Board == board)
                    .WhereSize(enum16by9, enum4by3)
                    .OrderBy(w => w.Time)
                    .Skip(page * WallPageSize)
                    .Take(WallPageSize);

                new LogEvent(String.Format("board walls request: board: {0}, page: {1}, res16by9: {2}, res4by3: {3}, result count: {4}",
                    board, requestPage, res16by9, res4by3, walls.Count())).Raise();

                return walls;
            }
            catch (FormatException)
            {
                //422 Unprocessable Entity
                context.OutgoingResponse.StatusCode = (System.Net.HttpStatusCode)422;
            }
            catch (OverflowException)
            {
                //422 Unprocessable Entity
                context.OutgoingResponse.StatusCode = (System.Net.HttpStatusCode)422;
            }

            return null;
        }

        public IEnumerable<WallEntity> ThreadWalls(string board, string requestThread, string requestPage, string res16by9, string res4by3)
        {
            var postCollection = database.GetCollection("posts");
            var context = WebOperationContext.Current;

            try
            {
                int page = Convert.ToInt32(requestPage) - 1;
                long thread = Convert.ToInt64(requestThread);
                var enum16by9 = (int)(res16by9 != null ? TypeUtils.ParseEnum<R16By9>(res16by9) : R16By9.None);
                var enum4by3 = (int)(res4by3 != null ? TypeUtils.ParseEnum<R4By3>(res4by3) : R4By3.None);

                var walls = postCollection.AsQueryable<WallEntity>()
                    .Where(w => w.ThreadId == thread)
                    .Where(w => w.Board == board)
                    .WhereSize(enum16by9, enum4by3)
                    .OrderBy(w => w.Time)
                    .Skip(page * WallPageSize)
                    .Take(WallPageSize);

                new LogEvent(String.Format("thread walls request: board: {0}, thread: {1}, page: {2}, res16by9: {3}, res4by3: {4}, result count: {5}",
                    board, requestThread, requestPage, res16by9, res4by3, walls.Count())).Raise();

                return walls;
            }
            catch (FormatException)
            {
                //422 Unprocessable Entity
                context.OutgoingResponse.StatusCode = (System.Net.HttpStatusCode)422;
            }
            catch (OverflowException)
            {
                //422 Unprocessable Entity
                context.OutgoingResponse.StatusCode = (System.Net.HttpStatusCode)422;
            }

            return null;
        }

        public string Teapot()
        {
            WebOperationContext.Current.OutgoingResponse.StatusCode = (System.Net.HttpStatusCode)418;
            return "I'm a teapot";
        }
    }
}
