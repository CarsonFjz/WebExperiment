namespace Basic.Core
{
    public class ApplicationBaseInfo
    {
        public static void SetApplicationName(string name)
        {
            Name = name;
        }
        public static string Name = "";

        public static void SetAppSettingPath(string appSettingPath)
        {
            AppSettingPath = appSettingPath;
        }
        public static string AppSettingPath = "appsettings.json";
    }
}
