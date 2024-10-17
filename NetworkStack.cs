using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SelectCourseProgram;

namespace SelectCourseProgram
{
    internal abstract class NetworkStack
    {
        public static async Task<NetworkResponse> GetAsync(string url, bool? useAuthBearer = true)
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

        public static async Task<NetworkResponse> Login()
        {
            Program.CurrentPostReq.JsonContent = DataInterface.FormatUserToJsonPartial(Program.CurrentUser);
            var response = await PostAsync(Program.BaseUrl + "/auth/login", Program.CurrentPostReq, false);
            if (IsSuccessfulStatusCode((int)response.StatusCode))
            {
                Program.Token = DataInterface.FormatJsonToToken(response.Content!);
            }
            return response;
        }

        public static async Task<NetworkResponse> Register()
        {
            Program.CurrentPostReq.JsonContent = DataInterface.FormatUserToJson(Program.CurrentUser);
            var response = await PostAsync(Program.BaseUrl + "/auth/register", Program.CurrentPostReq, false);
            if (IsSuccessfulStatusCode((int)response.StatusCode))
            {
                Program.Token = DataInterface.FormatJsonToToken(response.Content!);
            }
            return response;
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
            Program.CurrentPostReq.JsonContent = DataInterface.FormatUserToJson(Program.CurrentUser);
            var response = await PostAsync(Program.BaseUrl + "/users/profile", Program.CurrentPostReq);
            return IsSuccessfulStatusCode((int)response.StatusCode);
        }

        public static async Task<bool> UpdateCourseSelection(List<int> selectedCourseList, List<int> deselectCourseList, bool? willEraseAll = false)
        {
            if (willEraseAll == true)
            {
                Program.CurrentPostReq.JsonContent = "{\"selectedCourses\": []}";
            }
            else
            {
                Program.IsSignedIn = false;
                // 从 Program.CurrentUser.selectCourse 中 先移除 deselectCourseList 中的元素，再添加 selectedCourseList 中的元素（不重复）
                Program.CurrentUser.selectedCourses = Program.CurrentUser.selectedCourses?.Except(deselectCourseList).Concat(selectedCourseList).Distinct().ToArray();
                Program.CurrentPostReq.JsonContent = JsonSerializer.Serialize(new { Program.CurrentUser.selectedCourses });
                Program.IsSignedIn = true;
            }
            var response = await PostAsync(Program.BaseUrl + "/courses/update", Program.CurrentPostReq);
            return IsSuccessfulStatusCode((int)response.StatusCode);
        }

        public static async Task GetAvailableCoursesInfo()
        {
            var response = await GetAsync(Program.BaseUrl + "/courses/search/qamajor/" + Program.CurrentUser.qualification + "/" + Program.CurrentUser.major);
            if (IsSuccessfulStatusCode((int)response.StatusCode))
            {
                Program.AvailableCoursesList = DataInterface.FormatJsonToCourseMany(response.Content!);
            }
            var responseCommon = await GetAsync(Program.BaseUrl + "/courses/search/qamajor/" + Program.CurrentUser.qualification + "/common");
            if (IsSuccessfulStatusCode((int)responseCommon.StatusCode))
            {
                Program.AvailableCoursesList = [.. Program.AvailableCoursesList, .. DataInterface.FormatJsonToCourseMany(responseCommon.Content!)];
            }
        }

        public static async Task GetSelectedCoursesInfo()
        {
            var response = await GetAsync(Program.BaseUrl + "/courses/selected");
            if (IsSuccessfulStatusCode((int)response.StatusCode))
            {
                Program.SelectedCoursesList = DataInterface.FormatJsonToCourseMany(response.Content!);
            }
        }

        public static async Task<Courses?> GetCourseInfoById(string courseId)
        {
            var response = await GetAsync(Program.BaseUrl + "/courses/search/courseId/" + courseId);
            if (IsSuccessfulStatusCode((int)response.StatusCode))
            {
                return DataInterface.FormatJsonToCourse(response.Content!);
            }
            return null;
        }

        public static async Task GetAllCourseInfo()
        {
            var response = await GetAsync(Program.BaseUrl + "/courses/list");
            if (IsSuccessfulStatusCode((int)response.StatusCode))
            {
                Program.AvailableCoursesList = DataInterface.FormatJsonToCourseMany(response.Content!);
            }
        }

        public static async Task GetCourseInfoByMajor(string major)
        {
            var response = await GetAsync(Program.BaseUrl + "/courses/search/major/" + major);
            if (IsSuccessfulStatusCode((int)response.StatusCode))
            {
                Program.AvailableCoursesList = DataInterface.FormatJsonToCourseMany(response.Content!);
            }
        }

        public static async Task GetCourseInfoByQualification(string qualification)
        {
            var response = await GetAsync(Program.BaseUrl + "/courses/search/qualification/" + qualification);
            if (IsSuccessfulStatusCode((int)response.StatusCode))
            {
                Program.AvailableCoursesList = DataInterface.FormatJsonToCourseMany(response.Content!);
            }
        }

        public static async Task GetCourseInfoByQualificationAndMajor(string qualification, string major)
        {
            var response = await GetAsync(Program.BaseUrl + "/courses/search/qamajor/" + qualification + "/" + major);
            if (IsSuccessfulStatusCode((int)response.StatusCode))
            {
                Program.AvailableCoursesList = DataInterface.FormatJsonToCourseMany(response.Content!);
            }
        }

        public static async Task TimelyGetUserProfile()
        {
            while (Program.IsSignedIn)
            {
                await GetUserProfile();
                await Task.Delay(60000);
            }
        }

        public static bool IsSuccessfulStatusCode(int statusCode)
        {
            return statusCode >= 200 && statusCode < 300;
        }
    }
}
