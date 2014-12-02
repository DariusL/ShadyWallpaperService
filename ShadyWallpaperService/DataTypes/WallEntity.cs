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
        [DataMember]
        public long Id;
        [DataMember]
        public string Board;
        [DataMember]
        public long ThreadId;
        [DataMember]
        public int B16X9;
        [DataMember]
        public int B4X3;
        public long Time;
    }
}