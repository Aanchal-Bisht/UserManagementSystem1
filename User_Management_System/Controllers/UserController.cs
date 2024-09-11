using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Runtime.InteropServices;
using User_Management_System.Models;
using System.Numerics;
using System.Reflection;
using Microsoft.Extensions.Options;
using static System.Net.WebRequestMethods;
using Newtonsoft.Json;
using System;

namespace User_Management_System.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IOptions<Custom> _custom;
        private readonly IOptions<CustomConfig> _customConfig;


        public User myUser;
        UserRepository newUser = new UserRepository();
     //    UserRepository newUser = new UserRepository();



        public UserController(ILogger<UserController> logger, IConfiguration configuration, IOptions<Custom> custom, IOptions<CustomConfig> customConfig)
        {
            _logger = logger;
            _configuration = configuration;
            _custom = custom;
            _customConfig = customConfig;


        }

        public IActionResult Index()
        {
            return View();
        }
        // method to hit the delete user Api 
        private string deleteUserApi(int userId, string url)
      
        public string PatchUserApi(string username, string email, string password, DateOnly dob, string Gender, string Department, string phone, string url)
        {
            try
            {
                var client = new HttpClient();

                var webRequest = new HttpRequestMessage(HttpMethod.Patch, url);

                var response = client.Send(webRequest);

                using var reader = new StreamReader(response.Content.ReadAsStream());

                return reader.ReadToEnd();
            }
            catch
            {
                return null;
            }
        }
        private string deleteUserApi(int userId, string url)
        {

            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Delete, url);
                var response = client.Send(request);
                using var reader = new StreamReader(response.Content.ReadAsStream());
                var list = reader.ReadToEnd();
                return list;
            }
            catch
            {
                return null;
            }
        }

        [HttpPost]
        public IActionResult Index(string userId, string username, string email, DateOnly dob, string gender, string department, string phone, string buttontype)
        {
            myUser = new User();
            myUser.UserId = Convert.ToInt32(userId);
            myUser.UserName = username;
            myUser.Email = email;
            myUser.DOB = dob.ToString();
            myUser.Gender = gender;
            myUser.DeptName = department;
            myUser.Phone = phone;
            Console.WriteLine(myUser.DeptName);
            //TempData["newuser"] = myUser;
            int id = Convert.ToInt32(myUser.UserId);

            if (buttontype == "save")
            {
                string url = _custom.Value.Patch_API_URL + "?userId=" +userId + "&username=" + username + "&email=" + email + "&gender=" + gender +"&dob=" + dob + "&Department=" + department + "&phone=" + phone; 
                var PatchApiResponseObject = PatchUserApi(userId, username, email, dob, gender,department, phone, url);
                Console.WriteLine(PatchApiResponseObject);
                
            }

            if (buttontype == "delete")
            {
                string url = _customConfig.Value.DeleteUserApiUrl + "?userId=" + userId;
                var deleteApiResponseObject = deleteUserApi(Convert.ToInt32(userId), url);
                Console.WriteLine(deleteApiResponseObject);
                // newUser.DeleteUser(id);
            }

            return View("Index");
        }


        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult UserSignout()
        {

            return View();
        }
        // api hit for getting user details for admin user list 
        private string GetUserListfromApi(string url)
        {
            try
            {
                var client = new HttpClient();
                var webRequest = new HttpRequestMessage(HttpMethod.Get, url);
                var response = client.Send(webRequest);
                using var reader = new StreamReader(response.Content.ReadAsStream());
                var list = reader.ReadToEnd();
                return list;
            }
            catch
            {
                return null;
            }
        }
        public IActionResult UserList()

        {
            //calling api hitting method
            string url = _customConfig.Value.UserListApiUrl;
            var yourClassObject = GetUserListfromApi(url);
            //converting json string to datatable object
            var dataTable = JsonConvert.DeserializeObject<DataTable>(yourClassObject);

            //Console.WriteLine(dataTable);
            //DataTable dt = newUser.getUserList();
            if (dataTable != null)
            {
                ViewBag.DataTable = dataTable;
            }
            return View();
        }
        // api hit for getting user details for login and dashboard 
        private string GetUserDetailsFromAPI(string userName, string pwd, string url)
        {
            //hit the api passing "userName, pwd"

            //receive api response and deserialize it from json to class object

            //return this class object
            try
            {
                var client = new HttpClient();

                var webRequest = new HttpRequestMessage(HttpMethod.Get, url);

                var response = client.Send(webRequest);

                using var reader = new StreamReader(response.Content.ReadAsStream());

                return reader.ReadToEnd();
            }
            catch
            {
                return null;
            }


        }

        [HttpPost]
        public IActionResult RedirectToHome(string userName, string pwd)
        {
            string url = _customConfig.Value.UserDetailsApiUrl + "?userName=" + userName + "&Pass=" + pwd;
            //call the api method here
            var yourClassObject = GetUserDetailsFromAPI(userName, pwd, url);
           // Console.WriteLine(yourClassObject.ToString());
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(yourClassObject);
            //User myu1 = new User();
            //DataTable res = newUser.getUserDetails(userName, pwd);
            //myu1 = newUser.DisplayUser(userName, pwd);
            User user = new User();
            //  List<User> studentList = new List<Student>();
            var rowCount = (dt != null ? dt.Rows.Count : 0);

            for (var i = 0; i < rowCount; i++)
            {

                user.UserId = Convert.ToInt32(dt.Rows[i]["UserId"]);
                user.UserName = dt.Rows[i]["UserName"].ToString();
                user.Password = dt.Rows[i]["Pwd"].ToString();
                user.Email = dt.Rows[i]["Email"].ToString();
                user.Phone = dt.Rows[i]["ContactNo"].ToString();
                user.Gender = dt.Rows[i]["Gender"].ToString();
                user.DOB = dt.Rows[i]["DOB"].ToString();
                user.DeptName = dt.Rows[i]["DeptId"].ToString();
                user.RoleId = dt.Rows[i]["RoleId"].ToString();
                // studentList.Add(student);
            }

            // Console.WriteLine(myu1.RoleId);
            if (rowCount > 0 && string.IsNullOrEmpty(user.RoleId))//common user
            {
                TempData["user"] = userName;
                // User myu1 =newUser.DisplayUser(userName, pwd);
                //  Console.WriteLine(myu1.UserName);
                TempData["userName"] = user.UserName;
                TempData["userId"] = user.UserId;
                TempData["Email"] = user.Email;
                TempData["Gender"] = user.Gender;
                DateTime dateObject = DateTime.Parse(user.DOB);
                TempData["DOB"] = dateObject.Date;
                // Console.WriteLine(TempData["DOB"]); 
                TempData["Phone"] = user.Phone;
                TempData["DeptName"] = user.DeptName;
                TempData["UserRoleId"] = user.RoleId;

                TempData["Check"] = true;
                // Console.WriteLine(TempData["userId"]);
                return RedirectToAction("Index");
            }
            else if (rowCount > 0 && !string.IsNullOrEmpty(user.RoleId))//admin user check
            {
                TempData["UserRoleId"] = user.RoleId;//storing admin user's role-id

                TempData["Check"] = true;//for identifying admin user on different pages

                return RedirectToAction("UserList");//sending admin user to users-list as only admin user has access to this
            }

            else//invalid user
            {

                ViewBag.Message = "Invalid user";
                return View("Login");
            }

        }

        public IActionResult Register()
        {
            return View();
        }
        public string PostApiResponse(string username, string email, string password, DateOnly dob, string Gender, string Department, string phone,string url)
        {
            try
            {
                var client = new HttpClient();

                var webRequest = new HttpRequestMessage(HttpMethod.Post, url);

                var response = client.Send(webRequest);

                using var reader = new StreamReader(response.Content.ReadAsStream());

                return reader.ReadToEnd();
            }
            catch
            {
                return null;
            }
        }

        [HttpPost]
        public IActionResult RedirectToLogin(string username, string email, string password, DateOnly dob, string Gender, string Department, string phone)
        {
            string url = _custom.Value.Post_API_URL + "?username=" + username + "&email=" + email + "&password=" + password + "&gender=" + Gender + "&dob=" +dob + "&Department=" + Department + "&phone=" + phone;

            var classobject = PostApiResponse(username,  email,  password, dob,  Gender,Department,  phone,url);

            myUser = new User();
            myUser.UserName = username;
            myUser.Email = email;
            myUser.Password = password;
            myUser.DOB = dob.ToString();
            myUser.Gender = Gender;
            myUser.DeptName = Department;
            myUser.Phone = phone;

            //TempData["newuser"] = myUser;
            newUser.AddUser(myUser);
            return RedirectToAction("Login");
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Services()

        {
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
