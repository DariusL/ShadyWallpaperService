using ShadyWallpaperService.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Management;

namespace ShadyWallpaperService
{
    public static class Utils
    {
        public static IQueryable<WallEntity> WhereSize(this IQueryable<WallEntity> walls, int enum16By9, int enum4By3)
        {
            if (enum16By9 == (int)R16By9.None && enum4By3 == (int)R4By3.None)
                return walls;
            else
                return walls.Where(w => (w.B16X9 != (int)R16By9.None && w.B16X9 >= enum16By9)
                    || (w.B4X3 != (int)R4By3.None && w.B4X3 >= enum4By3));
        }
    }

    public class LogEvent : WebRequestErrorEvent
    {
        public LogEvent(string message)
            : base(null, null, 100001, new Exception(message))
        {
        }
    }
}