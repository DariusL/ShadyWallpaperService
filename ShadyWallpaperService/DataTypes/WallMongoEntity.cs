using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShadyWallpaperService.DataTypes
{
    public class WallMongoEntity
    {
        public ObjectId Id;
        public int ThreadId;
        public string WallUrl;
        public string ThumbUrl;
        public int B16X9;
        public int B4X3;
    }
}