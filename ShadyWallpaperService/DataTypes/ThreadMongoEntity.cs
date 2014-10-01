using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShadyWallpaperService.DataTypes
{
    public class ThreadMongoEntity
    {
        public ObjectId Id;
        public string Board;
        public int ThreadId;
        public int Time;
    }
}