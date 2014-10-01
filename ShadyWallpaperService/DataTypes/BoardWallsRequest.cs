﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ShadyWallpaperService.DataTypes
{
    [DataContract]
    public class BoardWallsRequest
    {
        [DataMember]
        public string Board { get; set; }
        [DataMember]
        public string R16X9 { get; set; }
        [DataMember]
        public string R4X3 { get; set; }
    }
}