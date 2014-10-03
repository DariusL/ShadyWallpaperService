﻿using MongoDB.Bson;
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
    public class ThreadEntity
    {
        public string Board;
        public long Time;
        [DataMember]
        public long ThreadId;
        [DataMember]
        public string OpContent;
        [DataMember]
        public IEnumerable<WallEntity> Walls;
    }
}