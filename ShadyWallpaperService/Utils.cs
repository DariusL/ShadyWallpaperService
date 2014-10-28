using ShadyWallpaperService.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShadyWallpaperService
{
    public static class Utils
    {
        public static IQueryable<WallEntity> WhereSize(this IQueryable<WallEntity> walls, int enum16By9, int enum4By3)
        {
            if (enum16By9 != (int)R16By9.None)
                walls = walls.Where(w => w.B16X9 >= enum16By9);
            if (enum4By3 != (int)R4By3.None)
                walls = walls.Where(w => w.B4X3 >= enum4By3);

            return walls;
        }
    }
}