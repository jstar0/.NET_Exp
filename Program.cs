using System.Configuration;
using System.Security.Policy;

namespace StudentInformationEntrySystem
{
    internal static class Program
    {
        // 从 App.config 中 加载 baseUrl 和 hasReadLicense
        public static readonly string? BaseUrl = ConfigurationManager.AppSettings["baseUrl"];
        private static readonly bool HasReadLicense = bool.Parse(ConfigurationManager.AppSettings["hasReadLicense"] ?? "false");
        private static readonly string AppVersion = ConfigurationManager.AppSettings["appVersion"] ?? "undefined";
        public static Users CurrentUser = new();
        public static readonly NetworkRequest CurrentPostReq = new();
        public static bool WillExit = true;
        public static bool IsSignedIn = false;
        public static bool HasGotCourseInfo = false;
        public static string? Token = null;

        // AvailableCoursesList 获取 /courses/search/major/{major}
        // CanSelectCoursesList (左表)，是 AvailableCoursesList -(去重) SelectedCoursesList
        // SelectedCoursesList (右表) 直接获取 /courses/selected
        // 只允许左表到右表的单向拖拽，右表DEL，是将右表信息移到左表
        public static Dictionary<int, Courses> AvailableCoursesList = [];
        public static Dictionary<int, Courses> CanSelectCoursesList = [];
        public static Dictionary<int, Courses> SelectedCoursesList = [];
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            if (!HasReadLicense)
            {
                Application.Run(new LicenseForm());
                if (WillExit)
                {
                    return;
                }
            }

            WillExit = false;

            Application.Run(new LoginForm());
        }
    }
}