using ConsoleTables;
using System;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelectCourseProgram
{
    internal abstract class ConsoleController
    {
        public static void LoadingPage()
        {
            WelcomePage(false);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("#########################################");
            Console.WriteLine("\t  选课系统用户界面已启动\n");
            Console.WriteLine("  Author: 于景一\tID: 23090032047");
            Console.WriteLine("#########################################\n");
            Console.ResetColor();
        }

        public static void LicenseNotice()
        {
            Console.WriteLine("如需调整服务器地址，请转至本目录下唯一的 config 文件: baseUrl 字段。");
            Console.WriteLine("客户端和后端代码开源在 GitHub，具体请看本目录下 README.md。");
            Console.WriteLine("项目前后端都开源，基于 GPL-3.0 license，请勿滥用该项目。\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("当您按下回车，表示已知悉上述提示信息，以后运行程序不再显示。");
            Console.ResetColor();
        }

        public static void WelcomePage(bool? showNotice = true)
        {
            // 使用 @ 可以防止字符串转义
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(@"  ____                            ____       _           _   ");
            Console.WriteLine(@" / ___|___  _   _ _ __ ___  ___  / ___|  ___| | ___  ___| |_ ");
            Console.WriteLine(@"| |   / _ \| | | | '__/ __|/ _ \ \___ \ / _ \ |/ _ \/ __| __|");
            Console.WriteLine(@"| |__| (_) | |_| | |  \__ \  __/  ___) |  __/ |  __/ (__| |_ ");
            Console.WriteLine(@" \____\___/ \__,_|_|  |___/\___| |____/ \___|_|\___|\___|\__|" + "\n");
            Console.ResetColor();

            if (showNotice ?? true)
            {
                Console.WriteLine("*** 简易的选课系统 ***");
                Console.WriteLine("Made with LOVE by 于景一\n");
            }

        }

        private static int SelectMenu(string[] options)
        {
            var selectedIndex = 0;
            ConsoleKey key;
            do
            {
                for (var i = 0; i < options.Length; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"> {options[i]}");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"  {options[i]}");
                    }
                }

                key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        selectedIndex = (selectedIndex == 0) ? options.Length - 1 : selectedIndex - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedIndex = (selectedIndex == options.Length - 1) ? 0 : selectedIndex + 1;
                        break;
                    case ConsoleKey.Enter:
                        Console.Clear();
                        return selectedIndex;
                }

                Console.SetCursorPosition(0, Console.CursorTop - options.Length);

            } while (true);
        }

        public static async Task ConsoleMenuMainLoop()
        {
            while (!Program.WillExit)
            {
                if (Program.IsSignedIn == false)
                {
                    await LoginOrRegisterAsync();
                    if (Program.WillExit)
                        return;
                }

                Console.Clear();
                WelcomePage();

                Console.WriteLine($"欢迎您, {Program.CurrentUser.nickname ?? Program.CurrentUser.username}                   ");
                Console.WriteLine(
                    $"您是 {Program.CurrentUser.qualification ?? "未指定资质"}，专业是 {Program.CurrentUser.major ?? "未指定专业"}\n");
                Console.WriteLine("登录 Token 有效期 1 小时，当您发现各功能无法使用时，请重新登录。");
                Console.WriteLine("我们建议您在选课前，先进行一次“个人信息维护”。\n");

                string[] options = ["个人信息查看与维护", "课程信息查看与选课", "重新登录或注册", "退出系统"];
                var choiceMenu = SelectMenu(options);
                switch (choiceMenu)
                {
                    case 0:
                        await UserInfoPage();
                        break;
                    case 1:
                        await CourseInfoPage();
                        break;
                    case 2:
                        DataInterface.ResetCurrentData();
                        await LoginOrRegisterAsync();
                        if (Program.WillExit)
                            return;
                        break;
                    case 3:
                        Program.WillExit = true;
                        break;
                }
            }
        }

        private static async Task LoginOrRegisterAsync()
        {
            Console.Clear();


            while (!Program.IsSignedIn && !Program.WillExit)
            {
                WelcomePage();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("请先登录或注册系统。\n");
                Console.ResetColor();

                string[] options = ["登录", "注册", "退出系统"];
                var choiceMenu = SelectMenu(options);
                switch (choiceMenu)
                {
                    case 0:
                        await ConsoleLogin();
                        Console.Clear();
                        break;
                    case 1:
                        await ConsoleRegister();
                        Console.Clear();
                        break;
                    case 2:
                        Program.WillExit = true;
                        return;
                }
            }
            Console.WriteLine("正在下载用户信息，请稍候...");
            await NetworkStack.GetUserProfile();
            Console.SetCursorPosition(0, Console.CursorTop - 1);
        }

        private static async Task ConsoleLogin()
        {
            Console.Clear();
            WelcomePage(false);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("正在登入到系统。\n");
            Console.ResetColor();
            Console.Write("请输入用户名: ");
            Program.CurrentUser.username = Console.ReadLine()!;
            Console.Write("请输入密码: ");
            Program.CurrentUser.password = Console.ReadLine()!;

            Console.WriteLine("\n登录中，请稍候...");
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            var response = await NetworkStack.Login();
            if (NetworkStack.IsSuccessfulStatusCode(response.StatusCode))
            {
                Program.IsSignedIn = true;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("登录成功                      ");
                Console.ResetColor();
                // Task 不会随着函数的 return 而消亡
                var getUserProfileTask = Task.Run(NetworkStack.GetUserProfile);
                await Task.Delay(1500);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("登录失败: {0}", response.Content);
                Console.ResetColor();
                await Task.Delay(2000);
                Console.Clear();
            }
        }

        private static async Task ConsoleRegister()
        {
            Console.Clear();
            WelcomePage(false);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("正在注册到系统。\n");
            Console.ResetColor();
            Console.Write("请输入用户名: ");
            Program.CurrentUser.username = Console.ReadLine()!;
            Console.Write("请输入密码: ");
            Program.CurrentUser.password = Console.ReadLine()!;
            Console.Write("请输入昵称: ");
            Program.CurrentUser.nickname = Console.ReadLine()!;
            bool isMajorValid = false, isQualificationValid = false;
            Console.WriteLine("请输入专业对应的序号");
            while (!isMajorValid)
            {
                Console.Write("“1. cs(computer science)”、“2. math”、“3. physics”: ");
                var majorChoice = Console.ReadLine()!;
                if (new[] { "1", "2", "3" }.Contains(majorChoice))
                {
                    isMajorValid = true;
                    Program.CurrentUser.major = majorChoice switch
                    {
                        "1" => "cs",
                        "2" => "math",
                        "3" => "physics",
                        _ => throw new ArgumentOutOfRangeException()
                    };
                }
                else
                {
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("专业序号输入有误，请重新输入                                          ");
                    Console.ResetColor();
                }
            }

            Console.Write("请输入学历对应的序号");
            while (!isQualificationValid)
            {
                Console.Write("“1. undergraduate”、“2. bachelor”、“3. doctor”: ");

                var qualificationChoice = Console.ReadLine()!;
                if (new[] { "1", "2", "3" }.Contains(qualificationChoice))
                {
                    isQualificationValid = true;
                    Program.CurrentUser.qualification = qualificationChoice switch
                    {
                        "1" => "undergraduate",
                        "2" => "bachelor",
                        "3" => "doctor",
                        _ => throw new ArgumentOutOfRangeException()
                    };
                }
                else
                {
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("学历序号输入有误，请重新输入                                          ");
                    Console.ResetColor();
                }
            }

            Console.WriteLine("\n注册中，请稍候...");
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            var response = await NetworkStack.Register();
            if (NetworkStack.IsSuccessfulStatusCode(response.StatusCode))
            {
                Program.IsSignedIn = true;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("注册成功                      ");
                Console.ResetColor();
                var getUserProfileTask = Task.Run(NetworkStack.GetUserProfile);
                await Task.Delay(1500);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("注册失败: {0}", response.Content);
                Console.ResetColor();
                await Task.Delay(2000);
                Console.Clear();
            }
        }

        private static async Task UserInfoPage()
        {
            Console.Clear();
            WelcomePage();
            Console.WriteLine("以下是您的个人信息：");
            await PrintUserProfile();
            do
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("您想要修改个人信息吗？(Enter / Esc)");
                Console.ResetColor();
                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.Enter:
                        Console.Clear();
                        await ProfileModify();
                        break;
                    case ConsoleKey.Escape:
                        return;
                }
            } while (true);
        }

        private static async Task PrintUserProfile()
        {
            Console.WriteLine("获取用户信息中...");
            await NetworkStack.GetUserProfile();
            Console.SetCursorPosition(0, Console.CursorTop - 1);

            var table = new ConsoleTable("类别", "属性值");
            table.AddRow("用户名", Program.CurrentUser.username)
                .AddRow("昵称", Program.CurrentUser.nickname ?? "未设置")
                .AddRow("学历", Program.CurrentUser.qualification ?? "未设置")
                .AddRow("专业", Program.CurrentUser.major ?? "未设置")
                .AddRow("已选课程数", Program.CurrentUser.selectedCourses?.Length ?? 0)
                .AddRow("已选课程", string.Join(", ", Program.CurrentUser.selectedCourses ?? []));
            Console.ForegroundColor = ConsoleColor.Cyan;
            table.Write();
            Console.ResetColor();
            Console.WriteLine("\n\n");
        }

        private static async Task ProfileModify()
        {
            // 禁止更新用户状态
            Program.IsSignedIn = false;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("以下内容，均非必填。如不想修改，您可以直接按 Enter 略过。\n");
            Console.ResetColor();
            Console.Write("输入新密码: ");
            Program.CurrentUser.password = Console.ReadLine() ?? Program.CurrentUser.password;
            Console.Write("请输入昵称: ");
            Program.CurrentUser.nickname = Console.ReadLine() ?? Program.CurrentUser.nickname;
            bool isMajorValid = false, isQualificationValid = false;
            Console.WriteLine("请输入专业对应的序号");
            while (!isMajorValid)
            {
                Console.Write("“1. cs(computer science)”、“2. math”、“3. physics”: ");
                var majorChoice = Console.ReadLine() ?? "";
                if (new[] { "1", "2", "3", "" }.Contains(majorChoice))
                {
                    isMajorValid = true;
                    Program.CurrentUser.major = majorChoice switch
                    {
                        "1" => "cs",
                        "2" => "math",
                        "3" => "physics",
                        "" => Program.CurrentUser.major,
                        _ => throw new ArgumentOutOfRangeException()
                    };
                }
                else
                {
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("专业序号输入有误，请重新输入                                       ");
                    Console.ResetColor();
                }
            }

            Console.Write("请输入学历对应的序号");
            while (!isQualificationValid)
            {
                Console.Write("“1. undergraduate”、“2. bachelor”、“3. doctor”: ");

                var qualificationChoice = Console.ReadLine()!;
                if (new[] { "1", "2", "3", "" }.Contains(qualificationChoice))
                {
                    isQualificationValid = true;
                    Program.CurrentUser.qualification = qualificationChoice switch
                    {
                        "1" => "undergraduate",
                        "2" => "bachelor",
                        "3" => "doctor",
                        "" => Program.CurrentUser.qualification,
                        _ => throw new ArgumentOutOfRangeException()
                    };
                }
                else
                {
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("学历序号输入有误，请重新输入                                       ");
                    Console.ResetColor();
                }
            }

            Console.WriteLine("\n更新用户信息中，请稍候...");
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            if (await NetworkStack.UpdateUserProfile())
            {
                Program.IsSignedIn = true;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("更新用户信息成功！               ");
                Console.ResetColor();
                await Task.Run(NetworkStack.GetUserProfile);
                Console.Clear();
                Console.WriteLine("更新后信息如下：");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("更新用户信息失败，所有改动未生效。    ");
                Console.ResetColor();
                await Task.Delay(2000);
                Console.Clear();
            }

            await PrintUserProfile();
            Program.IsSignedIn = true;
        }

        private static async Task CourseInfoPage()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("获取用户信息中...");
                await NetworkStack.GetUserProfile();
                Console.Clear();
                WelcomePage();
                await PrintSelectedCourse();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("您想要查询或修改选课信息吗？(Enter / Esc)");
                Console.ResetColor();
                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.Enter:
                        Console.Clear();
                        await CourseModify();
                        break;
                    case ConsoleKey.Escape:
                        return;
                }
            } while (true);
        }

        private static async Task CourseModify()
        {
            Console.Clear();
            WelcomePage();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"您的专业是 {Program.CurrentUser.major ?? "未知"}，您的学历是 {Program.CurrentUser.qualification ?? "未知"}");
            Console.WriteLine("您请注意：除非明确专业，否则只能选择公选课(common)；除非明确学历，否则无法选课。可以从“用户信息维护”中修改个人信息。");
            Console.ResetColor();
            Console.WriteLine("\n您想要选课、退选、查看所有课程信息，还是按种类筛选课程？");

            string[] options = ["选课和退选", "按ID查询课程信息", "查看所有课程信息（不论是否可选）", "按种类筛选课程", "离开本页"];
            var choiceMenu = SelectMenu(options);
            switch (choiceMenu)
            {
                case 0:
                    await CourseSelect();
                    break;
                case 1:
                    await PrintCourseInfoById();
                    break;
                case 2:
                    await PrintAllCourseInfo();
                    break;
                case 3:
                    await PrintFilteredCourseInfo();
                    break;
                case 4:
                    return;
            }
        }

        private static async Task CourseSelect()
        {
            Console.WriteLine("获取可选课程信息中...");
            await NetworkStack.GetAvailableCoursesInfo();
            Console.Clear();
            WelcomePage(false);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("以下是您可以选择的课程：      ");
            Console.ResetColor();
            // 下述方法无法正确处理 Non-ASCII 字符（中文）的长度，故使用库函数。
            // Console.WriteLine("{0,-4} {1,-24} {2,-15} {3,-10} {4,-40}", "ID", "课程名称", "学历", "专业", "描述");
            var table = new ConsoleTable("ID", "课程名称", "学历", "专业", "描述");
            foreach (var course in Program.AvailableCoursesList)
            {
                table.AddRow(course.id, course.name, course.qualification, course.major, course.description);
            }
            table.Write();

            await PrintSelectedCourse();
            Console.WriteLine("请输入您想要选择的课程ID，多个课程ID请用逗号分隔。");
            Console.WriteLine("如需取消某个选课，请在课程ID前加上减号。");
            Console.WriteLine("如需取消所有选课，请输入 “-all”。");
            Console.WriteLine("直接回车将不做变动。");
            Console.Write("您的选择是：");
            var selectedCourses = Console.ReadLine();
            var willEraseAll = false;
            var selectedCourseList = new List<int>();
            var deselectCourseList = new List<int>();
            if (selectedCourses == "-all")
            {
                willEraseAll = true;
            }
            else
            {
                if (string.IsNullOrEmpty(selectedCourses))
                {
                    Console.WriteLine("未做任何变动。");
                    await Task.Delay(2000);
                    return;
                }
                var selectedCourseIds = selectedCourses.Split(',');
                
                foreach (var courseId in selectedCourseIds)
                {
                    // 如果是正数，将数字部分存入 selectedCourseList 中
                    // 如果以“-”开头，将数字部分存入 deselectCourseList 中
                    if (int.TryParse(courseId, out var id))
                    {
                        if (id > 0)
                        {
                            selectedCourseList.Add(id);
                        }
                        else if (id < 0)
                        {
                            deselectCourseList.Add(-id);
                        }
                    }
                }
            }

            Console.WriteLine("\n更新选课信息中，请稍候...");
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            if (await NetworkStack.UpdateCourseSelection(selectedCourseList, deselectCourseList, willEraseAll))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("更新选课信息成功！                ");
                Console.ResetColor();
                await Task.Run(NetworkStack.GetUserProfile);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("更新选课信息失败，可能是由于您选择了非本专业的课程，或是网络异常。所有改动未生效。");
                Console.ResetColor();
                await Task.Delay(2000);
            }
            Console.Clear();
        }

        private static async Task PrintSelectedCourse()
        {
            Console.WriteLine("获取已选课程信息中...");
            await NetworkStack.GetSelectedCoursesInfo();
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("以下是您已选的课程：       ");
            Console.ResetColor();
            var table = new ConsoleTable("ID", "课程名称", "学历", "专业", "描述");
            foreach (var course in Program.SelectedCoursesList)
            {
                table.AddRow(course.id, course.name, course.qualification, course.major, course.description);
            }

            table.Write();
        }

        private static async Task PrintCourseInfoById()
        {
            Console.Clear();
            WelcomePage(false);
            Console.Write("请输入您想查询的课程数字ID: ");
            var courseId = Console.ReadLine();
            if (string.IsNullOrEmpty(courseId))
            {
                Console.WriteLine("您输入的ID有误。");
                return;
            }

            var course = await NetworkStack.GetCourseInfoById(courseId);
            if (course != null)
            {
                Console.WriteLine("课程信息如下：");
                var table = new ConsoleTable("类别", "属性值");
                table.AddRow("ID", course.id);
                table.AddRow("课程名称", course.name);
                table.AddRow("学历", course.qualification);
                table.AddRow("专业", course.major);
                table.AddRow("描述", course.description);
                table.Write();
            }
            else
            {
                Console.WriteLine("查询失败。可能是ID不正确或无权限。");
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n按任意键返回...");
            Console.ResetColor();
            Console.ReadKey(true);
        }

        private static async Task PrintAllCourseInfo()
        {
            Console.Clear();
            Console.WriteLine("获取所有课程信息中...");
            await NetworkStack.GetAllCourseInfo();
            Console.Clear();
            WelcomePage(false);
            var table = new ConsoleTable("ID", "课程名称", "学历", "专业", "描述");
            foreach (var course in Program.AvailableCoursesList)
            {
                table.AddRow(course.id, course.name, course.qualification, course.major, course.description);
            }
            table.Write();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("按任意键继续...");
            Console.ResetColor();
            Console.ReadKey(true);
            Console.Clear();
        }

        private static async Task PrintFilteredCourseInfo()
        {
            Console.Clear();
            WelcomePage(true);
            string[] options = ["按专业筛选", "按学历筛选", "按专业和学历筛选", "离开本页"];
            var choiceMenu = SelectMenu(options);
            switch (choiceMenu)
            {
                case 0:
                    await PrintFilteredByMajor();
                    break;
                case 1:
                    await PrintFilteredByQualification();
                    break;
                case 2:
                    await PrintFilteredByMajorAndQualification();
                    break;
                case 3:
                    return;
            }
        }

        private static async Task PrintFilteredByMajor()
        {
            Console.Clear();
            WelcomePage(false);
            Console.WriteLine("请输入您想要筛选的专业：");
            Console.Write("“1. cs(computer science)”、“2. math”、“3. physics”、“4. common”: ");
            var majorChoice = Console.ReadLine();

            if (new[] { "1", "2", "3", "4" }.Contains(majorChoice))
            {
                var major = majorChoice switch
                {
                    "1" => "cs",
                    "2" => "math",
                    "3" => "physics",
                    "4" => "common",
                    _ => throw new ArgumentOutOfRangeException()
                };

                Console.WriteLine("\n获取相关课程信息中...");
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                await NetworkStack.GetCourseInfoByMajor(major);

                var table = new ConsoleTable("ID", "课程名称", "学历", "专业", "描述");
                foreach (var course in Program.AvailableCoursesList)
                {
                    table.AddRow(course.id, course.name, course.qualification, course.major, course.description);
                }
                table.Write();
            }
            else
            {
                Console.WriteLine("输入有误。");
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("按任意键继续...");
            Console.ResetColor();
            Console.ReadKey(true);
            Console.Clear();
        }

        private static async Task PrintFilteredByQualification()
        {
            Console.Clear();
            WelcomePage(false);
            Console.WriteLine("请输入您想要筛选的学历：");
            Console.Write("“1. undergraduate”、“2. bachelor”、“3. doctor”: ");
            var qualificationChoice = Console.ReadLine();

            if (new[] { "1", "2", "3" }.Contains(qualificationChoice))
            {
                var qualification = qualificationChoice switch
                {
                    "1" => "undergraduate",
                    "2" => "bachelor",
                    "3" => "doctor",
                    _ => throw new ArgumentOutOfRangeException()
                };

                Console.WriteLine("\n获取相关课程信息中...");
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                await NetworkStack.GetCourseInfoByQualification(qualification);

                var table = new ConsoleTable("ID", "课程名称", "学历", "专业", "描述");
                foreach (var course in Program.AvailableCoursesList)
                {
                    table.AddRow(course.id, course.name, course.qualification, course.major, course.description);
                }
                table.Write();
            }
            else
            {
                Console.WriteLine("输入有误。");
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("按任意键继续...");
            Console.ResetColor();
            Console.ReadKey(true);
            Console.Clear();
        }

        private static async Task PrintFilteredByMajorAndQualification()
        {
            Console.Clear();
            WelcomePage(false);
            Console.WriteLine("请输入您想要筛选的专业：");
            Console.Write("“1. cs(computer science)”、“2. math”、“3. physics”、“4. common”: ");
            var majorChoice = Console.ReadLine();

            Console.WriteLine("请输入您想要筛选的学历：");
            Console.Write("“1. undergraduate”、“2. bachelor”、“3. doctor”: ");
            var qualificationChoice = Console.ReadLine();

            if (new[] { "1", "2", "3", "4" }.Contains(majorChoice) && new[] { "1", "2", "3" }.Contains(qualificationChoice))
            {
                var major = majorChoice switch
                {
                    "1" => "cs",
                    "2" => "math",
                    "3" => "physics",
                    "4" => "common",
                    _ => throw new ArgumentOutOfRangeException()
                };

                var qualification = qualificationChoice switch
                {
                    "1" => "undergraduate",
                    "2" => "bachelor",
                    "3" => "doctor",
                    _ => throw new ArgumentOutOfRangeException()
                };

                Console.WriteLine("\n获取相关课程信息中...");
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                await NetworkStack.GetCourseInfoByQualificationAndMajor(qualification, major);

                var table = new ConsoleTable("ID", "课程名称", "学历", "专业", "描述");
                foreach (var course in Program.AvailableCoursesList)
                {
                    table.AddRow(course.id, course.name, course.qualification, course.major, course.description);
                }
                table.Write();
            }
            else
            {
                Console.WriteLine("输入有误。");
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("按任意键继续...");
            Console.ResetColor();
            Console.ReadKey(true);
            Console.Clear();
        }
    }
}
