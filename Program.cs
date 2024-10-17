using System;
using System.Net.Http;
using System.Threading.Tasks;
// Install-Package System.Configuration.ConfigurationManager
using System.Configuration;

namespace SelectCourseProgram
{
    internal class Program
    {
        // 从 App.config 中 加载 baseUrl 和 hasReadLicense
        public static readonly string? BaseUrl = ConfigurationManager.AppSettings["baseUrl"];
        private static readonly bool HasReadLicense = bool.Parse(ConfigurationManager.AppSettings["hasReadLicense"] ?? "false");
        private static readonly string AppVersion = ConfigurationManager.AppSettings["appVersion"] ?? "undefined";

        public static Users CurrentUser = new();
        public static Courses[] AvailableCoursesList = [];
        public static Courses[] SelectedCoursesList = [];
        //public static readonly NetworkRequest CurrentGetReq = new();
        public static readonly NetworkRequest CurrentPostReq = new();
        public static string Token = "";
        public static bool WillExit = false;
        public static bool IsSignedIn = false;
        // 异步的 Main 方法
        private static async Task Main()
        {
            try
            {
                Console.CursorVisible = false;
                NetworkResponse isAddressValid = new();

                // 在主函数中，如果直接使用异步方法 await NetworkStack.GetAsync() 会阻塞主函数
                // （await 将阻塞本函数所在线程，但不阻塞其他线程）
                // 然而一般的 .NET 编程中，存在多个线程，UI 也是其中之一
                var checkAddressTask = Task.Run(async () =>
                    isAddressValid = await NetworkStack.GetAsync(BaseUrl + "/connectTest"));

                ConsoleController.LoadingPage();

                if (!HasReadLicense)
                {
                    ConsoleController.LicenseNotice();
                    Console.ReadLine();
                    AppSettings.UpdateAppSettings("hasReadLicense", "true");
                }

                if (!checkAddressTask.IsCompleted)
                {
                    Console.WriteLine("请稍侯，正在测试服务器连通性");
                    Console.Write("初次连接服务器可能较慢，因为服务器可能休眠，请耐心等待");
                }
                while (!checkAddressTask.IsCompleted)
                {
                    Console.Write(".");
                    await Task.Delay(1000);
                }
                if (!NetworkStack.IsSuccessfulStatusCode(isAddressValid.StatusCode))
                {
                    Console.WriteLine("\n\n服务器不可用，错误信息如下:");
                    Console.WriteLine($"StatusCode: {isAddressValid.StatusCode}, Message: {isAddressValid.Content}");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("根据错误信息，您应该发现失败原因，或敬请联系我。");
                    Console.WriteLine("您可以调整 config 文件中的 baseUrl 字段，或自行部署后端服务器。\n\n");
                    Console.ResetColor();
                    Console.WriteLine("程序将在您按下回车后结束。");
                    Console.ReadLine();
                    return;
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n\n服务器连接成功。服务器版本: {isAddressValid.Content}");
                Console.ResetColor();

                if (AppVersion != isAddressValid.Content)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("您的程序版本与服务器版本不一致，可能会导致不可预知的错误。");
                    Console.WriteLine($"客户端版本: {AppVersion}, 服务器版本: {isAddressValid.Content}");
                    Console.WriteLine("请按任意键继续...");
                    Console.ResetColor();
                    Console.ReadKey(true);
                }
                else
                {
                    await Task.Delay(2000);
                }

                Console.Clear();

                ConsoleController.WelcomePage();

                _ = Task.Run(NetworkStack.TimelyGetUserProfile);

                await Task.Run(ConsoleController.ConsoleMenuMainLoop);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}