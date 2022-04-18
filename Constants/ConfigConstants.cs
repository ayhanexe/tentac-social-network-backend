using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constants
{
    public static class ConfigConstants
    {
        public static string DefaultWallPhotoName { get; set; } = "wall-default.jpg";
        public static string DefaultRootPath { get; set; } = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\");
        public static string ProfileImagesRootPath { get; set; } = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\media\\profiles");
        public static string WallImagesRootPath { get; set; } = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\media\\walls");
        public static string StoryImagesRootPath { get; set; } = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\media\\stories");
    }
}
