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
    public class ThreadEntity
    {
        [DataMember]
        public long Id;
        public string Board;
        public long Time;
        [DataMember]
        public string OpContent;
        [DataMember]
        public WallEntity OpPost;

        public ThreadEntity() { }
        public ThreadEntity(long id, WallEntity opPost)
        {
            Id = id;
            OpPost = opPost;
        }
    }
}