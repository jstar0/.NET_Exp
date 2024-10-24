using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StudentInformationEntrySystem
{
    internal abstract class NetworkStack
    {
        private static async Task<NetworkResponse> GetAsync(string url, bool? useAuthBearer = true)
        {
            using var client = new System.Net.Http.HttpClient();
            try
            {
                if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
                {
                    return new NetworkResponse(-1, "URL Invalid.");
                }

                System.Net.Http.HttpRequestMessage request = new(System.Net.Http.HttpMethod.Get, url);
                if (Program.Token != "" && useAuthBearer == true)
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Program.Token);
                request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.SendAsync(request);

                return FormatResponse(response);
            }
            catch (System.Net.Http.HttpRequestException e)
            {
                return new NetworkResponse(-1, e.Message);
            }
        }

        private static async Task<NetworkResponse> PostAsync(string url, NetworkRequest req, bool? useAuthBearer = true)
        {
            using var client = new System.Net.Http.HttpClient();
            try
            {
                if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
                {
                    return new NetworkResponse(-1, "URL Invalid.");
                }

                System.Net.Http.HttpRequestMessage request = new(System.Net.Http.HttpMethod.Post, url)
                {
                    Content = new System.Net.Http.StringContent(req.JsonContent!, Encoding.UTF8, "application/json")
                };

                if (Program.Token != "" && useAuthBearer == true)
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Program.Token);
                request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.SendAsync(request);

                return FormatResponse(response);
            }
            catch (System.Net.Http.HttpRequestException e)
            {
                return new NetworkResponse(-1, e.Message);
            }
        }

        private static NetworkResponse FormatResponse(HttpResponseMessage response)
        {
            try
            {
                return new NetworkResponse((int)response.StatusCode, response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception e)
            {
                return new NetworkResponse(-1, e.Message);
            }
        }

        public static async Task<string> Login(string username, string password)
        {
            var loginInfo = new UsersLoginInfo(username, password);
            var jsonContent = DataInterface.FormatUserToJsonPartial(loginInfo);
            Program.CurrentPostReq.JsonContent = jsonContent;
            var response = await PostAsync(Program.BaseUrl + "/auth/login", Program.CurrentPostReq, false);
            if (IsSuccessfulStatusCode((int)response.StatusCode))
            {
                Program.Token = DataInterface.FormatJsonToToken(response.Content!);
                Program.IsSignedIn = true;
                return "OK";
            }

            return "错误信息：" + (response.Content != null && response.Content.StartsWith("{")
                ? JsonSerializer.Deserialize<JsonElement>(response.Content).GetProperty("message").GetString()
                : response.Content);
        }

        public static async Task<string> Register(string username, string password, string schoolId)
        {
            var registerInfo = new UsersRegisterInfo(username, password, schoolId);
            Program.CurrentPostReq.JsonContent = DataInterface.FormatUserRegisterToJson(registerInfo);
            var response = await PostAsync(Program.BaseUrl + "/auth/register", Program.CurrentPostReq, false);
            if (IsSuccessfulStatusCode((int)response.StatusCode))
            {
                Program.Token = DataInterface.FormatJsonToToken(response.Content!);
            }
            if (!IsSuccessfulStatusCode(response.StatusCode))
            {
                return "错误原因: " + JsonSerializer.Deserialize<JsonElement>(response.Content!).GetProperty("message").GetString();
            }
            Program.IsSignedIn = true;
            return "OK";
        }

        public static async Task<string?> Forget(string schoolId)
        {
            var forgetInfo = new { schoolId };
            Program.CurrentPostReq.JsonContent = DataInterface.FormatUserForgetToJson(forgetInfo);
            var response = await PostAsync(Program.BaseUrl + "/users/bySchoolId", Program.CurrentPostReq, false);
            return response.Content;
        }

        public static async Task GetUserProfile()
        {
            var response = await GetAsync(Program.BaseUrl + "/users/profile");
            if (IsSuccessfulStatusCode((int)response.StatusCode))
            {
                Program.CurrentUser = DataInterface.FormatJsonToUser(response.Content!);
            }
        }

        public static async Task<bool> UpdateUserProfile()
        {
            Program.CurrentPostReq.JsonContent = DataInterface.FormatUserToJsonWithoutPhoto(Program.CurrentUser);
            var response = await PostAsync(Program.BaseUrl + "/users/profile", Program.CurrentPostReq);
            return IsSuccessfulStatusCode((int)response.StatusCode);
        }

        public static async Task<bool> UpdateUserPhoto(string compressedImgBase64)
        {
            var userPhoto = new { photo = compressedImgBase64 };
            Program.CurrentPostReq.JsonContent = JsonSerializer.Serialize(userPhoto);

            var response = await PostAsync(Program.BaseUrl + "/users/photo", Program.CurrentPostReq);
            return IsSuccessfulStatusCode((int)response.StatusCode);
        }

        public static async Task<Image?> GetUserPhoto()
        {
            var response = await GetAsync(Program.BaseUrl + "/users/photo");
            if (IsSuccessfulStatusCode((int)response.StatusCode))
            {
                if (String.IsNullOrEmpty(response.Content))
                {
                    return null;
                }
                Program.CurrentUser.photo = response.Content;
                byte[] imgBytes = Convert.FromBase64String(response.Content);
                using var ms = new MemoryStream(imgBytes);
                return Image.FromStream(ms);
            }
            return null;
        }

        public static async Task GetCoursesList(string major)
        {
            // 获取 AvailableCoursesList
            var response = await GetAsync(Program.BaseUrl + "/courses/search/major/" + major);
            if (IsSuccessfulStatusCode((int)response.StatusCode))
            {
                var courseMajorList = DataInterface.FormatJsonToCourseMany(response.Content!);
                // 从 courseList 提取 id 作为 key，然后将 Courses 对象作为 value
                Program.AvailableCoursesList = courseMajorList.ToDictionary(course => course.id);
            }
            var responseCommon = await GetAsync(Program.BaseUrl + "/courses/search/major/common");
            if (IsSuccessfulStatusCode((int)responseCommon.StatusCode))
            {
                var courseCommonList = DataInterface.FormatJsonToCourseMany(response.Content!);
                // 追加到 Program.CoursesList
                foreach (var course in courseCommonList)
                {
                    Program.AvailableCoursesList.TryAdd(course.id, course);
                }
            }

            // 获取 SelectedCoursesList
            var responseSelected = await GetAsync(Program.BaseUrl + "/courses/selected");
            if (IsSuccessfulStatusCode((int)responseSelected.StatusCode))
            {
                var courseSelectedList = DataInterface.FormatJsonToCourseMany(responseSelected.Content!);
                Program.SelectedCoursesList = courseSelectedList.ToDictionary(course => course.id);
            }

            // 取得 CanSelectCoursesList （AvailableCoursesList - SelectedCoursesList）
            Program.CanSelectCoursesList = Program.AvailableCoursesList
                .Where(course => !Program.SelectedCoursesList.ContainsKey(course.Key))
                .ToDictionary(course => course.Key, course => course.Value);

        }

        public static bool IsSuccessfulStatusCode(int statusCode)
        {
            return statusCode >= 200 && statusCode < 300;
        }
    }
}
