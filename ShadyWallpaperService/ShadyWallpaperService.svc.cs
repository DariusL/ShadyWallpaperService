﻿using MongoDB.Driver.Builders;
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

                var threadIds = postCollection.AsQueryable<WallEntity>()
                    .Where(w => w.B16X9 >= enum16by9 || w.B4X3 >= enum4by3)
                    .Select(w => w.ThreadId)
                    .Distinct();

                var threads = threadCollection.AsQueryable<ThreadEntity>()
                    .Where(e => e.Board == board)
                    .Where(e => e.ThreadId.In(threadIds))
                    .OrderBy(e => e.Time)
                    .Skip(page * ThreadPageSize)
                    .Take(ThreadPageSize)
                    .ToList();

                foreach (var thread in threads)
                {
                    thread.Walls = postCollection.AsQueryable<WallEntity>()
                        .Where(w => w.ThreadId == thread.ThreadId)
                        .Where(w => w.B16X9 >= enum16by9 || w.B4X3 >= enum4by3)
                        .OrderBy(w => w.Time)
                        .Take(3);
                }

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

            return null;
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

        public string Teapot()
        {
            WebOperationContext.Current.OutgoingResponse.StatusCode = (System.Net.HttpStatusCode)418;
            return "I'm a teapot";
        }
    }
}
