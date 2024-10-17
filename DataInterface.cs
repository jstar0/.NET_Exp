using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SelectCourseProgram
{
    /*
        export interface IUsers {
             username: string;
             password: string;
             nickname?: string;
             qualification?: 'undergraduate' | 'bachelor' | 'doctor';
             major?: string;
             selectedCourses?: number[];
        }

        export interface ICourse {
             id: number;
             name: string;
             qualification: 'undergraduate' | 'bachelor' | 'doctor';
             major?: string;
             description?: string;
        }
    */
    public class Users(string? username, string? password, string? nickname, string? qualification, string? major, int[]? selectedCourses)
    {
        public Users()
            : this(null, null, null, null, null, null) { }
        public string? username { get; set; } = username;
        public string? password { get; set; } = password;
        public string? nickname { get; set; } = nickname;
        public string? qualification { get; set; } = qualification;
        public string? major { get; set; } = major;
        public int[]? selectedCourses { get; set; } = selectedCourses;
    }

    public class UsersLoginInfo(string? username, string? password)
    {
        public UsersLoginInfo()
            : this(null, null) { }
        public string? username { get; set; } = username;
        public string? password { get; set; } = password;
    }

    public class Courses
    {
        public int id { get; set; }
        public string name { get; set; }
        public string qualification { get; set; }
        public string major { get; set; }
        public string description { get; set; }
    }

    public class NetworkRequest(string? jsonContent)
    {
        public NetworkRequest() : this(null) { }

        public string? JsonContent { get; set; } = jsonContent;
    }

    public class NetworkResponse(int statusCode, string? content)
    {
        public NetworkResponse() : this(0, null) { }

        public int StatusCode { get; set; } = statusCode;
        public string? Content { get; set; } = content;
    }

    internal abstract class DataInterface
    {
        public static Users FormatJsonToUser(string jsonContent)
        {
            try
            {
                return JsonSerializer.Deserialize<Users>(jsonContent)!;
            }
            catch (Exception)
            {
                return new Users();
            }
        }

        public static Courses? FormatJsonToCourse(string jsonContent)
        {
            try 
            {
                return JsonSerializer.Deserialize<Courses>(jsonContent)!;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static Courses[] FormatJsonToCourseMany(string jsonContent)
        {
            try
            {
                return JsonSerializer.Deserialize<Courses[]>(jsonContent)!;
            }
            catch (Exception)
            {
                return [];
            }
        }

        public static string FormatUserToJson(Users person)
        {
            try
            {
                return JsonSerializer.Serialize<Users>(person);
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string FormatUserToJsonPartial(Users person)
        {
            try
            {
                UsersLoginInfo userLoginInfo = new(person.username, person.password);
                return JsonSerializer.Serialize<UsersLoginInfo>(userLoginInfo);
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string FormatJsonToToken(string jsonContent)
        {
            using var doc = JsonDocument.Parse(jsonContent);
            var root = doc.RootElement;

            if (root.TryGetProperty("access_token", out var accessTokenElement))
            {
                var accessToken = accessTokenElement.GetString()!;
                return accessToken;
            }

            throw new Exception("Access token not found in the JSON content.");
        }

        public static void ResetCurrentData()
        {
            Program.CurrentUser = new Users();
            Program.AvailableCoursesList = [];
            Program.SelectedCoursesList = [];
            Program.Token = "";
            Program.WillExit = false;
            Program.IsSignedIn = false;
        }
    }
}
