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
    //[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)] 
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

                var posts = postCollection.AsQueryable<WallEntity>()
                    .Where(w => w.Board == board)
                    .Where(w => w.B16X9 >= enum16by9 || w.B4X3 >= enum4by3)
                    .ToList();

                var threadIds = posts
                    .Select(w => w.ThreadId)
                    .Distinct();

                var threads = threadCollection.AsQueryable<ThreadEntity>()
                    .Where(t => t.Id.In(threadIds))
                    .ToList();

                threads.ForEach(t => t.Walls = posts.Where(w => w.ThreadId == t.Id).Take(3));

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
                    .Where(w => w.B16X9 >= enum16by9 || w.B4X3 >= enum4by3)
                    .OrderBy(w => w.Time)
                    .Skip(page * WallPageSize)
                    .Take(WallPageSize);

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
                    .Where(w => w.B16X9 >= enum16by9 || w.B4X3 >= enum4by3)
                    .OrderBy(w => w.Time)
                    .Skip(page * WallPageSize)
                    .Take(WallPageSize);

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
