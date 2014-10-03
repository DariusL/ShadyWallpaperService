using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ShadyWallpaperService.DataTypes
{
    [DataContract]
    [BsonIgnoreExtraElements]
    public class WallEntity
    {
        [DataMember]
        public string WallUrl;
        [DataMember]
        public string ThumbUrl;

        public long ThreadId;
        public int B16X9;
        public int B4X3;
        public int Time;
    }
}