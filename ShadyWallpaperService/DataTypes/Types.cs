using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShadyWallpaperService.DataTypes
{
    internal enum R16By9
    {
        None,
        All,
        R1280By720,
        R1366By768,
        R1600By900,
        R1920By1080,
        R2560By1440,
        R3840By2160,
        R7680By4320
    }

    internal enum R4By3
    {
        None,
        All,
        R800X600,
        R1024X768,
        R1280By1024,
        R1600By1200
    }

    internal static class TypeUtils
    {
        internal static T ParseEnum<T>(string value) where T : struct
        {
            T ret;
            if(Enum.TryParse<T>(value, true, out ret))
                return ret;
            else 
                throw new FormatException();
        }
    }
}