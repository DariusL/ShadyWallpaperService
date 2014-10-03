using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ShadyWallpaperService.DataTypes
{
    [DataContract]
    public class WallEntity
    {
        [DataMember]
        public string WallUrl;
        [DataMember]
        public string ThumbUrl;

        public ObjectId Id;
        public long ThreadId;
        public int B16X9;
        public int B4X3;
        public int Time;
    }
}