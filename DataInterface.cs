using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.VisualBasic.ApplicationServices;

namespace StudentInformationEntrySystem
{
    public class Users
    {
        public string? username { get; set; }
        public string? password { get; set; }
        public string? schoolId { get; set; }
        public BasicInfo? basic { get; set; }
        public List<int>? selectedCourses { get; set; }
        public FamilyInfo? family { get; set; }
        public string? photo { get; set; }

        public Users(string username, string password, string schoolId, BasicInfo? basic = null, List<int>? selectedCourses = null, FamilyInfo? family = null)
        {
            this.username = username;
            this.password = password;
            this.schoolId = schoolId;
            this.basic = basic;
            this.selectedCourses = selectedCourses;
            this.family = family;
        }

        public Users() {}
    }

    public class BasicInfo
    {
        public string name { get; set; }
        public int age { get; set; }
        public string gender { get; set; }
        public string ancestry { get; set; }
        public string political { get; set; }
        public string qualification { get; set; }
        public string major { get; set; }
        public string contact { get; set; }
        public string profile { get; set; }

        public BasicInfo(string name, int age, string gender, string ancestry, string political, string qualification, string major, string contact, string profile)
        {
            this.name = name;
            this.age = age;
            this.gender = gender;
            this.ancestry = ancestry;
            this.political = political;
            this.qualification = qualification;
            this.major = major;
            this.contact = contact;
            this.profile = profile;
        }
    }

    public class FamilyInfo
    {
        public string birth { get; set; }
        public string living { get; set; }
        public string zipCode { get; set; }
        public string homePhone { get; set; }
        public string address { get; set; }
        public bool livingInCity { get; set; }

        public FamilyInfo(string birth, string living, string zipCode, string homePhone, string address, bool livingInCity)
        {
            this.birth = birth;
            this.living = living;
            this.zipCode = zipCode;
            this.homePhone = homePhone;
            this.address = address;
            this.livingInCity = livingInCity;
        }
    }

    public class UsersLoginInfo(string? username, string? password)
    {
        public UsersLoginInfo()
            : this(null, null) { }
        public string? username { get; set; } = username;
        public string? password { get; set; } = password;
    }

    public class UsersRegisterInfo(string? username, string? password, string? schoolId)
    {
        public UsersRegisterInfo()
            : this(null, null, null) { }
        public string? username { get; set; } = username;
        public string? password { get; set; } = password;
        public string? schoolId { get; set; } = schoolId;
    }

    public class UsersPhoto
    {
        public UsersPhoto(string? photo)
        {
            this.photo = photo;
        }
        public string? photo { get; set; }
    }

    public class Courses
    {
        public int id { get; set; }
        public string name { get; set; }
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

        public static string? FormatUserToJsonWithoutPhoto(Users person)
        {
            try
            {
                // 删去 person 中的 photo 属性
                person.photo = null;
                return JsonSerializer.Serialize(person);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string FormatUserToJsonPartial(UsersLoginInfo person)
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

        public static string FormatUserRegisterToJson(UsersRegisterInfo person)
        {
            try
            {
                UsersRegisterInfo userRegisterInfo = new(person.username, person.password, person.schoolId);
                return JsonSerializer.Serialize(userRegisterInfo);
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string FormatUserForgetToJson(object forgetInfo)
        {
            try
            {
                return JsonSerializer.Serialize(forgetInfo);
            }
            catch (Exception)
            {
                return "";
            }
        }




        public static string? FormatJsonToToken(string jsonContent)
        {
            // 先判断是不是Json
            if (!jsonContent.StartsWith("{") || !jsonContent.EndsWith("}"))
            {
                return null;
            }
            using var doc = JsonDocument.Parse(jsonContent);
            var root = doc.RootElement;

            if (root.TryGetProperty("access_token", out var accessTokenElement))
            {
                var accessToken = accessTokenElement.GetString()!;
                return accessToken;
            }
            return null;
        }

        public static void ResetCurrentData()
        {
            Program.CurrentUser = new Users();
            Program.Token = null;
            Program.AvailableCoursesList = [];
            Program.SelectedCoursesList = [];
            Program.WillExit = false;
            Program.IsSignedIn = false;
        }
    }
}
