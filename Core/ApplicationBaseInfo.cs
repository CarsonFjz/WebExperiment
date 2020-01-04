using System;
using System.Collections.Generic;
using System.Text;

namespace Basic.Core
{
    public class ApplicationBaseInfo
    {
        public static void SetApplicationName(string name)
        {
            Name = name;
        }
        public static string Name = "";

        public static void SetAppSettingPath(string path)
        {
            AppSettingPath = path;
        }
        public static string AppSettingPath = "appsettings.json";
    }
}
