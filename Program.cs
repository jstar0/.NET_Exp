using System.Configuration;
using System.Security.Policy;

namespace StudentInformationEntrySystem
{
    internal static class Program
    {
        // �� App.config �� ���� baseUrl �� hasReadLicense
        public static readonly string? BaseUrl = ConfigurationManager.AppSettings["baseUrl"];
        private static readonly bool HasReadLicense = bool.Parse(ConfigurationManager.AppSettings["hasReadLicense"] ?? "false");
        private static readonly string AppVersion = ConfigurationManager.AppSettings["appVersion"] ?? "undefined";
        public static Users CurrentUser = new();
        public static readonly NetworkRequest CurrentPostReq = new();
        public static bool WillExit = true;
        public static bool IsSignedIn = false;
        public static bool HasGotCourseInfo = false;
        public static string? Token = null;

        // AvailableCoursesList ��ȡ /courses/search/major/{major}
        // CanSelectCoursesList (���)���� AvailableCoursesList -(ȥ��) SelectedCoursesList
        // SelectedCoursesList (�ұ�) ֱ�ӻ�ȡ /courses/selected
        // ֻ��������ұ�ĵ�����ק���ұ�DEL���ǽ��ұ���Ϣ�Ƶ����
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